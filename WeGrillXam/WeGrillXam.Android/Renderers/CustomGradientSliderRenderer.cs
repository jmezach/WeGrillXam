using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using WeGrillXam.Controls;
using WeGrillXam.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomGradientSlider), typeof(CustomGradientSliderRenderer))]
namespace WeGrillXam.Droid.Renderers
{
	public class CustomGradientSliderRenderer : SliderRenderer
	{
        public CustomGradientSliderRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //convert details from CustomGradientSlider to android values
                var slider = Element as CustomGradientSlider;
                var startColor = slider.StartColor.ToAndroid();
                var centerColor = slider.CenterColor.ToAndroid();
                var endColor = slider.EndColor.ToAndroid();
                var cornerRadiusInPx = ((float)slider.CornerRadius).DpToPixels(Context);
                var heightPx = ((float)slider.HeightRequest).DpToPixels(Context);

                //create minimum track
                var p = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { startColor, endColor });
                p.SetCornerRadius(cornerRadiusInPx);
                var progress = new ClipDrawable(p, GravityFlags.Left, ClipDrawableOrientation.Horizontal);

                //create maximum track
                var background = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { startColor, endColor });
                background.SetCornerRadius(cornerRadiusInPx);

                var pd = new LayerDrawable(new Drawable[] { background, progress });

                pd.SetLayerHeight(0, (int)heightPx);
                pd.SetLayerHeight(1, (int)heightPx);
                Control.ProgressDrawable = pd;
            }
        }
    }
}

