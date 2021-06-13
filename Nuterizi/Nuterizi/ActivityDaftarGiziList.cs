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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ActivityDaftarGiziList : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_daftar_gizi_list);

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            string query = Intent.GetStringExtra("query");
            List<ListMakanan> resultList = ApiClient.CariMakanan(query);

            // set recycleview
            RecyclerView rvListCariMakanan = FindViewById<RecyclerView>(Resource.Id.rvListCariMakanan);
            ListCariMakananAdapter mAdapter = new ListCariMakananAdapter(resultList);
            LinearLayoutManager mLayoutManager = new LinearLayoutManager(this);
            rvListCariMakanan.SetLayoutManager(mLayoutManager);
            rvListCariMakanan.SetAdapter(mAdapter);
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