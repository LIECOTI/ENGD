// SPDX-License-Identifier: GPL-3.0-or-later

using System.Threading;
using System.Threading.Tasks;
using ENGD.Common;
using ENGD.Services;
using ENGD.UI.Pages;

namespace ENGD.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly IAlertService _alertService;
        private readonly IProcessRunner _processRunner;
        private string _currentPageTitle = "Home";
        private object? _currentPageContent;

        public MainViewModel(IAlertService alertService, IProcessRunner processRunner)
        {
            _alertService = alertService;
            _processRunner = processRunner;

            OpenSettingsCommand = new RelayCommand(OpenSettings);
            OpenHelpCommand = new AsyncRelayCommand(OpenHelpAsync);

            OpenSettings();
        }

        public string CurrentPageTitle
        {
            get => _currentPageTitle;
            set => SetProperty(ref _currentPageTitle, value);
        }

        public object? CurrentPageContent
        {
            get => _currentPageContent;
            set => SetProperty(ref _currentPageContent, value);
        }

        public RelayCommand OpenSettingsCommand { get; }

        public AsyncRelayCommand OpenHelpCommand { get; }

        private void OpenSettings()
        {
            CurrentPageTitle = "Settings";
            CurrentPageContent = new SettingsPage
            {
                DataContext = new SettingsViewModel()
            };
        }

        private async Task OpenHelpAsync(CancellationToken cancellationToken)
        {
            await _alertService.ShowDangerWarningAsync(
                "Help",
                "Help content is not yet available. Dangerous actions always require confirmation.",
                requireExplicitConfirmation: false,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
