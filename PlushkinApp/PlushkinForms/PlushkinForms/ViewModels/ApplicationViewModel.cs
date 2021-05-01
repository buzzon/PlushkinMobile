using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using PlushkinForms.Models;
using PlushkinForms.Services;

namespace PlushkinForms.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        bool initialized = false;   // была ли начальная инициализация
        Bookmark selectedBookmark;  // выбранный друг
        private bool isBusy;    // идет ли загрузка с сервера

        public ObservableCollection<Bookmark> Bookmarks { get; set; }
        BookmarkService bookmarkService = new BookmarkService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateBookmarkCommand { protected set; get; }
        public ICommand DeleteBookmarkCommand { protected set; get; }
        public ICommand SaveBookmarkCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public ApplicationViewModel()
        {
            Bookmarks = new ObservableCollection<Bookmark>();
            IsBusy = false;
            CreateBookmarkCommand = new Command(CreateBookmark);
            DeleteBookmarkCommand = new Command(DeleteBookmark);
            SaveBookmarkCommand = new Command(SaveBookmark);
            BackCommand = new Command(Back);
        }

        public Bookmark SelectedBookmark
        {
            get { return selectedBookmark; }
            set
            {
                if (selectedBookmark != value)
                {
                    Bookmark tempBookmark = new Bookmark()
                    {
                        Id = value.Id,
                        Type = value.Type,
                        Title = value.Title,
                        Url = value.Url,
                        Date = value.Date,
                        User = value.User
                    };
                    selectedBookmark = null;
                    OnPropertyChanged("SelectedBookmark");
                    //Navigation.PushAsync(new BookmarkPage(tempBookmark, this));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void CreateBookmark()
        {
            //await Navigation.PushAsync(new BookmarkPage(new Bookmark(), this));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }

        public async Task GetBookmarks()
        {
            if (initialized == true) return;
            IsBusy = true;
            IEnumerable<Bookmark> bookmarks = await bookmarkService.Get();

            while (Bookmarks.Any())
                Bookmarks.RemoveAt(Bookmarks.Count - 1);

            foreach (Bookmark f in bookmarks)
                Bookmarks.Add(f);
            IsBusy = false;
            initialized = true;
        }
        private async void SaveBookmark(object bookmarkObject)
        {
            Bookmark bookmark = bookmarkObject as Bookmark;
            if (bookmark != null)
            {
                IsBusy = true;
                // редактирование
                if (bookmark.Id > 0)
                {
                    Bookmark updatedBookmark = await bookmarkService.Update(bookmark);
                    // заменяем объект в списке на новый
                    if (updatedBookmark != null)
                    {
                        int pos = Bookmarks.IndexOf(updatedBookmark);
                        Bookmarks.RemoveAt(pos);
                        Bookmarks.Insert(pos, updatedBookmark);
                    }
                }
                // добавление
                else
                {
                    Bookmark addedBookmark = await bookmarkService.Add(bookmark);
                    if (addedBookmark != null)
                        Bookmarks.Add(addedBookmark);
                }
                IsBusy = false;
            }
            Back();
        }
        private async void DeleteBookmark(object bookmarkObject)
        {
            Bookmark bookmark = bookmarkObject as Bookmark;
            if (bookmark != null)
            {
                IsBusy = true;
                Bookmark deletedBookmark = await bookmarkService.Delete(bookmark.Id);
                if (deletedBookmark != null)
                {
                    Bookmarks.Remove(deletedBookmark);
                }
                IsBusy = false;
            }
            Back();
        }
    }
}
