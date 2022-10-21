using System;
using System.IO;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace DynamicImage
{
    public class DynamicImageGenerator
    {
        private readonly ICounterProvider counter;
        private const int ImageWidth = 200;
        private const int ImageHeight = 200;

        public DynamicImageGenerator(ICounterProvider counter)
        {
            this.counter = counter;
        }

        public Stream GenerateImage(Guid counterId, string? name)
        {
            SkiaBitmapExportContext bmp = new(ImageWidth, ImageHeight, 1f);
            ICanvas canvas = bmp.Canvas;
            int currentCounter = counter.Increment(counterId);

            string text = currentCounter.ToString();
            if (!string.IsNullOrEmpty(name))
            {
                text += " " + name;
            }

            Font myFont = new("Arial");
            float myFontSize = 24;
            canvas.Font = myFont;
            SizeF textSize = canvas.GetStringSize(text, myFont, myFontSize);

            Point point = new(
                x: (ImageWidth - textSize.Width) / 2,
                y: (ImageHeight - textSize.Height) / 2);
            Rect textRectangle = new(point, textSize);
            canvas.FillColor = Colors.White;
            canvas.FillRectangle(textRectangle);

            canvas.FontSize = myFontSize * .9f;
            canvas.FontColor = Colors.Black;
            canvas.DrawString(text, textRectangle,
                HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.OverflowBounds);

            MemoryStream ms = new();
            bmp.WriteToStream(ms);
            ms.Position = 0;
            return ms;
        }
    }
}
