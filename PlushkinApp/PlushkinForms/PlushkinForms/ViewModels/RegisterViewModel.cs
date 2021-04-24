using PlushkinForms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ValidationsXFSample.Validators;
using ValidationsXFSample.Validators.Rules;
using Xamarin.Forms;

namespace PlushkinForms.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsNotValid { get; set; } = true;

        public ValidatableObject<string> FirstName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();

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
            FirstName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Требуется ввести Имя" });

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Требуется ввести Email" });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "Неверный Email" });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Требуется ввести пароль" });
            Password.Validations.Add(new IsValidPasswordRule<string> { ValidationMessage = "Пароль должен содержать не менее одной строчной буквы, одной заглавной буквы, одной цифровой цифры и одного специального символа." });

        }

        bool AreFieldsValid()
        {
            bool isFirstNameValid = FirstName.Validate();
            bool isEmailValid = Email.Validate();
            bool isPasswordValid = Password.Validate();

            IsNotValid = !(isFirstNameValid && isEmailValid && isPasswordValid);

            Errors.Clear();
            Errors.AddRange(FirstName.Errors);
            Errors.AddRange(Email.Errors);
            Errors.AddRange(Password.Errors);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotValid)));

            return isFirstNameValid && isEmailValid && isPasswordValid;
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
