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

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // get data riwayat
            List<RiwayatGizi> data = DB.GetRiwayatHitungGizi();

            // Create your application here
            RecyclerView rvRiwayatHitung = FindViewById<RecyclerView>(Resource.Id.rvRiwayatHitung);
            RiwayatHitungAdapter mAdapter = new RiwayatHitungAdapter(data);
            LinearLayoutManager mLayoutManager = new LinearLayoutManager(this);
            rvRiwayatHitung.SetLayoutManager(mLayoutManager);
            rvRiwayatHitung.SetAdapter(mAdapter);
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