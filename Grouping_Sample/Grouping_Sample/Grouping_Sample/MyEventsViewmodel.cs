using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Grouping_Sample
{
    class MyEventsViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CalendarEvents myevents;
        public ObservableCollection<EventsHB> Items = new ObservableCollection<EventsHB>();
        private ObservableCollection<EventsHB> _allItems;

        //Update: add a new tempItem to get json data
        private ObservableCollection<EventsHBTwo> _tempItems;


        public bool haveItemsToLoad = true;
        public bool loadMore = false;
        public bool isLoading;
        public int i = 1;
        public string eventType;

        public ObservableCollection<EventsHB> AllItems
        {
            get
            {
                return _allItems;
            }
            set
            {
                _allItems = value;
                OnPropertyChanged("AllItems");
            }
        }

        //Update: add a new tempItem to get json data
        public ObservableCollection<EventsHBTwo> tempItem
        {
            get
            {
                return _tempItems;
            }
            set
            {
                _tempItems = value;
                OnPropertyChanged("tempItem");
            }
        }

        public ICommand LoadMoreCommand
        {
            get
            {
                return new Command(() =>
                {
                    loadMore = true;
                    MyEventsList();
                });
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    i = 1;
                    haveItemsToLoad = true;
                    Utility.showloading = true;
                    MyEventsList();
                    IsRefreshing = false;
                });
            }
        }

        public async void MyEventsList()
        {
            if (Utility.IsInternet())
            {
                int countInInt = i * 20;
                try
                {
                    HttpClient client = new HttpClient();
                    var Response = await client.GetAsync("http://54.208.96.132:8090/smartwcm-admin/rest/v3/calendar/list-active-events-app/applicationid/32/siteid/45/entityid/6552/entitytype/edu-school-calendar/count/20/type/all?swcmTicket=69f2ed48-6850-4e0f-a2e9-90b54c50fd28");
                    if (Response.IsSuccessStatusCode)
                    {
                        string response = await Response.Content.ReadAsStringAsync();
                        Debug.WriteLine("response:>>" + response);
                
                        myevents = new CalendarEvents();
                        if (response != "")
                        {
                            
                            myevents = JsonConvert.DeserializeObject<CalendarEvents>(response.ToString());
                 
                        }
                        tempItem = new ObservableCollection<EventsHBTwo>(myevents.eventsHB);

                        //update: converData here 
                        converData(tempItem);

                        int listCount = myevents.eventsHB.Count;
                        Debug.WriteLine("listCount:>>" + listCount);
                        Debug.WriteLine("countInInt:>>" + countInInt);
                        if (countInInt != listCount)
                        {
                            haveItemsToLoad = false;
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Something went wrong at the server, please try again later.", "Ok");
                        });
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception:>" + ex);
                }
            }
            else
            {
                if (!Utility.IsWindowsDevice())
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "No Internet Connection.", "Ok");
                }
                else
                {
                    ShowAlert("No Internet Connection.");
                }
            }
        }

        public void ShowAlert(string message)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await Application.Current.MainPage.DisplayAlert("Alert", message, "Ok");
            });
        }

        public void converData(ObservableCollection<EventsHBTwo> allItem)
        {

            AllItems = new ObservableCollection<EventsHB>();

            EventsHB julyGroup = new EventsHB() { month = "July" };
            EventsHB juneGroup = new EventsHB() { month = "June" };

            foreach (var item in allItem)
            {
                EventsHBTwo hb = item;
                if (hb.month == "July")
                {
                    julyGroup.Add(hb.eventTO);
                }
                else if (hb.month == "June")
                {
                    juneGroup.Add(hb.eventTO);
                }
            }

            //at last, add them to All items.
            AllItems.Add(julyGroup);
            AllItems.Add(juneGroup);
        }
    }
}
