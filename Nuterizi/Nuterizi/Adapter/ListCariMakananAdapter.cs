using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuterizi.Adapter
{
    public class ListCariMakananAdapter : RecyclerView.Adapter
    {
        public List<ListMakanan> makanan;

        public ListCariMakananAdapter(List<ListMakanan> data)
        {
            makanan = data;
        }
        public override int ItemCount
        {
            get { return makanan.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ListCariMakananViewHolder vh = holder as ListCariMakananViewHolder;
            vh.tvNamaMakanan.Text = makanan[position].nama;

            holder.ItemView.Click += (sender, e) =>
            {
                Intent intentHasilGizi = new Intent(holder.ItemView.Context, typeof(ActivityDaftarGiziHasil));
                intentHasilGizi.PutExtra("query", makanan[position].nama);
                holder.ItemView.Context.StartActivity(intentHasilGizi);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.activity_daftar_gizi_list_item, parent, false);
            ListCariMakananViewHolder vh = new ListCariMakananViewHolder(itemView);

            return vh;
        }
    }

    class ListCariMakananViewHolder : RecyclerView.ViewHolder
    {
        public TextView tvNamaMakanan { get; set; }

        public ListCariMakananViewHolder(View itemView) : base(itemView)
        {
            tvNamaMakanan = itemView.FindViewById<TextView>(Resource.Id.tvNamaMakanan);
        }
    }
}