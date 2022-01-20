using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace WeGrillXam.Controls
{
	public class Gauge : SKCanvasView
	{
        private const float INNER_CIRCLE_RADIUS = 125f;

        #region Properties
        // Properties for the Values
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(float), typeof(Gauge), 0.0f);

        public float Value
        {
            get { return (float)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly BindableProperty StartValueProperty =
            BindableProperty.Create(nameof(StartValue), typeof(float), typeof(Gauge), 0.0f);

        public float StartValue
        {
            get { return (float)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public static readonly BindableProperty EndValueProperty =
            BindableProperty.Create(nameof(EndValue), typeof(float), typeof(Gauge), 100.0f);

        public float EndValue
        {
            get { return (float)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        public static readonly BindableProperty HighlightRangeStartValueProperty =
            BindableProperty.Create(nameof(HighlightRangeStartValue), typeof(float), typeof(Gauge), 70.0f);

        public float HighlightRangeStartValue
        {
            get { return (float)GetValue(HighlightRangeStartValueProperty); }
            set { SetValue(HighlightRangeStartValueProperty, value); }
        }

        public static readonly BindableProperty HighlightRangeEndValueProperty =
            BindableProperty.Create(nameof(HighlightRangeEndValue), typeof(float), typeof(Gauge), 100.0f);

        public float HighlightRangeEndValue
        {
            get { return (float)GetValue(HighlightRangeEndValueProperty); }
            set { SetValue(HighlightRangeEndValueProperty, value); }
        }

        // Properties for the Colors
        public static readonly BindableProperty GaugeLineColorProperty =
            BindableProperty.Create(nameof(GaugeLineColor), typeof(Color), typeof(Gauge), Color.FromHex("#70CBE6"));

        public Color GaugeLineColor
        {
            get { return (Color)GetValue(GaugeLineColorProperty); }
            set { SetValue(GaugeLineColorProperty, value); }
        }

        public static readonly BindableProperty ValueColorProperty =
            BindableProperty.Create(nameof(ValueColor), typeof(Color), typeof(Gauge), Color.FromHex("FF9A52"));

        public Color ValueColor
        {
            get { return (Color)GetValue(ValueColorProperty); }
            set { SetValue(ValueColorProperty, value); }
        }

        public static readonly BindableProperty RangeColorProperty =
            BindableProperty.Create(nameof(RangeColor), typeof(Color), typeof(Gauge), Color.FromHex("#E6F4F7"));

        public Color RangeColor
        {
            get { return (Color)GetValue(RangeColorProperty); }
            set { SetValue(RangeColorProperty, value); }
        }

        public static readonly BindableProperty NeedleColorProperty =
           BindableProperty.Create(nameof(NeedleColor), typeof(Color), typeof(Gauge), Color.FromRgb(252, 18, 30));

        public Color NeedleColor
        {
            get { return (Color)GetValue(NeedleColorProperty); }
            set { SetValue(NeedleColorProperty, value); }
        }

        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(Gauge), Color.FromRgb(0, 0, 0));

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(Gauge), Color.FromRgb(255, 255, 255));

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        // Properties for the Units

        public static readonly BindableProperty UnitsTextProperty =
            BindableProperty.Create("UnitsText", typeof(string), typeof(Gauge), "");

        public string UnitsText
        {
            get { return (string)GetValue(UnitsTextProperty); }
            set { SetValue(UnitsTextProperty, value); }
        }

        public static readonly BindableProperty ValueFontSizeProperty =
           BindableProperty.Create("ValueFontSize", typeof(float), typeof(Gauge), 33f);

        public float ValueFontSize
        {
            get { return (float)GetValue(ValueFontSizeProperty); }
            set { SetValue(ValueFontSizeProperty, value); }
        }
        #endregion

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;
            canvas.Clear();

            int width = e.Info.Width;
            int height = e.Info.Height;

            SKPaint backPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = this.BackgroundColor.ToSKColor()
            };

            canvas.DrawRect(new SKRect(0, 0, width, height), backPaint);

            canvas.Save();

            canvas.Translate(width / 2, height / 2);
            canvas.Scale(Math.Min(width / 210f, height / 520f));
            SKPoint center = new SKPoint(0, 0);

            var rect = new SKRect(-200, -200, 200, 200);

            // Add a buffer for the rectangle
            rect.Inflate(-10, -10);

            // Draw the main gauge
            var startAngle = 135;
            var sweepAngle = 270f;

            var shader = SKShader.CreateSweepGradient(center, new SKColor[] { StartColor.ToSKColor(), EndColor.ToSKColor() },
                SKShaderTileMode.Decal, 0, 270);
            var mainGuagePaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Round,
                Shader = shader,
                StrokeWidth = 50
            };

            canvas.Save();
            using (SKPath path = new SKPath())
            {
                canvas.RotateDegrees(startAngle);
                path.AddArc(rect, 0, sweepAngle);
                canvas.DrawPath(path, mainGuagePaint);
                canvas.Restore();
            }

            // Draw the overlaying lines
            var linesOverlayPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = BackgroundColor.ToSKColor(),
                PathEffect = SKPathEffect.CreateDash(new float[] { 2, 14 }, 0),
                StrokeWidth = 50
            };

            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path, linesOverlayPaint);
            }

            // Draw center circle
            var centerCirclePaint = new SKPaint
            {
                Color = NeedleColor.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 20
            };
            canvas.DrawCircle(center, INNER_CIRCLE_RADIUS, centerCirclePaint);

            //Draw Needle
            DrawNeedle(canvas, Value);

            // Draw the Units of Measurement Text on the display
            SKPaint textPaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Black
            };

            float textWidth = textPaint.MeasureText(UnitsText);
            textPaint.TextSize = 12f;

            SKRect textBounds = SKRect.Empty;
            textPaint.MeasureText(UnitsText, ref textBounds);

            float xText = -1 * textBounds.MidX;
            float yText = 95 - textBounds.Height;

            // And draw the text
            canvas.DrawText(UnitsText, xText, yText, textPaint);

            // Draw the Value on the display
            var valueText = Value.ToString("F0"); //You can set F1 or F2 if you need float values
            float valueTextWidth = textPaint.MeasureText(valueText);
            textPaint.TextSize = ValueFontSize;

            textPaint.MeasureText(valueText, ref textBounds);

            xText = -1 * textBounds.MidX;
            yText = 85 - textBounds.Height;

            // And draw the text
            canvas.DrawText(valueText, xText, yText, textPaint);
            canvas.Restore();
        }

        float AmountToAngle(float value)
        {
            return 135f + (value / (EndValue - StartValue)) * 270f;
        }

        void DrawNeedle(SKCanvas canvas, float value)
        {
            float angle = -135f + (value / (100 - 0)) * 270f;
            canvas.Save();
            canvas.RotateDegrees(angle);
            float needleWidth = 25f;
            float needleHeight = 60f;
            var x = 0;
            var y = -INNER_CIRCLE_RADIUS;

            Console.WriteLine($"Point ({x},{y})");

            SKPaint paint = new SKPaint
            {
                IsAntialias = true,
                Color = NeedleColor.ToSKColor(),
                StrokeCap = SKStrokeCap.Round
            };

            SKPath needleRightPath = new SKPath();
            needleRightPath.MoveTo(x, y);
            needleRightPath.LineTo(x + needleWidth, y);
            needleRightPath.LineTo(x, y - needleHeight);
            needleRightPath.LineTo(x, y);
            needleRightPath.LineTo(x + needleWidth, y);


            SKPath needleLeftPath = new SKPath();
            needleLeftPath.MoveTo(x, y);
            needleLeftPath.LineTo(x - needleWidth, y);
            needleLeftPath.LineTo(x, y - needleHeight);
            needleLeftPath.LineTo(x, y);
            needleLeftPath.LineTo(x - needleWidth, y);


            canvas.DrawPath(needleRightPath, paint);
            canvas.DrawPath(needleLeftPath, paint);
            canvas.Restore();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Determine when to change. Basically on any of the properties that we've added that affect
            // the visualization, including the size of the control, we'll repaint
            if (propertyName == HighlightRangeEndValueProperty.PropertyName
                || propertyName == HighlightRangeStartValueProperty.PropertyName
                || propertyName == ValueProperty.PropertyName
                || propertyName == WidthProperty.PropertyName
                || propertyName == HeightProperty.PropertyName
                || propertyName == StartValueProperty.PropertyName
                || propertyName == EndValueProperty.PropertyName
                || propertyName == GaugeLineColorProperty.PropertyName
                || propertyName == ValueColorProperty.PropertyName
                || propertyName == RangeColorProperty.PropertyName
                || propertyName == UnitsTextProperty.PropertyName)
            {
                InvalidateSurface();
            }
        }
    }
}
