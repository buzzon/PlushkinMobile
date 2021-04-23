using PlushkinForms.Views;
using System;
using System.ComponentModel;
using System.Windows.Input;
using ValidationsXFSample.Validators;
using ValidationsXFSample.Validators.Rules;
using Xamarin.Forms;

namespace PlushkinForms.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public ValidatableObject<string> FirstName { get; set; } = new ValidatableObject<string>();

        public Command RegisterGoogleCommand { get; }
        public Command LoginCommand { get; }

        public RegisterViewModel()
        {
            AddValidationRules();

            RegisterGoogleCommand = new Command(OnRegisterGoogleClicked);
            LoginCommand = new Command(OnLoginClicked);
        }

        public ICommand RegisterCommand => new Command(async () =>
        {
            if (AreFieldsValid())
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        });

        private void AddValidationRules() 
        {
            FirstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "First Name Required" });
        }

        bool AreFieldsValid()
        {
            bool isFirstNameValid = FirstName.Validate();
            return isFirstNameValid;
        }

        private async void OnRegisterGoogleClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AuthorizationPage)}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
