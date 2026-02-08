// SPDX-License-Identifier: GPL-3.0-or-later

using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using ENGD.UI.Controls;

namespace ENGD.Services
{
    public sealed class AvaloniaWindowManager : IWindowManager
    {
        public async Task<TResult> ShowDialogAsync<TResult>(object viewModel, CancellationToken cancellationToken = default)
        {
            if (viewModel is not WarningDialogViewModel warningViewModel)
            {
                throw new NotSupportedException($"Unsupported dialog view model: {viewModel.GetType().Name}");
            }

            var dialogWindow = new Window
            {
                Width = 560,
                Height = 320,
                CanResize = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Content = new WarningDialog
                {
                    DataContext = warningViewModel
                }
            };

            var owner = GetOwnerWindow();
            using var cancellationRegistration = cancellationToken.Register(() =>
            {
                Dispatcher.UIThread.Post(dialogWindow.Close);
            });

            _ = warningViewModel.ResultTask.ContinueWith(_ =>
            {
                Dispatcher.UIThread.Post(dialogWindow.Close);
            }, TaskScheduler.Default);

            if (owner == null)
            {
                dialogWindow.Show();
            }
            else
            {
                _ = dialogWindow.ShowDialog(owner);
            }

            var result = await warningViewModel.ResultTask.ConfigureAwait(false);
            return (TResult)(object)result;
        }

        private static Window? GetOwnerWindow()
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                return desktop.MainWindow;
            }

            return null;
        }
    }
}
