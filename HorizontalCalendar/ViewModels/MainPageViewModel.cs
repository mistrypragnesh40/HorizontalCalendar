using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HorizontalCalendar.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public MainPageViewModel()
        {
        }

        public ICommand SelectedDateCommand => new Command(() =>
        {
        });

    }
}
