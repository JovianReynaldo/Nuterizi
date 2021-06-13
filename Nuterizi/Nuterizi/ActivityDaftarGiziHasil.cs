using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuterizi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ActivityDaftarGiziHasil : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_daftar_gizi_hasil);

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            string query = Intent.GetStringExtra("query");
            Makanan resultList = ApiClient.GetDetailMakanan(query);

            TextView tvNamaMakanan = FindViewById<TextView>(Resource.Id.tvNamaMakanan);
            TextView tvSajian = FindViewById<TextView>(Resource.Id.tvSajian);
            TextView tvKalori = FindViewById<TextView>(Resource.Id.tvKalori);
            TextView tvKarbohidrat = FindViewById<TextView>(Resource.Id.tvKarbohidrat);
            TextView tvProtein = FindViewById<TextView>(Resource.Id.tvProtein);
            TextView tvLemak = FindViewById<TextView>(Resource.Id.tvLemak);
            
            tvNamaMakanan.Text = resultList.nama.ToString() + " kal";
            tvSajian.Text = "Jumlah sajian per " + resultList.sajian.ToString() + " g";
            tvKalori.Text = resultList.kalori.ToString() + " g";
            tvKarbohidrat.Text = resultList.karbohidrat.ToString() + " g";
            tvProtein.Text = resultList.protein.ToString() + " g";
            tvLemak.Text = resultList.lemak.ToString() + " g";

            // button onclick
            Button btnKembali = this.FindViewById<Button>(Resource.Id.btnKembali);
            btnKembali.Click += (sender, e) => {
                Finish();
            };
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