using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace NDS.Utility
{
    public class ImageResizer
    {
        /// <summary>
        /// http://www.blackbeltcoder.com/Articles/graph/programmatically-resizing-an-image
        /// Maximum width of resized image.
        /// </summary>
        public int MaxX { get; set; }

        /// <summary>
        /// Maximum height of resized image.
        /// </summary>
        public int MaxY { get; set; }

        /// <summary>
        /// If true, resized image is trimmed to exactly fit
        /// maximum width and height dimensions.
        /// </summary>
        public bool TrimImage { get; set; }

        /// <summary>
        /// Format used to save resized image.
        /// </summary>
        public ImageFormat SaveFormat { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageResizer()
        {
            MaxX = MaxY = 150;
            TrimImage = false;
            SaveFormat = ImageFormat.Jpeg;
        }

        /// <summary>
        /// Resizes the image from the source file according to the
        /// current settings and saves the result to the targe file.
        /// </summary>
        /// <param name="source">Path containing image to resize</param>
        /// <param name="target">Path to save resized image</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Resize(string source, string target)
        {
            using (Image src = Image.FromFile(source, true))
            {
                // Check that we have an image
                if (src != null)
                {
                    int origX, origY, newX, newY;
                    int trimX = 0, trimY = 0;

                    // Default to size of source image
                    newX = origX = src.Width;
                    newY = origY = src.Height;

                    // Does image exceed maximum dimensions?
                    if (origX > MaxX || origY > MaxY)
                    {
                        // Need to resize image
                        if (TrimImage)
                        {
                            // Trim to exactly fit maximum dimensions
                            double factor = Math.Max((double)MaxX / (double)origX,
                                (double)MaxY / (double)origY);
                            newX = (int)Math.Ceiling((double)origX * factor);
                            newY = (int)Math.Ceiling((double)origY * factor);
                            trimX = newX - MaxX;
                            trimY = newY - MaxY;
                        }
                        else
                        {
                            // Resize (no trim) to keep within maximum dimensions
                            double factor = Math.Min((double)MaxX / (double)origX,
                                (double)MaxY / (double)origY);
                            newX = (int)Math.Ceiling((double)origX * factor);
                            newY = (int)Math.Ceiling((double)origY * factor);
                        }
                    }

                    // Create destination image
                    using (Image dest = new Bitmap(newX - trimX, newY - trimY))
                    {
                        Graphics graph = Graphics.FromImage(dest);
                        graph.InterpolationMode =
                            System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graph.DrawImage(src, -(trimX / 2), -(trimY / 2), newX, newY);
                        dest.Save(target, SaveFormat);
                        // Indicate success
                        return true;
                    }
                }
            }
            // Indicate failure
            return false;
        }
    }


    public static class Resize
    {
        public enum ImageComperssion
        {
            Maximum = 50,
            Good = 60,
            Normal = 70,
            Fast = 80,
            Minimum = 90,
        }

        public static void ResizeImage(this Stream inputStream, int width, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image img = new Bitmap(inputStream);
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImage(this System.Drawing.Image img, int width, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByWidth(this Stream inputStream, int width, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image img = new Bitmap(inputStream);
            int height = img.Height * width / img.Width;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByWidth(this System.Drawing.Image img, int width, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            int height = img.Height * width / img.Width;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByHeight(this Stream inputStream, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            System.Drawing.Image img = new Bitmap(inputStream);
            int width = img.Width * height / img.Height;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void ResizeImageByHeight(this System.Drawing.Image img, int height, string savePath, ImageComperssion ic = ImageComperssion.Normal)
        {
            int width = img.Width * height / img.Height;
            System.Drawing.Image result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            result.CompressImage(savePath, ic);
        }

        public static void CompressImage(this System.Drawing.Image img, string path, ImageComperssion ic)
        {
            System.Drawing.Imaging.EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Convert.ToInt32(ic));
            ImageFormat format = img.RawFormat;
            ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == format.Guid);
            string mimeType = codec == null ? "image/jpeg" : codec.MimeType;
            ImageCodecInfo jpegCodec = null;
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codecs.Length; i++)
            {
                if (codecs[i].MimeType == mimeType)
                {
                    jpegCodec = codecs[i];
                    break;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }


    }


}
