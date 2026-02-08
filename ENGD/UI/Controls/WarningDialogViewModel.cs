// SPDX-License-Identifier: GPL-3.0-or-later

using System.Threading.Tasks;
using ENGD.Common;
using ENGD.ViewModels;

namespace ENGD.UI.Controls
{
    public sealed class WarningDialogViewModel : ViewModelBase
    {
        private bool _confirmChecked;
        private readonly TaskCompletionSource<bool> _resultTcs = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public WarningDialogViewModel(string title, string message, bool requireExplicitConfirmation)
        {
            Title = title;
            Message = message;
            RequireExplicitConfirmation = requireExplicitConfirmation;

            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        public string Title { get; }

        public string Message { get; }

        public bool RequireExplicitConfirmation { get; }

        public bool ConfirmChecked
        {
            get => _confirmChecked;
            set
            {
                if (SetProperty(ref _confirmChecked, value))
                {
                    (ConfirmCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public Task<bool> ResultTask => _resultTcs.Task;

        public RelayCommand ConfirmCommand { get; }

        public RelayCommand CancelCommand { get; }

        private bool CanConfirm() => !RequireExplicitConfirmation || ConfirmChecked;

        private void Confirm() => _resultTcs.TrySetResult(true);

        private void Cancel() => _resultTcs.TrySetResult(false);
    }
}
