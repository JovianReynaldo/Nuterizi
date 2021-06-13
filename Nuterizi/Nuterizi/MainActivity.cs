using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace Nuterizi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/Nuterizi")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // set toolbar
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // set data
            hitungKebutuhanKalori();


            // button onclick
            Button btnHitungKebutuhanGizi = FindViewById<Button>(Resource.Id.btnHitungKebutuhanGizi);
            Button btnDaftarGizi = FindViewById<Button>(Resource.Id.btnDaftarGizi);
            Button btnReminder = FindViewById<Button>(Resource.Id.btnReminder);

            //DB.EmptyRiwayatGizi();

            btnHitungKebutuhanGizi.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(ActivityHitungGizi));
                StartActivity(intent);
            };

            btnDaftarGizi.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(ActivityDaftarGizi));
                StartActivity(intent);
            };

            btnReminder.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(ActivityReminder));
                StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnResume()
        {
            base.OnResume();
            hitungKebutuhanKalori();
        }

        private void hitungKebutuhanKalori()
        {
            // set data
            Button btnEnergi = FindViewById<Button>(Resource.Id.btnEnergi);
            RiwayatGizi latestData = DB.GetLatestRiwayatHitungGizi();
            if (latestData != null)
            {
                btnEnergi.Text = (latestData.Energi.ToString() != "" ? latestData.Energi.ToString() : "0") + "kkal";
                btnEnergi.Click += (sender, e) =>
                {
                    if (latestData.Energi.ToString() != "")
                    {
                        Intent intentHitungGizi = new Intent(this, typeof(ActivityHitungGiziHasil));
                        intentHitungGizi.PutExtra("energi", latestData.Energi);
                        intentHitungGizi.PutExtra("karbohidrat", latestData.Karbohidrat);
                        intentHitungGizi.PutExtra("protein", latestData.Protein);
                        intentHitungGizi.PutExtra("lemak", latestData.Lemak);
                        StartActivity(intentHitungGizi);
                    }
                };
            }
            else
            {
                btnEnergi.Text = "0 kkal";
            }
        }
    }
}