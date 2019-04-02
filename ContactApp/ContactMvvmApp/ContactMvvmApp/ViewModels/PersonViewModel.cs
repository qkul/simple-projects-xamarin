using ContactMvvmApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ContactMvvmApp.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        PeopleListViewModel _peopleListViewModel;
        public Person Person { get; private set; }
        public PersonViewModel()
        {
            Person = new Person();
        }
        public PeopleListViewModel ListViewModel
        {
            get { return _peopleListViewModel; }
            set
            {
                if (_peopleListViewModel != value)
                {
                    _peopleListViewModel = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public string Name
        {
            get { return Person.Name; }
            set
            {
                if (Person.Name != value)
                {
                    Person.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Email
        {
            get { return Person.Email; }
            set
            {
                if (Person.Email != value)
                {
                    Person.Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string Phone
        {
            get { return Person.Phone; }
            set
            {
                if (Person.Phone != value)
                {
                    Person.Phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Name.Trim())) ||
                    (!string.IsNullOrEmpty(Phone.Trim())) ||
                    (!string.IsNullOrEmpty(Email.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    } 
}
