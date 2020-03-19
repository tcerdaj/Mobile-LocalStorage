using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using LocalDatabase.Mobile.Models;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace LocalDatabase.Mobile.Utilities
{
    public static class Utils
    {
        public static async Task<MediaFile> TakePhoto()
        {
            MediaFile photo = null;

            var action =
                        await UserDialogs.Instance.ActionSheetAsync("Select Image Source", "Cancel", null, null, "Gallery",
                            "Camera");

            if (action == null)
            {
                return null;
            }

            try
            {
                PermissionStatus status;

                if (action == "Gallery")
                {
                    status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                    if (status != PermissionStatus.Granted)
                    {
                        var _status = await CrossPermissions.Current.RequestPermissionsAsync(new Permission[] { Permission.Photos });
                        status = _status.Values.FirstOrDefault();
                    }
                }
                else
                {
                    status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    if (status != PermissionStatus.Granted)
                    {
                        var _status = await CrossPermissions.Current.RequestPermissionsAsync(new Permission[] { Permission.Camera });
                        status = _status.Values.FirstOrDefault();
                    }
                }

                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                if (storageStatus != PermissionStatus.Granted)
                {
                    var _storageStatus = await CrossPermissions.Current.RequestPermissionsAsync(new Permission[] { Permission.Storage });
                    storageStatus = _storageStatus.Values.FirstOrDefault();
                }

                if (status == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
                {
                    if (action == "Gallery")
                    {
                        photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions() { PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium, SaveMetaData = false, MaxWidthHeight = 400 });
                    }
                    else
                    {
                        photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { SaveToAlbum = true, PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium, SaveMetaData = false, MaxWidthHeight = 400 });
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                UserDialogs.Instance.Alert($"Unable to take photo, {e.Message}.", "Take Photo");
            }

            return photo;
        }
    }
}
