using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HorizontalCalendar.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
        }

        public ICommand LoadMoreCommand => new Command(() =>
           {
           });

    }
}
