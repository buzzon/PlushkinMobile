using PlushkinForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlushkinForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel viewModel;

        public ProfilePage()
        {
            InitializeComponent();
            viewModel = new ProfileViewModel();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await viewModel.GetUser();
            base.OnAppearing();
        }
    }
}