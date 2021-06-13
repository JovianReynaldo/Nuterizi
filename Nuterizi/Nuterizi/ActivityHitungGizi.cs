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
using Android.Util;

namespace Nuterizi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ActivityHitungGizi : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_hitung_gizi);

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Spinner Option
            Spinner spFrekuensiAktivitas = FindViewById<Spinner>(Resource.Id.spFrekuensiAktivitas);
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Akt_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spFrekuensiAktivitas.Adapter = adapter;

            // Button Riwayat onclick
            Button btnHitungHasilKebutuhanGizi = FindViewById<Button>(Resource.Id.btnHitungHasilKebutuhanGizi);
            Button btnRiwayat = FindViewById<Button>(Resource.Id.btnRiwayat);

            btnRiwayat.Click += (sender, e) =>
            {
                Intent intentRiwayatGizi = new Intent(this, typeof(ActivityHitungGiziRiwayat));
                StartActivity(intentRiwayatGizi);
            };

            // Button Hitung onclick
            RadioButton rbLaki = FindViewById<RadioButton>(Resource.Id.rbLaki);
            RadioButton rbPerempuan = FindViewById<RadioButton>(Resource.Id.rbPerempuan);
            EditText etUmur = FindViewById<EditText>(Resource.Id.etUmur);
            EditText etTinggiBadan = FindViewById<EditText>(Resource.Id.etTinggiBadan);
            EditText etBeratBadan = FindViewById<EditText>(Resource.Id.etBeratBadan);

            btnHitungHasilKebutuhanGizi.Click += (sender, e) =>
            {
                DataKebutuhanGizi hitungGizi = new DataKebutuhanGizi
                {
                    JenisKelamin = rbLaki.Checked ? "L" : (rbPerempuan.Checked ? "P" : ""),
                    Frekuensi = spFrekuensiAktivitas.SelectedItem.ToString()
                };
                if (etUmur.Text != "")
                {
                    hitungGizi.Umur = int.Parse(etUmur.Text);
                }

                if (etTinggiBadan.Text != "")
                {
                    hitungGizi.TinggiBadan = int.Parse(etTinggiBadan.Text);
                }

                if (etBeratBadan.Text != "")
                {
                    hitungGizi.BeratBadan = int.Parse(etBeratBadan.Text);
                }

                if (hitungGizi.JenisKelamin != "" && etUmur.Text != "" && etTinggiBadan.Text != "" && etBeratBadan.Text != "" && spFrekuensiAktivitas.SelectedItem.ToString() != "")
                {
                    HitungEnergi(hitungGizi);
                } else
                {
                    Toast.MakeText(this, "Harap lengkapi data terlebih dahulu", ToastLength.Long).Show();
                }
            };
        }

        private void HitungEnergi(DataKebutuhanGizi dataGizi)
        {
            KebutuhanGizi hasilHitungGizi = new KebutuhanGizi();
            double BMR = 0;
            double total = 0;

            if (dataGizi.JenisKelamin == "L")
            {
                BMR = 10 * dataGizi.BeratBadan + 6.25 * dataGizi.TinggiBadan - 5 * dataGizi.Umur + 5;
            }
            else if (dataGizi.JenisKelamin == "P")
            {
                BMR = 10 * dataGizi.BeratBadan + 6.25 * dataGizi.TinggiBadan - 5 * dataGizi.Umur - 161;
            }

            switch (dataGizi.Frekuensi)
            {
                case "Tidak pernah":
                    total = BMR * 1.2;
                    break;
                case "Jarang (1-2 hari)":
                    total = BMR * 1.375;
                    break;
                case "Kadang (3-4 hari)":
                    total = BMR * 1.55;
                    break;
                case "Sering (5-6 hari)":
                    total = BMR * 1.75;
                    break;
                case "Selalu/olahraga berat":
                    total = BMR * 1.9;
                    break;
                default:
                    break;
            }


            hasilHitungGizi.Energi = Convert.ToInt32(total);
            hasilHitungGizi.Karbohidrat = Convert.ToInt32(hasilHitungGizi.Energi * 0.55 / 4);
            hasilHitungGizi.Protein = Convert.ToInt32(hasilHitungGizi.Energi * 0.2 / 4);
            hasilHitungGizi.Lemak = Convert.ToInt32(hasilHitungGizi.Energi * 0.25 / 9);

            DB.SaveRiwayatHitungGizi(dataGizi, hasilHitungGizi);

            Intent intentHitungGizi = new Intent(this, typeof(ActivityHitungGiziHasil));
            intentHitungGizi.PutExtra("energi", hasilHitungGizi.Energi);
            intentHitungGizi.PutExtra("karbohidrat", hasilHitungGizi.Karbohidrat);
            intentHitungGizi.PutExtra("protein", hasilHitungGizi.Protein);
            intentHitungGizi.PutExtra("lemak", hasilHitungGizi.Lemak);
            StartActivity(intentHitungGizi);
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