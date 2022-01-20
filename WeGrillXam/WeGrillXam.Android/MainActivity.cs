using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

[assembly: Shiny.ShinyApplication(
    ShinyStartupTypeName = "WeGrillXam.Startup",
    XamarinFormsAppTypeName = "WeGrillXam.App"
)]

namespace WeGrillXam.Droid
{
    [Activity(Label = "WeGrillXam", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public partial class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
    }
}
