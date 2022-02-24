using HorizontalCalendar.Models;
using HorizontalCalendar.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace HorizontalCalendar.ViewModels
{
    internal class CalendarViewModel : BaseViewModel
    {
        #region Properties & Event
        int MaxYear;
        int MinYear;
        public ObservableRangeCollection<CalendarModel> CalendarList { get; } = new ObservableRangeCollection<CalendarModel>();

        private string _currentMonthYear = string.Empty;
        public string CurrentMonthYear
        {
            get => _currentMonthYear;
            set => SetProperty(ref _currentMonthYear, value);
        }

        private DateTime _currentDate;
        public DateTime CurrentDate
        {
            get => _currentDate;
            set => SetProperty(ref _currentDate, value);
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value, onChanged: () =>
            {
                _uiRef.SelectedDate = SelectedDate.Value;
            });
        }

        private string _selectedDateInString;
        public string SelectedDateInString
        {
            get => _selectedDateInString;
            set => SetProperty(ref _selectedDateInString, value);
        }
        private CalendarView _uiRef;
        #endregion

        #region Constructor
        public CalendarViewModel(CalendarView uiRef)
        {
            MaxYear = DateTime.Now.Year + 30;
            MinYear = DateTime.Now.Year - 100;
            _uiRef = uiRef;
            var selectedDate = DateTime.Now;
            BindDates(selectedDate.Year, selectedDate.Month, selectedDate.ToString("MMM"), selectedDate);
        }
        #endregion

        #region Methods
        public void BindDates(int year, int month, string MonthName, DateTime? newDate = null)
        {
            int daysCount = DateTime.DaysInMonth(year, month);
            CurrentMonthYear = MonthName + " - " + year.ToString();
            var dates = new List<CalendarModel>();
            for (int days = 1; days <= daysCount; days++)
            {
                DateTime date = new DateTime(year, month, days);
                CalendarModel obj = new CalendarModel();
                obj.Date = date;
                obj.Year = year;
                obj.Month = month;
                obj.DateInNumber = days;
                obj.DayName = date.ToString("ddd");
                dates.Add(obj);
            }
            dates.AddRange(dates);
            if (newDate.HasValue)
            {
                var currentDate = dates.Where(f => f.DateInNumber == newDate?.Day).FirstOrDefault();
                currentDate.CurrentDate = true;
                SelectedDate = newDate.Value;
                CurrentDate = newDate.Value;
                SelectedDateInString = CurrentDate.ToString("ddd, MMMM dd");
            }
            else
            {
                var date = dates.Where(f => f.Date.Date == SelectedDate.Value.Date).FirstOrDefault();
                if (date != null)
                {
                    date.CurrentDate = true;
                }
            }
            CalendarList.ReplaceRange(dates);
        }
        #endregion

        #region Commands
        // used for Traverse through date
        public ICommand CurrentDateCommand
        {
            get
            {
                return new Command<CalendarModel>((item) =>
                {
                    if (item != null)
                    {
                        CalendarList.ForEach(f => { f.CurrentDate = false; });
                        CurrentDate = item.Date;
                        item.CurrentDate = true;
                        SelectedDate = CurrentDate;
                        SelectedDateInString = item.Date.ToString("dddd, dd MMMM");
                    }

                });
            }
        }


        //Executed on the Tap of > Sign.
        public ICommand NextMonthCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CurrentDate != null)
                    {
                        bool isMaxDateReach = false;
                        if (MaxYear == CurrentDate.Year && CurrentDate.Month == 12)
                        {
                            isMaxDateReach = true;
                        }

                        if (!isMaxDateReach)
                        {
                            CurrentDate = CurrentDate.Date.AddMonths(1);
                            BindDates(CurrentDate.Year, CurrentDate.Month, CurrentDate.ToString("MMM"));
                        }

                    }
                });
            }
        }

        //Executed on the Tap of < Sign.
        public ICommand PreviousMonthCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (CurrentDate != null)
                    {
                        bool isMinDateReach = false;
                        if (MinYear == CurrentDate.Year && CurrentDate.Month == 1)
                        {
                            isMinDateReach = true;
                        }
                        if (!isMinDateReach)
                        {
                            CurrentDate = CurrentDate.Date.AddMonths(-1);
                            BindDates(CurrentDate.Year, CurrentDate.Month, CurrentDate.ToString("MMM"));
                        }
                    }
                });
            }
        }
        #endregion
    }
}
