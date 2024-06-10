using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public class PermissionsHelper
    {
        public static async Task<PermissionStatus> CheckAndRequestStorageReadPermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            Debug.WriteLine($"Initial Read Permission Status: {status}");
            if (status != PermissionStatus.Granted)
            {
                if (DeviceInfo.Version.Major >= 11)
                {
                    // Android 11 and above
                    status = await RequestManageExternalStoragePermissionAsync();
                }
                else
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
                Debug.WriteLine($"Requested Read Permission Status: {status}");
            }
            return status;
        }

        public static async Task<PermissionStatus> CheckAndRequestStorageWritePermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            Debug.WriteLine($"Initial Write Permission Status: {status}");
            if (status != PermissionStatus.Granted)
            {
                if (DeviceInfo.Version.Major >= 11)
                {
                    // Android 11 and above
                    status = await RequestManageExternalStoragePermissionAsync();
                }
                else
                {
                    status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }
                Debug.WriteLine($"Requested Write Permission Status: {status}");
            }
            return status;
        }



        public static async Task<bool> EnsureStoragePermissionsAsync()
        {
            var readStatus = await CheckAndRequestStorageReadPermissionAsync();
            var writeStatus = await CheckAndRequestStorageWritePermissionAsync();

            Debug.WriteLine($"Read Permission Status: {readStatus}");
            Debug.WriteLine($"Write Permission Status: {writeStatus}");

            if (readStatus != PermissionStatus.Granted || writeStatus != PermissionStatus.Granted)
            {
                var currentPage = GetCurrentPage();
                if (currentPage != null)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await currentPage.DisplayAlert("Permission Denied", "Cannot continue without granting permission.", "OK");
                    });
                }
                return false;
            }
            return true;
        }

        private static Page GetCurrentPage()
        {
            var mainPage = Application.Current.MainPage;
            if (mainPage is NavigationPage navigationPage)
            {
                return navigationPage.CurrentPage;
            }
            if (mainPage is TabbedPage tabbedPage)
            {
                return tabbedPage.CurrentPage;
            }
            if (mainPage is MasterDetailPage masterDetailPage)
            {
                return masterDetailPage.Detail;
            }
            return mainPage;
        }
        public static async Task<PermissionStatus> RequestManageExternalStoragePermissionAsync()
        {
            if (DeviceInfo.Version.Major >= 11)
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    // Redirect user to settings to manually enable the permission
                    var result = await Application.Current.MainPage.DisplayAlert("Permission required",
                        "To access all files, please enable the 'All files access' permission in the settings.", "Settings", "Cancel");
                    if (result)
                    {
                        // Open settings to manually enable permission
                        AppInfo.ShowSettingsUI();
                    }
                    return PermissionStatus.Denied;
                }
                return status;
            }
            else
            {
                return await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
        }

    }
}