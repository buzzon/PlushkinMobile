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
    class AuthorizationViewModel : INotifyPropertyChanged
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsNotValid { get; set; } = true;

        public ValidatableObject<string> Email { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();

        public AuthorizationViewModel() 
        {
            AddValidationRules();
        }

        public ICommand PasswordRecoveryCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync($"//{nameof(PasswordRecoveryPage)}");
        });

        public ICommand ShowRegisterPageCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        });

        public ICommand LoginCommand => new Command(async () =>
        {
            if (AreFieldsValid())
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        });

        private void AddValidationRules()
        {
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Требуется ввести Email" });
            Email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "Неверный Email" });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Требуется ввести пароль" });
            Password.Validations.Add(new IsValidPasswordRule<string> { ValidationMessage = "Неверный пароль" });
        }

        bool AreFieldsValid()
        {
            Email.Value = Email.Value?.Replace(" ", "");
            bool isEmailValid = Email.Validate();
            bool isPasswordValid = Password.Validate();

            IsNotValid = !(isEmailValid && isPasswordValid);

            Errors.Clear();
            Errors.AddRange(Email.Errors);
            Errors.AddRange(Password.Errors);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotValid)));

            return isEmailValid && isPasswordValid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
