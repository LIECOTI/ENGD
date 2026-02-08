// SPDX-License-Identifier: GPL-3.0-or-later
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ENGD.Services
{
    public interface IAlertService
    {
        Task<bool> ShowDangerWarningAsync(string title, string message, bool requireExplicitConfirmation = true, CancellationToken cancellationToken = default);
    }

    public sealed class AlertService : IAlertService
    {
        private readonly IWindowManager _windowManager; // inject your window manager

        public AlertService(IWindowManager windowManager)
        {
            _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        }

        public async Task<bool> ShowDangerWarningAsync(string title, string message, bool requireExplicitConfirmation = true, CancellationToken cancellationToken = default)
        {
            var vm = new ENGD.UI.Controls.WarningDialogViewModel(title, message, requireExplicitConfirmation);
            var result = await _windowManager.ShowDialogAsync<bool>(vm, cancellationToken).ConfigureAwait(false);
            // TODO: log user decision to application logs
            return result;
        }
    }
}
