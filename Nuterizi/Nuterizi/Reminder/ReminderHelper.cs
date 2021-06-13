using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nuterizi.Reminder
{
    class ReminderHelper
    {
        private static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "nuterizi.db3");
        private static SQLiteConnection db = new SQLiteConnection(dbPath);

        public static List<RiwayatReminder> GetReminderList()
        {
            db.CreateTable<RiwayatReminder>();
            List<RiwayatReminder> returnList = new List<RiwayatReminder>();
            var table = db.Table<RiwayatReminder>().OrderBy(xx => xx.Time);

            foreach (var s in table)
            {
                returnList.Add(s);
            }

            return returnList;
        }

        public static void SaveRiwayatReminder(int notificationId, DateTime notificationTime, string notificationMessage)
        {
            db.CreateTable<RiwayatReminder>();

            // set data
            RiwayatReminder data = new RiwayatReminder();
            data.NotificationID = notificationId;
            data.Time = notificationTime;
            data.Message = notificationMessage;
            data.Active = true;

            // insert data
            db.Insert(data);
        }
        public static void SwitchRiwayatReminder(int id, bool status)
        {
            db.CreateTable<RiwayatReminder>();

            // update data
            db.Query<RiwayatGizi>($"UPDATE RiwayatReminder SET Active = '{status}' " +
                $"WHERE _id = {id}");
        }
        public static void UpdateRiwayatReminder(int id, int notificationId, DateTime notificationTime, string notificationMessage, bool status)
        {
            db.CreateTable<RiwayatReminder>();

            // set data
            RiwayatReminder data = new RiwayatReminder();
            data.Id = id;
            data.NotificationID = notificationId;
            data.Time = notificationTime;
            data.Message = notificationMessage;
            data.Active = status;

            // update data
            db.Update(data);
        }

        public static void DeleteReminder(int notifId)
        {
            db.CreateTable<RiwayatReminder>();

            // delete data
            db.Query<RiwayatGizi>($"DELETE FROM RiwayatReminder WHERE _id = {notifId}");
        }
    }


    [Table("RiwayatReminder")]
    public class RiwayatReminder
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public int NotificationID { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public bool Active { get; set; }
    }
}