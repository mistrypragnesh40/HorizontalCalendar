using HorizontalCalendar.Models;
using HorizontalCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorizontalCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarView : StackLayout, INotifyPropertyChanged
    {
        #region Properties
        public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(
        propertyName: nameof(HeaderBackgroundColor),
        returnType: typeof(Color),
        declaringType: typeof(CalendarView),
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
        declaringType: typeof(CalendarView),
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
           declaringType: typeof(CalendarView),
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
          declaringType: typeof(CalendarView),
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
        declaringType: typeof(CalendarView),
        defaultValue: Color.White,
        defaultBindingMode: BindingMode.OneWay
        );
        public Color SelectedDateTextColor
        {
            get => (Color)base.GetValue(SelectedDateTextColorProperty);
            set => base.SetValue(SelectedDateTextColorProperty, value);
        }



        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
            nameof(SelectedDate), 
            typeof(DateTime?),
            typeof(CalendarView),
            DateTime.Now,
            BindingMode.TwoWay);

        public DateTime? SelectedDate
        {
            get
            {
                return (DateTime?)GetValue(SelectedDateProperty);
            }
            set
            {
                SetValue(SelectedDateProperty, value);
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        #endregion



        #region Constructor
        public CalendarView()
        {
            InitializeComponent();
            this.BindingContext = new CalendarViewModel(this); ;
        }
        #endregion



        #region Methods
        protected bool SetProperty<T>(ref T backingStore, T value,
          [CallerMemberName] string propertyName = "",
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
    }
}