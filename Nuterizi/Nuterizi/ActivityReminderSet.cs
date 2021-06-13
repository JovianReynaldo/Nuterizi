using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Nuterizi.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuterizi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ActivityReminderSet : AppCompatActivity
    {
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        private string Mode;
        private int Id;
        private int NotificationId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_reminder_add);

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here

            Button btnReminderCreate = FindViewById<Button>(Resource.Id.btnReminderCreate);
            TimePicker tpReminderTime = FindViewById<TimePicker>(Resource.Id.tpReminderTime);
            EditText etReminderMessage = FindViewById<EditText>(Resource.Id.etReminderMessage);

            Mode = Intent.GetStringExtra("MODE");
            Id = Intent.GetIntExtra("ID", 0);
            NotificationId = Intent.GetIntExtra("NOTIFICATIONID", 0);
            tpReminderTime.Hour = Intent.GetIntExtra("HOUR", tpReminderTime.Hour);
            tpReminderTime.Minute = Intent.GetIntExtra("MINUTE", tpReminderTime.Minute);
            etReminderMessage.Text = Intent.GetStringExtra("MESSAGE");

            btnReminderCreate.Click += (sender, e) =>
            {
                if(etReminderMessage.Text == "")
                {
                    Toast.MakeText(this, "Message is empty, please fill", ToastLength.Short).Show();
                    return;
                }

                DateTime currentDate = DateTime.Now;
                DateTime setDate = new DateTime(
                    currentDate.Year,
                    currentDate.Month, 
                    (currentDate.Hour > tpReminderTime.Hour || (currentDate.Hour == tpReminderTime.Hour && currentDate.Minute >= tpReminderTime.Minute)) 
                    ? currentDate.Day + 1 : currentDate.Day, 
                    tpReminderTime.Hour, 
                    tpReminderTime.Minute, 
                    00
                );

                CreateNotificationChannel();
                createNotification(etReminderMessage.Text, setDate);
            };
        }
        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private void createNotification(string message, DateTime date)
        {
            PendingIntent pendingIntent;
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
            Intent myIntent  = new Intent(this, typeof(ReminderNotification));

            //bundling
            var valuesForActivity = new Bundle();
            valuesForActivity.PutString("MESSAGE", message);
            myIntent.PutExtras(valuesForActivity);

            //convert
            string dateString = $"{date.Month}/{date.Day}/{date.Year} {date.Hour}:{date.Minute}:{date.Second} {date.ToString("tt")}";
            DateTimeOffset dateOffsetValue = DateTimeOffset.Parse(dateString);
            var millisec = dateOffsetValue.ToUnixTimeMilliseconds();

            if(Mode == "EDIT")
            {
                Log.Debug("set", "edit");
                
                // cancel alarm
                pendingIntent = PendingIntent.GetBroadcast(this, NotificationId, myIntent, 0);
                manager.Cancel(pendingIntent);
                
                // set new alarm
                pendingIntent = PendingIntent.GetBroadcast(this, (int)millisec, myIntent, 0);
                manager.SetRepeating(AlarmType.Rtc, millisec, AlarmManager.IntervalDay, pendingIntent);
                ReminderHelper.UpdateRiwayatReminder(Id, (int)millisec, date, message, true);
            } 
            else
            {
                Log.Debug("set", "add");
                pendingIntent = PendingIntent.GetBroadcast(this, (int)millisec, myIntent, 0);
                manager.SetRepeating(AlarmType.Rtc, millisec, AlarmManager.IntervalDay, pendingIntent);
                ReminderHelper.SaveRiwayatReminder((int)millisec, date, message);
            }
            Log.Debug("set", "ok");
            Finish();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}