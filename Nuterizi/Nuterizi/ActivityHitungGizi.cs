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
    public class ActivityHitungGizi : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_hitung_gizi);

            // Create your application here
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spFrekuensiAktivitas);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Akt_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

    }
}