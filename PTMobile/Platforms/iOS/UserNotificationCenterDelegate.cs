using CommunityToolkit.Mvvm.Messaging;
using ObjCRuntime;
using PTMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications;

namespace PTMobile.Platforms.iOS
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate 
    {
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userinfo = response.Notification.Request.Content.UserInfo;
            string navigationID = userinfo["NavigationID"].ToString();

            WeakReferenceMessenger.Default.Send(new PushNotificationReceived("test"));
        }
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert);
        }
    }
}
