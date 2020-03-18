using System;
using System.Collections.Generic;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Localization;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels
{
    public class ErrorEmulatorVm : Bindable
    {
        private readonly ErrorEmulator _errorEmulator;
        private readonly Action _onErrorTypeChanged;

        private int _selectedIndex;

        public ErrorEmulatorVm(ErrorEmulator errorEmulator, Action onErrorTypeChanged)
        {
            _errorEmulator = errorEmulator;
            _onErrorTypeChanged = onErrorTypeChanged;

            ErrorTypes = ErrorEmulator.ErrorLabels;
        }

        public IReadOnlyList<string> ErrorTypes { get; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (SetAndRaise(ref _selectedIndex, value))
                {
                    _errorEmulator.ErrorType = (ErrorType)_selectedIndex;
                    _onErrorTypeChanged();
                }
            }
        }
    }
}
