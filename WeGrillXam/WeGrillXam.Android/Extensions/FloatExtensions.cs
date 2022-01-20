using System;
using Android.Content;
using Android.Util;

namespace WeGrillXam.Droid
{
	public static class FloatExtensions
	{
        public static float DpToPixels(this float valueInDp, Context context)
        {
            var metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}

