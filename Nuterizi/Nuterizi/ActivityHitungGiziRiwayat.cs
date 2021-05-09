using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Nuterizi.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuterizi
{
    [Activity(Label = "ActivityHitungGiziRiwayat")]
    public class ActivityHitungGiziRiwayat : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_hitung_gizi_riwayat);

            // Test database
            Log.Debug("Program", "DB start");
            //DB.SaveRiwayatHitungGizi();
            List<RiwayatGizi> data = DB.GetRiwayatHitungGizi();

            // Create your application here
            RecyclerView rvRiwayatHitung = FindViewById<RecyclerView>(Resource.Id.rvRiwayatHitung);
            // Instantiate the adapter and pass in its data source:
            RiwayatHitungAdapter mAdapter = new RiwayatHitungAdapter(data);

            LinearLayoutManager mLayoutManager = new LinearLayoutManager(this);
            rvRiwayatHitung.SetLayoutManager(mLayoutManager);

            rvRiwayatHitung.SetAdapter(mAdapter);
        }
    }
}