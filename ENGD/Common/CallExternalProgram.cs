// SPDX-License-Identifier: GPL-3.0-or-later
// ENGD — CallExternalProgram async helper for .NET 10

using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ENGD.Common
{
    public sealed class ProcessResult
    {
        public int ExitCode { get; }
        public string StdOut { get; }
        public string StdErr { get; }

        public ProcessResult(int exitCode, string stdOut, string stdErr)
        {
            ExitCode = exitCode;
            StdOut = stdOut ?? string.Empty;
            StdErr = stdErr ?? string.Empty;
        }
    }

    public interface IProcessRunner
    {
        Task<ProcessResult> RunAsync(string fileName, string arguments, string? workingDirectory = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default);
    }

    public sealed class CallExternalProgram : IProcessRunner
    {
        public async Task<ProcessResult> RunAsync(string fileName, string arguments, string? workingDirectory = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("fileName must be provided", nameof(fileName));
            }

            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments ?? string.Empty,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory ?? Environment.CurrentDirectory
            };

            using var proc = new Process { StartInfo = psi, EnableRaisingEvents = true };

            var stdoutBuilder = new StringBuilder();
            var stderrBuilder = new StringBuilder();

            var stdoutTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var stderrTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            proc.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null) stdoutTcs.TrySetResult(true);
                else stdoutBuilder.AppendLine(e.Data);
            };
            proc.ErrorDataReceived += (s, e) =>
            {
                if (e.Data == null) stderrTcs.TrySetResult(true);
                else stderrBuilder.AppendLine(e.Data);
            };

            try
            {
                if (!proc.Start())
                {
                    throw new InvalidOperationException("Failed to start process: " + fileName);
                }

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                if (timeout.HasValue && timeout.Value != Timeout.InfiniteTimeSpan)
                {
                    linkedCts.CancelAfter(timeout.Value);
                }

                var processExitTask = Task.Run(() =>
                {
                    proc.WaitForExit();
                    return proc.ExitCode;
                }, linkedCts.Token);

                try
                {
                    var exitCode = await processExitTask.ConfigureAwait(false);
                    await Task.WhenAll(stdoutTcs.Task, stderrTcs.Task).ConfigureAwait(false);
                    return new ProcessResult(exitCode, stdoutBuilder.ToString(), stderrBuilder.ToString());
                }
                catch (OperationCanceledException) when (linkedCts.IsCancellationRequested)
                {
                    try
                    {
                        if (!proc.HasExited) proc.Kill(true);
                    }
                    catch
                    {
                        // ignore
                    }

                    throw new TaskCanceledException($"Process execution canceled: {fileName} {arguments}");
                }
            }
            finally
            {
                // detach events
                proc.OutputDataReceived -= null!;
                proc.ErrorDataReceived -= null!;
            }
        }
    }
}
