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

namespace Nuterizi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ActivityDaftarGizi : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_daftar_gizi);

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Button Cari Makanan onclick
            EditText etCariMakanan = FindViewById<EditText>(Resource.Id.etCariMakanan);
            Button btnCariMakanan = FindViewById<Button>(Resource.Id.btnCariMakanan);

            btnCariMakanan.Click += (sender, e) => {
                //List<object> resultList = ApiClient.CariMakanan(etCariMakanan.Text);
                Intent intentCariMakanan = new Intent(this, typeof(ActivityDaftarGiziList));
                intentCariMakanan.PutExtra("query", etCariMakanan.Text);
                StartActivity(intentCariMakanan);
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