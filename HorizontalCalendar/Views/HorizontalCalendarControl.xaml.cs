using HorizontalCalendar.Models;
using HorizontalCalendar.ViewModels;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace HorizontalCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HorizontalCalendarControl : StackLayout
    {
        #region Binding Calendar Property
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
        private string _selectedDateInString;
        public string SelectedDateInString
        {
            get => _selectedDateInString;
            set => SetProperty(ref _selectedDateInString, value);
        }
        #endregion



        #region UI Customization Property Properties
        public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(
        propertyName: nameof(HeaderBackgroundColor),
        returnType: typeof(Color),
        declaringType: typeof(HorizontalCalendarControl),
        defaultValue: Color.Orange,
        defaultBindingMode: BindingMode.OneWay
        );
        public Color HeaderBackgroundColor
        {
            get => (Color)base.GetValue(HeaderBackgroundColorProperty);
            set => base.SetValue(HeaderBackgroundColorProperty, value);
        }

        public static readonly BindableProperty HeaderTextColorProperty = BindableProperty.Create(
        propertyName: nameof(HeaderTextColor),
        returnType: typeof(Color),
        declaringType: typeof(HorizontalCalendarControl),
        defaultValue: Color.White,
        defaultBindingMode: BindingMode.OneWay
        );
        public Color HeaderTextColor
        {
            get => (Color)base.GetValue(HeaderTextColorProperty);
            set => base.SetValue(HeaderTextColorProperty, value);
        }

        public static readonly BindableProperty LeftRightArrowColorProperty = BindableProperty.Create(
           propertyName: nameof(LeftRightArrowColor),
           returnType: typeof(Color),
           declaringType: typeof(HorizontalCalendarControl),
           defaultValue: Color.Black,
           defaultBindingMode: BindingMode.OneWay
           );
        public Color LeftRightArrowColor
        {
            get => (Color)base.GetValue(LeftRightArrowColorProperty);
            set => base.SetValue(LeftRightArrowColorProperty, value);
        }


        public static readonly BindableProperty SelectedDateBackGroundColorProperty = BindableProperty.Create(
          propertyName: nameof(SelectedDateBackGroundColor),
          returnType: typeof(Color),
          declaringType: typeof(HorizontalCalendarControl),
          defaultValue: Color.Green,
          defaultBindingMode: BindingMode.OneWay
          );
        public Color SelectedDateBackGroundColor
        {
            get => (Color)base.GetValue(SelectedDateBackGroundColorProperty);
            set => base.SetValue(SelectedDateBackGroundColorProperty, value);
        }


        public static readonly BindableProperty SelectedDateTextColorProperty = BindableProperty.Create(
        propertyName: nameof(SelectedDateTextColor),
        returnType: typeof(Color),
        declaringType: typeof(HorizontalCalendarControl),
        defaultValue: Color.White,
        defaultBindingMode: BindingMode.OneWay
        );
        public Color SelectedDateTextColor
        {
            get => (Color)base.GetValue(SelectedDateTextColorProperty);
            set => base.SetValue(SelectedDateTextColorProperty, value);
        }




        #endregion

        #region Selection Date Property

        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
        nameof(SelectedDate),
        typeof(DateTime?),
        typeof(HorizontalCalendarControl),
        DateTime.Now,
        BindingMode.TwoWay, propertyChanged: SelectedDatePropertyChanged);

        private static void SelectedDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var currentControls = (HorizontalCalendarControl)bindable;

            if (newValue != null)
            {
                var date = (DateTime)newValue;
                currentControls.BindDates(date);
            }
        }

        public DateTime? SelectedDate
        {
            get => (DateTime)base.GetValue(SelectedDateProperty);
            set => base.SetValue(SelectedDateProperty, value);
        }
        #endregion

        #region Constructor
        public HorizontalCalendarControl()
        {
            InitializeComponent();
            MaxYear = DateTime.Now.Year + 30;
            MinYear = DateTime.Now.Year - 100;
            BindDates(DateTime.Now);
        }
        #endregion
        #region Methods
        public void BindDates(DateTime selectedDate)
        {
            int year = selectedDate.Year;
            int month = selectedDate.Month;
            string MonthName = selectedDate.ToString("MMM");

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

            var currentDate = dates.Where(f => f.Date.Date == SelectedDate.Value.Date).FirstOrDefault();
            if (currentDate != null)
            {
                CurrentDate = currentDate.Date;
                currentDate.CurrentDate = true;
                SelectedDate = CurrentDate;
                SelectedDateInString = CurrentDate.ToString("dddd, dd MMMM");
            }
            CalendarList.ReplaceRange(dates);
        }


        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "",
         Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
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
                            BindDates(CurrentDate);
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
                            BindDates(CurrentDate);
                        }
                    }
                });
            }
        }
        #endregion
    }
}