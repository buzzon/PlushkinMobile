using PlushkinForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlushkinForms.ViewModels
{
    class AuthorizationViewModel : BaseViewModel
    {
        public Command PasswordRecoveryCommand { get; }
        public Command ShowRegisterPageCommand { get; }

        public Command LoginCommand { get; }

        public AuthorizationViewModel() 
        {
            PasswordRecoveryCommand = new Command(OnPasswordRecoveryClicked);
            ShowRegisterPageCommand = new Command(OnShowRegisterPageClicked);
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnPasswordRecoveryClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(PasswordRecoveryPage)}");
        }

        private async void OnShowRegisterPageClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}
