using PlushkinForms.ViewModels;
using Xamarin.Forms;

namespace PlushkinForms.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
        }
    }
}