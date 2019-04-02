using ContactMvvmApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactMvvmApp.ViewModels
{
    public class PeopleListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PersonViewModel> People { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateFriendCommand { protected set; get; }
        public ICommand DeleteFriendCommand { protected set; get; }
        public ICommand SaveFriendCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        PersonViewModel selectedFriend;

        public INavigation Navigation { get; set; }

        public PeopleListViewModel()
        {
            People = new ObservableCollection<PersonViewModel>();
            CreateFriendCommand = new Command(CreateFriend);
            DeleteFriendCommand = new Command(DeleteFriend);
            SaveFriendCommand = new Command(SaveFriend);
            BackCommand = new Command(Back);
        }

        public PersonViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (selectedFriend != value)
                {
                    PersonViewModel tempFriend = value;
                    selectedFriend = null;
                    OnPropertyChanged("SelectedFriend");
                    Navigation.PushAsync(new PersonPage(tempFriend));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateFriend()
        {
            Navigation.PushAsync(new PersonPage(new PersonViewModel() { ListViewModel = this }));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SaveFriend(object friendObject)
        {
            PersonViewModel friend = friendObject as PersonViewModel;
            if (friend != null && friend.IsValid)
            {
                People.Add(friend);
            }
            Back();
        }
        private void DeleteFriend(object friendObject)
        {
            PersonViewModel friend = friendObject as PersonViewModel;
            if (friend != null)
            {
                People.Remove(friend);
            }
            Back();
        }
    }
}