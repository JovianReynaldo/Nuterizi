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

namespace Nuterizi
{
    public static class DB
    {
        private static string dbPath = Path.Combine( System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"nuterizi.db3");
        private static SQLiteConnection db = new SQLiteConnection(dbPath);

        public static RiwayatGizi GetLatestRiwayatHitungGizi()
        {
            db.CreateTable<RiwayatGizi>();
            List<RiwayatGizi> returnList = db.Query<RiwayatGizi>("SELECT * FROM RiwayatHitung ORDER BY _id DESC LIMIT 1");
            Log.Debug("data", returnList.Count.ToString());

            if (returnList.Count != 0)
            {
                Log.Debug("data", returnList[0].Id.ToString());
                return returnList[0];
            } else
            {
                return null;
            }
        }

        public static List<RiwayatGizi> GetRiwayatHitungGizi()
        {
            db.CreateTable<RiwayatGizi>();
            List<RiwayatGizi> returnList = new List<RiwayatGizi>();
            Log.Debug("DBstatus", "Reading Data");
            var table = db.Table<RiwayatGizi>();
            foreach (var s in table)
            {
                returnList.Add(s);
            }
            return returnList;
        }

        public static void SaveRiwayatHitungGizi(DataKebutuhanGizi dataKebutuhanGizi, KebutuhanGizi kebutuhanGizi)
        {
            db.CreateTable<RiwayatGizi>();
            RiwayatGizi data = new RiwayatGizi();
            data.JenisKelamin = dataKebutuhanGizi.JenisKelamin;
            data.Umur = dataKebutuhanGizi.Umur;
            data.TinggiBadan = dataKebutuhanGizi.TinggiBadan;
            data.BeratBadan = dataKebutuhanGizi.BeratBadan;
            data.Frekuensi = dataKebutuhanGizi.Frekuensi;
            data.Energi = kebutuhanGizi.Energi;
            data.Karbohidrat = kebutuhanGizi.Karbohidrat;
            data.Protein = kebutuhanGizi.Protein;
            data.Lemak = kebutuhanGizi.Lemak;
            data.Tanggal = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            db.Insert(data);
        }

        public static void EmptyRiwayatGizi()
        {
            db.CreateTable<RiwayatGizi>();
            db.Execute("delete from RiwayatHitung");
        }
    }

    [Table("RiwayatHitung")]
    public class RiwayatGizi
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string JenisKelamin { get; set; }
        public int Umur { get; set; }
        public int TinggiBadan { get; set; }
        public int BeratBadan { get; set; }
        public string Frekuensi { get; set; }
        public int Energi { get; set; }
        public int Karbohidrat { get; set; }
        public int Protein { get; set; }
        public int Lemak { get; set; }
        public string Tanggal {get; set;}
    }
}