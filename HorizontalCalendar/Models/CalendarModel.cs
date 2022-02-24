using HorizontalCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizontalCalendar.Models
{
    internal class CalendarModel : BaseViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string DayName { get; set; }
        public int DateInNumber { get; set; }

        private bool _currentDate;
        public bool CurrentDate
        {
            get => _currentDate; 
            set=> SetProperty(ref _currentDate, value);
        }
        public DateTime Date { get; set; }

    }
}
