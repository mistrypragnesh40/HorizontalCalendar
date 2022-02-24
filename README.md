# Horizontal Calendar Control
Horizontal Calendar Control - is a cross-platform plugin for Xamairn Forms that allows you to display a customized calendars in your app.  

![image](https://user-images.githubusercontent.com/47309472/155514027-81443347-e4aa-4915-b1f2-4c8e14de4280.png)

## How To Use
Nuget: https://www.nuget.org/packages/HorizontalCalendarControl/1.0.1  

Install this plugin in your Xamarin Form Project.  

Namespace : 

```
xmlns:views="clr-namespace:HorizontalCalendar.Views;assembly=HorizontalCalendar"
```

Write Following Code In Your XAML Page.
```
 <views:HorizontalCalendarControl LeftRightArrowColor="White"   />
```

## UI Customization
![image](https://user-images.githubusercontent.com/47309472/155515110-0f1d47ad-8cbf-43dd-843d-2cb1a0133d31.png)  

Following Property, you can use for customizing Calendar UI  
## Property
1. HeaderBackgroundColor : Used to set Header Background color.
2. HeaderTextColor : Used to Set Header Text color.
3. SelectedDateTextColor : Used to Set Selected Date Text Color.
4. SelectedDateBackGroundColor : Used to Set Selected Date Background Color.
5. LeftRightArrowColor : Used to set left and right arrow color.
6. SelectedDate : Used to return Selected Date.

```
<views:HorizontalCalendarControl 
      HeaderBackgroundColor="LightBlue"
      HeaderTextColor="Black"
      SelectedDateTextColor="LightBlue" 
      SelectedDateBackGroundColor="Black" 
      LeftRightArrowColor="Black"   />
```

## How To Get Selected Date  

Write Following Code to Get Selected Date  
```
    <Label Text="{Binding Source={x:Reference calendarControl},Path=SelectedDate}" />
    <views:HorizontalCalendarControl  x:Name="calendarControl"/>
```
## Get Date On Button Click Event (MVVM)
```
    <views:HorizontalCalendarControl   x:Name="calendarControl"  />
    <Button Text="Get Selected Date" Command="{Binding GetDateCommand}" 
            CommandParameter="{Binding Source={x:Reference calendarControl},Path=SelectedDate}"  />
```
