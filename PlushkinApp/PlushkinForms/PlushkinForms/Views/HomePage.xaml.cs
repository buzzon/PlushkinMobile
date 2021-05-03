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
    public partial class HomePage : ContentPage
    {
        ApplicationViewModel viewModel;
        public HomePage()
        {
            InitializeComponent();
            viewModel = new ApplicationViewModel() { Navigation = this.Navigation }; 
            BindingContext = viewModel;
        }
        protected override async void OnAppearing()
        {
            await viewModel.GetBookmarks();
            base.OnAppearing();
        }
    }
}