﻿namespace OmniGui
{
    using Zafiro.PropertySystem.Standard;

    public class TextBlock : Layout
    {
        private string text;
        private Brush foreground;

        public static readonly ExtendedProperty FontSizeProperty = PropertyEngine.RegisterProperty("FontSize", typeof(TextBlock),
            typeof(float), new PropertyMetadata() { DefaultValue = 16F });
        public static readonly ExtendedProperty FontWeightProperty = PropertyEngine.RegisterProperty("FontWeight", typeof(TextBlock),
    typeof(float), new PropertyMetadata() { DefaultValue = FontWeight.Normal });
        public static readonly ExtendedProperty FontFamilyProperty = PropertyEngine.RegisterProperty("FontFamily", typeof(TextBlock),
typeof(float), new PropertyMetadata() { DefaultValue = "Arial" });
        public static readonly ExtendedProperty TextProperty = PropertyEngine.RegisterProperty("Text", typeof(TextBlock),
typeof(string), new PropertyMetadata() { DefaultValue = null });

        public TextBlock()
        {
            Foreground = new Brush(Colors.Black);
        }

        private void UpdateFormattedText()
        {
            FormattedText.Text = Text;
            FormattedText.FontFamily = FontFamily;
            FormattedText.Brush = Foreground;
            FormattedText.FontSize = FontSize;
            FormattedText.FontWeight = FontWeight;
        }

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set
            {
                SetValue(FontFamilyProperty, value);
                UpdateFormattedText();
            }
        }

        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set
            {
                SetValue(FontWeightProperty, value);
                UpdateFormattedText();
            }
        }

        public float FontSize
        {
            get { return (float)GetValue(FontSizeProperty); }
            set
            {
                SetValue(FontSizeProperty, value);
                UpdateFormattedText();
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Text == null)
            {
                return Size.Empty;
            }

            if (TextWrapping == TextWrapping.Wrap)
            {
                FormattedText.Constraint = new Size(availableSize.Width, double.PositiveInfinity);
            }
            else
            {
                FormattedText.Constraint = Size.Infinite;
            }

            return FormattedText.Measure();
        }

        public override void Render(IDrawingContext drawingContext)
        {
            if (Text == null)
            {
                return;
            }

            drawingContext.DrawText(FormattedText, VisualBounds.Point);
        }

        public Brush Foreground
        {
            get { return foreground; }
            set
            {
                foreground = value;
                UpdateFormattedText();
            }
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                UpdateFormattedText();
            }
        }

        private FormattedText FormattedText { get; set; } = new FormattedText() { FontSize = 16 };

        public TextWrapping TextWrapping { get; set; }
    }
}