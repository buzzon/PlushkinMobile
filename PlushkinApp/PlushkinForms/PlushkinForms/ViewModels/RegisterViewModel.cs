using PlushkinForms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PlushkinForms.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }
        public Command RegisterGoogleCommand { get; }
        public Command LoginCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
            RegisterGoogleCommand = new Command(OnRegisterGoogleClicked);
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnRegisterGoogleClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AuthorizationPage)}");
        }
    }
}
