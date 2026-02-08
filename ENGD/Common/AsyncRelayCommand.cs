// SPDX-License-Identifier: GPL-3.0-or-later

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ENGD.Common
{
    public sealed class AsyncRelayCommand : ICommand
    {
        private readonly Func<CancellationToken, Task> _executeAsync;
        private readonly Func<bool>? _canExecute;
        private CancellationTokenSource? _cts;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<CancellationToken, Task> executeAsync, Func<bool>? canExecute = null)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke() ?? true);

        public async void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            _isExecuting = true;
            RaiseCanExecuteChanged();

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            try
            {
                await _executeAsync(_cts.Token).ConfigureAwait(false);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void Cancel() => _cts?.Cancel();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
