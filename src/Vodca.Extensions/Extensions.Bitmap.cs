//-----------------------------------------------------------------------------
// <copyright file="Extensions.Bitmap.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the image codec info.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>The Image codec info</returns>
        public static ImageCodecInfo GetImageCodecInfo(this ImageFormat format)
        {
            if (format != null)
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

                return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
            }

            return null;
        }

        /// <summary>
        /// Saves Bitmap as as JPEG.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <param name="filepath">The file path.</param>
        /// <param name="quality">The quality.</param>
        /// <param name="isvirtualpath">if set to <c>true</c> [is virtual path].</param>
        public static void SaveAsJpeg(this Image bmp, string filepath, long quality = 80L, bool isvirtualpath = true)
        {
            if (bmp != null && !string.IsNullOrWhiteSpace(filepath) && quality < 101 && quality > 0)
            {
                using (var encoderParameters = new EncoderParameters(1))
                {
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                    if (isvirtualpath)
                    {
                        filepath = filepath.MapPath();
                    }

                    bmp.Save(filepath, ImageFormat.Jpeg.GetImageCodecInfo(), encoderParameters);
                }
            }
        }

        /// <summary>
        /// Saves as JPEG.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="quality">The quality.</param>
        public static void SaveAsJpeg(this Image bmp, Stream stream, long quality = 80L)
        {
            if (bmp != null && stream != null && quality < 101 && quality > 0)
            {
                using (var encoderParameters = new EncoderParameters(1))
                {
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                    bmp.Save(stream, ImageFormat.Jpeg.GetImageCodecInfo(), encoderParameters);
                }
            }
        }

        /// <summary>
        /// Adds the layers to image.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="imgPaths">The img paths.</param>
        /// <returns>The Layered Image</returns>
        public static Image AddLayersToImage(this Image img, float? width = null, float? height = null, params string[] imgPaths)
        {
            if (img != null)
            {
                using (var bitmap = new Bitmap((int?)width ?? img.Width, (int?)height ?? img.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                        graphics.DrawImage(img, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

                        foreach (var imgPath in imgPaths)
                        {
                            try
                            {
                                var filepath = imgPath.MapPath();
                                if (File.Exists(filepath))
                                {
                                    using (var layer = Image.FromFile(filepath, true))
                                    {
                                        if (layer.IsNotNull())
                                        {
                                            graphics.DrawImage(layer, new Rectangle(0, 0, layer.Width, layer.Height));
                                        }
                                    }
                                }
                                else
                                {
                                    VLog.Logger.Error("AddLayersToImage: filepath is not found=> " + filepath);
                                }
                            }
                            catch (OutOfMemoryException mex)
                            {
                                VLog.LogException(mex);
                            }
                            catch (FileNotFoundException fex)
                            {
                                VLog.LogException(fex);
                            }
                            catch (ArgumentException aex)
                            {
                                VLog.LogException(aex);
                            }
                        }

                        return Image.FromHbitmap(bitmap.GetHbitmap());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Adds the layers to image.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="imgUris">The img uris.</param>
        /// <returns>The Layered Image</returns>
        public static Image AddLayersToImage(this Image img, int? width = null, int? height = null, params Uri[] imgUris)
        {
            if (img != null)
            {
                using (var bitmap = new Bitmap(width.HasValue ? width.Value : img.Width, height.HasValue ? height.Value : img.Height))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                        graphics.DrawImage(img, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

                        foreach (var imgUri in imgUris)
                        {
                            using (var layer = imgUri.TryLoadImage())
                            {
                                if (layer != null)
                                {
                                    graphics.DrawImage(layer, new Rectangle(0, 0, layer.Width, layer.Height));
                                }
                            }
                        }

                        return Image.FromHbitmap(bitmap.GetHbitmap());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Tries to load image from Uri
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The Image or null</returns>
        public static Image TryLoadImage(this Uri uri)
        {
            try
            {
                var request = WebRequest.Create(uri);
                using (var response = request.GetResponse())
                {
                    using (var data = response.GetResponseStream())
                    {
                        if (data != null)
                        {
                            return Image.FromStream(data, true, true);
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                VLog.LogException(wex);
            }
            catch (ArgumentException aex)
            {
                VLog.LogException(aex);
            }
            catch (Exception ex)
            {
                VLog.LogException(ex);
            }

            return null;
        }

        /// <summary>
        /// Tries to load image from url string.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The Image or null</returns>
        public static Image TryLoadImage(this string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                Uri uri;
                if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
                {
                    return TryLoadImage(uri);
                }
            }
            else
            {
                VLog.Logger.Warn("Image TryLoadImage(this string url) parameter is null");
            }

            VLog.Logger.Warn("Image TryLoadImage(this string url) parameter not valid =>" + url);

            return null;
        }
    }
}
