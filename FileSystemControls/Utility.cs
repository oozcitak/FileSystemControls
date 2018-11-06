using System;
using System.Drawing;

namespace Manina.Windows.Forms
{
    internal static class Utility
    {
        /// <summary>
        /// Gets the bounding rectangle of an image required to fit
        /// in to the given rectangle keeping the image aspect ratio.
        /// </summary>
        /// <param name="image">The source image.</param>
        /// <param name="fit">The rectangle to fit in to.</param>
        /// <param name="hAlign">Horizontal image aligment in percent.</param>
        /// <param name="vAlign">Vertical image aligment in percent.</param>
        /// <returns>New image size.</returns>
        public static Rectangle GetSizedIconBounds(Image image, Rectangle fit, float hAlign, float vAlign)
        {
            if (hAlign < 0 || hAlign > 1.0f)
                throw new ArgumentException("hAlign must be between 0.0 and 1.0 (inclusive).", "hAlign");
            if (vAlign < 0 || vAlign > 1.0f)
                throw new ArgumentException("vAlign must be between 0.0 and 1.0 (inclusive).", "vAlign");
            Size scaled = GetSizedIconBounds(image, fit.Size);
            int x = fit.Left + (int)(hAlign * (fit.Width - scaled.Width));
            int y = fit.Top + (int)(vAlign * (fit.Height - scaled.Height));

            return new Rectangle(x, y, scaled.Width, scaled.Height);
        }

        /// <summary>
        /// Gets the scaled size of an image required to fit
        /// in to the given size keeping the image aspect ratio.
        /// </summary>
        /// <param name="image">The source image.</param>
        /// <param name="fit">The size to fit in to.</param>
        /// <returns>New image size.</returns>
        public static Size GetSizedIconBounds(Image image, Size fit)
        {
            float f = System.Math.Max(image.Width / (float)fit.Width, image.Height / (float)fit.Height);
            if (f < 1.0f) f = 1.0f; // Do not upsize small images
            int width = (int)System.Math.Round(image.Width / f);
            int height = (int)System.Math.Round(image.Height / f);
            return new Size(width, height);
        }

        /// <summary>
        /// Convertes a <see cref="RectangleF"/> to a <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="r">The rectangle to convert.</param>
        /// <returns>Converted rectangle.</returns>
        public static Rectangle ToRectangle(RectangleF r)
        {
            return new Rectangle((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);
        }

        /// <summary>
        /// Formats the given file size as a human readable string.
        /// </summary>
        /// <param name="size">File size in bytes.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatSize(long size)
        {
            double mod = 1024;
            double sized = size;

            // string[] units = new string[] { "B", "KiB", "MiB", "GiB", "TiB", "PiB" };
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            int i;
            for (i = 0; sized > mod; i++)
            {
                sized /= mod;
            }

            return string.Format("{0:0.#} {1}", sized, units[i]);
        }
    }
}
