using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuterizi.Reminder
{
    [BroadcastReceiver(Enabled = true)]

    public class ReminderNotification : BroadcastReceiver
    {
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";
        public ReminderNotification()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var message = intent.Extras.GetString("MESSAGE", "Jangal lupa makan :)");

            // Build the notification:
            var builder = new NotificationCompat.Builder(context, CHANNEL_ID)
                          .SetAutoCancel(true) 
                          .SetContentTitle("Reminder") 
                          .SetSmallIcon(Resource.Drawable.NuteriziIcon) 
                          .SetContentText(message);

            // publish the notification:
            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(NOTIFICATION_ID, builder.Build());
        }
    }
}