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
            SelectedDate = DateTime.Now.AddDays(10);
        }

        public ICommand SelectedDateCommand => new Command<DateTime>((selectedDate) =>
        {
            SelectedDate = selectedDate; 
        });

    }
}
