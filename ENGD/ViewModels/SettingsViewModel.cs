// SPDX-License-Identifier: GPL-3.0-or-later

namespace ENGD.ViewModels
{
    public sealed class SettingsViewModel : ViewModelBase
    {
        private bool _useMica = true;
        private bool _useRoundedCorners = true;

        public bool UseMica
        {
            get => _useMica;
            set => SetProperty(ref _useMica, value);
        }

        public bool UseRoundedCorners
        {
            get => _useRoundedCorners;
            set => SetProperty(ref _useRoundedCorners, value);
        }
    }
}
