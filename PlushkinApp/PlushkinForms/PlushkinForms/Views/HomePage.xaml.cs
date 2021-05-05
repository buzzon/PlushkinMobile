using PlushkinForms.Models;
using PlushkinForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static PlushkinForms.Services.BookmarkService;

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
            await viewModel.GetBookmarks(TypeFilter.Empty);
            base.OnAppearing();


            MessagingCenter.Subscribe<object, string[]>(this, "AddItem", (sender, arg) =>
            {
                viewModel.SaveBookmark(new Bookmark() { type = "U", title = arg[0], url = arg[1] });
                MessagingCenter.Unsubscribe<object, string[]>(this, "AddItem");
            });
        }
    }
}