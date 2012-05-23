//-----------------------------------------------------------------------------
// <copyright file="Extensions.Imaging.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Date:       06/24/2010
//-----------------------------------------------------------------------------
//  Based on: http://aspnet.codeplex.com/
namespace Vodca
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Gets the content type by image format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>The content type by image format. Default 'image/x-unknown'</returns>
        public static string GetContentTypeByImageFormat(this ImageFormat format)
        {
            string ctype = "image/x-unknown";

            if (format.Equals(ImageFormat.Gif))
            {
                ctype = "image/gif";
            }
            else if (format.Equals(ImageFormat.Jpeg))
            {
                ctype = "image/jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                ctype = "image/png";
            }
            else if (format.Equals(ImageFormat.Bmp) || format.Equals(ImageFormat.MemoryBmp))
            {
                ctype = "image/bmp";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                ctype = "image/x-icon";
            }
            else if (format.Equals(ImageFormat.Tiff))
            {
                ctype = "image/tiff";
            }

            return ctype;
        }

        /// <summary>
        ///     Gets the type of the image format by content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>The type of the image format by content</returns>
        public static ImageFormat GetImageFormatByContentType(string contentType)
        {
            ImageFormat format = null;

            if (contentType != null)
            {
                if (contentType.Equals("image/gif"))
                {
                    format = ImageFormat.Gif;
                }
                else if (contentType.Equals("image/jpeg") || contentType.Equals("image/pjpeg"))
                {
                    format = ImageFormat.Jpeg;
                }
                else if (contentType.Equals("image/png"))
                {
                    format = ImageFormat.Png;
                }
                else if (contentType.Equals("image/bmp"))
                {
                    format = ImageFormat.Bmp;
                }
                else if (contentType.Equals("image/x-icon"))
                {
                    format = ImageFormat.Icon;
                }
                else if (contentType.Equals("image/tiff"))
                {
                    format = ImageFormat.Tiff;
                }
            }

            return format;
        }

        /// <summary>
        ///     Gets the type of the file extension by content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>The type of the file extension by content</returns>
        public static string GetImageFileExtensionByContentType(string contentType)
        {
            string ext = "bin";

            if (contentType.Equals("image/gif"))
            {
                ext = "gif";
            }
            else if (contentType.Equals("image/jpeg") || contentType.Equals("image/pjpeg"))
            {
                ext = "jpg";
            }
            else if (contentType.Equals("image/png"))
            {
                ext = "png";
            }
            else if (contentType.Equals("image/bmp"))
            {
                ext = "bmp";
            }
            else if (contentType.Equals("image/x-icon"))
            {
                ext = "ico";
            }
            else if (contentType.Equals("image/tiff"))
            {
                ext = "tif";
            }

            return ext;
        }

        /// <summary>
        ///     Gets the bytes from image. The image format is Jpeg!
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The byte array from image</returns>
        public static byte[] GetBytesFromImage(this Image image)
        {
            return image.GetBytesFromImage(ImageFormat.Jpeg);
        }

        /// <summary>
        ///     Gets the bytes from image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>The byte array from image</returns>
        public static byte[] GetBytesFromImage(this Image image, ImageFormat imageFormat)
        {
            if (image != null && imageFormat != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, imageFormat);
                    ms.Seek(0, SeekOrigin.Begin);
                    var imageBytes = new byte[ms.Length];
                    ms.Read(imageBytes, 0, (int)ms.Length);

                    return imageBytes;
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets the image from bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>The image from byte array</returns>
        public static Image GetImageFromBytes(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            using (var ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        ///     Scales the image.
        /// </summary>
        /// <param name="bytes">The image bytes.</param>
        /// <param name="maxWidth">Max width of the image.</param>
        /// <param name="maxHeight">Max weight of the image.</param>
        /// <param name="imageformat">The image format.</param>
        /// <returns>The scaled image</returns>
        public static byte[] ScaleImage(this byte[] bytes, int maxWidth, int maxHeight, ImageFormat imageformat)
        {
            if (bytes != null)
            {
                MemoryStream memory = null;
                byte[] result;
                try
                {
                    memory = new MemoryStream(bytes);
                    using (Image image = Image.FromStream(memory))
                    {
                        result = image.ScaleImage(maxWidth, maxHeight, imageformat);
                    }
                }
                finally
                {
                    if (memory != null)
                    {
                        memory.Dispose();
                    }
                }

                return result;
            }

            return null;
        }

        /// <summary>
        ///     Scales the image.
        /// </summary>
        /// <param name="image">The image instance.</param>
        /// <param name="maxWidth">Max width of the image.</param>
        /// <param name="maxHeight">Max weight of the image.</param>
        /// <param name="imageformat">The image format.</param>
        /// <returns>The scaled image</returns>
        public static byte[] ScaleImage(this Image image, int maxWidth, int maxHeight, ImageFormat imageformat)
        {
            Ensure.IsNotNull(image, "The image can't be null!");
            Ensure.IsTrue(maxWidth > 0, "The max width must be higher then 0");
            Ensure.IsTrue(maxHeight > 0, "The max height must be higher then 0");

            if (imageformat == null)
            {
                imageformat = image.RawFormat;
            }

            if (image.Size.Width > maxWidth || image.Size.Height > maxHeight)
            {
                // resize the image to fit our website's required size
                int newWidth = image.Size.Width;
                int newHeight = image.Size.Height;

                if (newWidth > maxWidth)
                {
                    newWidth = maxWidth;
                    newHeight = (int)(newHeight * ((float)newWidth / image.Size.Width));
                }

                if (newHeight > maxHeight)
                {
                    newHeight = maxHeight;
                    newWidth = image.Size.Width;
                    newWidth = (int)(newWidth * ((float)newHeight / image.Size.Height));
                }

                /* Resize the image to fit in the allowed image size */
                bool indexed = image.PixelFormat == PixelFormat.Format1bppIndexed || image.PixelFormat == PixelFormat.Format4bppIndexed || image.PixelFormat == PixelFormat.Format8bppIndexed || image.PixelFormat == PixelFormat.Indexed;

                using (Bitmap newImage = indexed ? new Bitmap(newWidth, newHeight) : new Bitmap(newWidth, newHeight, image.PixelFormat))
                {
                    using (var graphics = Graphics.FromImage(newImage))
                    {
                        if (indexed)
                        {
                            graphics.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
                        }

                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        newImage.Save(memoryStream, imageformat);
                        return memoryStream.ToArray();
                    }
                }
            }

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, imageformat);
                return memoryStream.ToArray();
            }
        }
    }
}
