﻿using Android.Graphics;
using OmniGui.Geometry;
using Rect = Android.Graphics.Rect;

namespace OmniGui.Android
{
    using Rect = Rect;

    public class AndroidTextEngine : ITextEngine
    {
        public Size Measure(FormattedText formattedText)
        {
            var paint = new Paint();
            var rect = new Rect();
            paint.TextSize = formattedText.FontSize;          
            paint.GetTextBounds(formattedText.Text, 0, formattedText.Text.Length, rect);

            var descent = paint.Descent();

            var height = rect.Height();
            var width = rect.Width();
            return new Size(width, height + descent);
        }

        public double GetHeight(string fontFamily, float fontSize)
        {
            var paint = new Paint();
            paint.TextSize = fontSize;

            var fontMetrics = paint.GetFontMetrics(null);

            return fontMetrics;
        }
    }
}