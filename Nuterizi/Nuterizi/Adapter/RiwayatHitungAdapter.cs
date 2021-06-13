using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuterizi.Adapter
{
    class RiwayatHitungAdapter : RecyclerView.Adapter
    {
        public List<RiwayatGizi> riwayatGizi;

        public RiwayatHitungAdapter(List<RiwayatGizi> data)
        {
            riwayatGizi = data;
        }
        public override int ItemCount
        {
            get { return riwayatGizi.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RiwayatHitungAdapterViewHolder vh = holder as RiwayatHitungAdapterViewHolder;
            vh.tvTanggal.Text = riwayatGizi[position].Tanggal.ToString();
            vh.tvKebutuhanHarian.Text = riwayatGizi[position].Energi.ToString();

            holder.ItemView.Click += (sender, e) =>
            {
                Intent intentHitungGizi = new Intent(holder.ItemView.Context, typeof(ActivityHitungGiziHasil));
                intentHitungGizi.PutExtra("frekuensi", riwayatGizi[position].Frekuensi);
                intentHitungGizi.PutExtra("energi", riwayatGizi[position].Energi);
                intentHitungGizi.PutExtra("karbohidrat", riwayatGizi[position].Karbohidrat);
                intentHitungGizi.PutExtra("protein", riwayatGizi[position].Protein);
                intentHitungGizi.PutExtra("lemak", riwayatGizi[position].Lemak);
                holder.ItemView.Context.StartActivity(intentHitungGizi);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.activity_hitung_gizi_riwayat_item, parent, false);
            RiwayatHitungAdapterViewHolder vh = new RiwayatHitungAdapterViewHolder(itemView);

            return vh;
        }
    }

    class RiwayatHitungAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView tvTanggal { get; set; }
        public TextView tvKebutuhanHarian { get; set; }

        public RiwayatHitungAdapterViewHolder(View itemView) : base(itemView)
        {
            tvTanggal = itemView.FindViewById<TextView>(Resource.Id.tvTanggal);
            tvKebutuhanHarian = itemView.FindViewById<TextView>(Resource.Id.tvKebutuhanHarian);
        }
    }
}