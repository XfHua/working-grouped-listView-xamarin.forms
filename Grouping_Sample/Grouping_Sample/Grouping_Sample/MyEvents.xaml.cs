using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grouping_Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyEvents : ContentPage
    {
        MyEventsViewmodel mevm;
        public MyEvents()
        {
            InitializeComponent();
            mevm = new MyEventsViewmodel();
            BindingContext = mevm;

            Upcoming_label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    MyEventsListview.ScrollTo(((IList)MyEventsListview.ItemsSource)[0], ScrollToPosition.Start, false);
                    Upcoming_layout.BackgroundColor = Color.FromHex("#0091da");
                    All_layout.BackgroundColor = Color.FromHex("#FFFFFF");
                    Upcoming_label.TextColor = Color.FromHex("FFFFFF");
                    allevents_label.TextColor = Color.FromHex("aee4ff");
                    mevm.eventType = "upcoming";
                    mevm.i = 1;
                    mevm.haveItemsToLoad = true;
                    Utility.showloading = true;
                    events_layout.RaiseChild(Upcoming_layout);
                    mevm.MyEventsList();
                })
            });

            allevents_label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    MyEventsListview.ScrollTo(((IList)MyEventsListview.ItemsSource)[0], ScrollToPosition.Start, false);
                    Upcoming_layout.BackgroundColor = Color.FromHex("#FFFFFF");
                    All_layout.BackgroundColor = Color.FromHex("#0091da");
                    Upcoming_label.TextColor = Color.FromHex("aee4ff");
                    allevents_label.TextColor = Color.FromHex("FFFFFF");
                    mevm.eventType = "all";
                    mevm.i = 1;
                    mevm.haveItemsToLoad = true;
                    Utility.showloading = true;
                    events_layout.RaiseChild(All_layout);
                    mevm.MyEventsList();
                })
            });

            MyEventsListview.ItemAppearing += (sender, e) =>
            {
                if (mevm.isLoading || mevm.AllItems != null && mevm.AllItems.Count == 0)
                    return;

                //hit bottom
                if (mevm.AllItems != null && mevm.AllItems.Count > 0)
                {
                    try
                    {
                        EventsHB item = mevm.AllItems[mevm.AllItems.Count - 1];
                        if ((e.Item as EventsHB).eventTO.calendarEventId.ToString() == item.eventTO.calendarEventId.ToString())
                        {
                            mevm.loadMore = true;
                            if (mevm.haveItemsToLoad)
                            {
                                Debug.WriteLine("Enter bottom");
                                mevm.i = mevm.i + 1;
                                Utility.showloading = false;
                                mevm.LoadMoreCommand.Execute(null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception:>" + ex);
                    }
                }
            };
        }

        protected override void OnAppearing()
        {
            Utility.showloading = true;
            mevm.eventType = "upcoming";
            mevm.MyEventsList();
        }
    }
}