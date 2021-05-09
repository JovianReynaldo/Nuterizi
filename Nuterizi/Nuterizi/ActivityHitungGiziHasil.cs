using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
    public class ActivityHitungGiziHasil : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_hitung_gizi_hasil);

            // set data
            KebutuhanGizi hasilHitungGizi = new KebutuhanGizi();
            hasilHitungGizi.Energi = Intent.GetIntExtra("energi", 0);
            hasilHitungGizi.Karbohidrat = Intent.GetIntExtra("karbohidrat", 0);
            hasilHitungGizi.Protein = Intent.GetIntExtra("protein", 0);
            hasilHitungGizi.Lemak = Intent.GetIntExtra("lemak", 0);

            TextView tvEnergi = FindViewById<TextView>(Resource.Id.tvEnergi);
            TextView tvKarbohidrat = FindViewById<TextView>(Resource.Id.tvKarbohidrat);
            TextView tvProtein = FindViewById<TextView>(Resource.Id.tvProtein);
            TextView tvLemak = FindViewById<TextView>(Resource.Id.tvLemak);

            tvEnergi.Text = hasilHitungGizi.Energi.ToString();
            tvKarbohidrat.Text = hasilHitungGizi.Karbohidrat.ToString();
            tvProtein.Text = hasilHitungGizi.Protein.ToString();
            tvLemak.Text = hasilHitungGizi.Lemak.ToString();


            // button onclick
            Button btnKembali = FindViewById<Button>(Resource.Id.btnKembali);
            btnKembali.Click += (sender, e) => {
                Finish();
            };
        }
    }
}