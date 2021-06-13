using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Nuterizi.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nuterizi.Adapter
{
    class ReminderAdapter : RecyclerView.Adapter
    {
        public List<RiwayatReminder> riwayatReminder;

        public ReminderAdapter(List<RiwayatReminder> data)
        {
            riwayatReminder = data;
        }
        public override int ItemCount
        {
            get { return riwayatReminder.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RiwayatReminderAdapterViewHolder vh = holder as RiwayatReminderAdapterViewHolder;

            DateTime time = riwayatReminder[position].Time;
            vh.tvReminderTime.Text = $"{time.Hour}:{time.Minute} {time.ToString("tt")}";
            vh.tvReminderMessage.Text = riwayatReminder[position].Message.ToString();
            vh.swReminderSwitch.Checked = riwayatReminder[position].Active;

            // add listener
            vh.swReminderSwitch.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e) { switch_itemSelected(sender, e, vh.ItemView.Context, position); };
            vh.spReminderAction.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>((sender, e) => spinner_ItemSelected(sender, e, vh.ItemView.Context, position));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.activity_reminder_item, parent, false);
            RiwayatReminderAdapterViewHolder vh = new RiwayatReminderAdapterViewHolder(itemView);

            return vh;
        }
        private void switch_itemSelected(object sender, CompoundButton.CheckedChangeEventArgs e, Context context, int position)
        {
            AlarmManager manager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            Intent myIntent = new Intent(context, typeof(ReminderNotification));
            if (e.IsChecked)
            {
                //set data
                var valuesForActivity = new Bundle();
                valuesForActivity.PutString("MESSAGE", riwayatReminder[position].Message);
                myIntent.PutExtras(valuesForActivity);

                DateTime date = riwayatReminder[position].Time;
                string dateString = $"{date.Month}/{date.Day}/{date.Year} {date.Hour}:{date.Minute}:{date.Second} {date.ToString("tt")}";
                DateTimeOffset dateOffsetValue = DateTimeOffset.Parse(dateString);
                var millisec = dateOffsetValue.ToUnixTimeMilliseconds();

                // set alarm
                PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, (int)millisec, myIntent, 0);
                manager.SetRepeating(AlarmType.Rtc, millisec, AlarmManager.IntervalDay, pendingIntent);
            }
            else
            {
                // cancel alarm
                PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, riwayatReminder[position].NotificationID, myIntent, 0);
                manager.Cancel(pendingIntent);
            }
            ReminderHelper.SwitchRiwayatReminder(riwayatReminder[position].Id, e.IsChecked);
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e, Context context, int position)
        {
            Spinner spinner = (Spinner)sender;

            if (e.Position == 0)
            {
                // edit
                Intent activityReminderSet = new Intent(context, typeof(ActivityReminderSet));

                // set data
                var valuesForActivity = new Bundle();
                valuesForActivity.PutString("MODE", "EDIT");
                valuesForActivity.PutInt("HOUR", riwayatReminder[position].Time.Hour);
                valuesForActivity.PutInt("MINUTE", riwayatReminder[position].Time.Minute);
                valuesForActivity.PutInt("ID", riwayatReminder[position].Id);
                valuesForActivity.PutInt("NOTIFICATIONID", riwayatReminder[position].NotificationID);
                valuesForActivity.PutString("MESSAGE", riwayatReminder[position].Message);

                // start activity with data
                activityReminderSet.PutExtras(valuesForActivity);
                context.StartActivity(activityReminderSet);
            }

            if (e.Position == 1 && riwayatReminder.Count > 0)
            {
                // delete - cancel
                AlarmManager manager = (AlarmManager)context.GetSystemService(Context.AlarmService);
                Intent myIntent = new Intent(context, typeof(ReminderNotification));
                PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, riwayatReminder[position].NotificationID, myIntent, 0);
                manager.Cancel(pendingIntent);

                // delete - db
                ReminderHelper.DeleteReminder(riwayatReminder[position].Id);

                // refresh data
                riwayatReminder.Clear();
                NotifyDataSetChanged();
                RefreshData();
                spinner.SetSelection(2);
            }

        }

        private async void RefreshData()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.3));
            riwayatReminder = ReminderHelper.GetReminderList();
            NotifyDataSetChanged();
        }

    }

    class RiwayatReminderAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView tvReminderTime { get; set; }
        public TextView tvReminderMessage { get; set; }
        public Spinner spReminderAction { get; set; }
        public Switch swReminderSwitch { get; set; }

        public RiwayatReminderAdapterViewHolder(View itemView) : base(itemView)
        {
            tvReminderTime = itemView.FindViewById<TextView>(Resource.Id.tvReminderTime);
            tvReminderMessage = itemView.FindViewById<TextView>(Resource.Id.tvReminderMessage);
            swReminderSwitch = itemView.FindViewById<Switch>(Resource.Id.swReminderSwitch);
            spReminderAction = itemView.FindViewById<Spinner>(Resource.Id.spReminderAction);

            // Spinner Option
            var list = new List<String>();
            list.Add("Edit Reminder");
            list.Add("Delete Reminder");
            list.Add("");

            int hidingItemIndex = 0;

            SpinnerAdapter dataAdapter = new SpinnerAdapter(ItemView.Context, Android.Resource.Layout.SimpleSpinnerItem, list, hidingItemIndex);
            dataAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spReminderAction.Adapter = dataAdapter;
            spReminderAction.SetSelection(2);
        }
    }

    public class SpinnerAdapter : ArrayAdapter<String>    {
        private int itemCount;
        public override int Count
        {
            get { return itemCount; }
        }

        public SpinnerAdapter(Context context, int textViewResourceId, List<String> objects, int hidingItemIndex) : base(context, textViewResourceId, objects)
        {
            itemCount = objects.Count - 1;
        }
    }
}