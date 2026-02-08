// SPDX-License-Identifier: GPL-3.0-or-later

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ENGD.Services
{
    public sealed class FileService : IFileService
    {
        public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
        {
            return File.ReadAllTextAsync(path, cancellationToken);
        }

        public Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = default)
        {
            return File.WriteAllTextAsync(path, contents, cancellationToken);
        }
    }
}
