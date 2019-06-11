using Plugin.Connectivity;
using Xamarin.Forms;

namespace Grouping_Sample
{
    public class Utility
    {
        public static bool isSiteIdRestActivated = false;
        public static bool showloading = true;
        public static bool loadGroupOrTopic = false;
        public static bool showGroupTab = false;

        public static bool isAdmin = false;

        public static bool checkLogin = true;
        public static bool rooturlRestActivated = true;

        public static bool addressbookRefresh = true;

        public static bool isNotificationTap = false;

        public static string ADMIN_DOMAIN = "http://54.208.96.132:8090/smartwcm-admin";
        public static string SERVICE_DOMAIN = "http://54.208.96.132:8090/smartwcm-service";
        public static string TICKET = "69f2ed48-6850-4e0f-a2e9-90b54c50fd28";

        //public static string SERVICE_DOMAIN = "http://services.smartwcm.com";
        //public static string ADMIN_DOMAIN = "http://pm-admin.smartwcm.com";
        //public static string TICKET = "ffb09f96-737b-4c2a-bd5e-2f3b40a585a5";

        public static bool IsInternet()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                // write your code if there is no Internet available  
                return false;
            }
        }

        public static bool IsWindowsDevice()
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
