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

namespace Nuterizi
{
    public class DataKebutuhanGizi
    {
        public string JenisKelamin { get; set; }
        public int Umur { get; set; }
        public int TinggiBadan { get; set; }
        public int BeratBadan { get; set; }
        public string Frekuensi { get; set; }
        public int TotalKebutuhan { get; set; }
    }
}