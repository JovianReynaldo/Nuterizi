using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace Nuterizi
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button btnHitungKebutuhanGizi = FindViewById<Button>(Resource.Id.btnHitungKebutuhanGizi);
            Button btnDaftarGizi = FindViewById<Button>(Resource.Id.btnDaftarGizi);
            Button btnReminder = FindViewById<Button>(Resource.Id.btnReminder);


            btnHitungKebutuhanGizi.Click += (sender, e) => {
                Intent intent = new Intent(this, typeof(ActivityHitungGizi));
                StartActivity(intent);
            };

            btnDaftarGizi.Click += (sender, e) => {
                Intent intent = new Intent(this, typeof(ActivityDaftarGizi));
                StartActivity(intent);
            };

            btnReminder.Click += (sender, e) => {
                Intent intent = new Intent(this, typeof(ActivityReminder));
                StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}