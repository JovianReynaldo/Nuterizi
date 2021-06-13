using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;
using TaskStackBuilder = Android.App.TaskStackBuilder;
using Android.Support.V4.App;
using Nuterizi.Reminder;
using Android.Util;
using AndroidX.RecyclerView.Widget;
using Nuterizi.Adapter;

namespace Nuterizi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ActivityReminder : AppCompatActivity
    {
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_reminder);

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            Button btnReminderAdd = FindViewById<Button>(Resource.Id.btnReminderAdd);

            btnReminderAdd.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(ActivityReminderSet));
                StartActivity(intent);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            // fetch latest data
            List<RiwayatReminder> data = ReminderHelper.GetReminderList();

            // update data
            RecyclerView rvRiwayatReminder = FindViewById<RecyclerView>(Resource.Id.rvRiwayatReminder);
            ReminderAdapter mAdapter = new ReminderAdapter(data);
            LinearLayoutManager mLayoutManager = new LinearLayoutManager(this);
            rvRiwayatReminder.SetLayoutManager(mLayoutManager);
            rvRiwayatReminder.SetAdapter(mAdapter);
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