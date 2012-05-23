//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.HttpPostedFile.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/15/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Rule determines whether the uploaded file is Web format Image (Jpeg or Gif  or Png).
        /// </summary>
        /// <remarks>
        ///     The progressive Jpeg are valid. <br />
        ///  A simple or "baseline" JPEG file is stored as one top-to-bottom scan of the
        /// image.  Progressive JPEG divides the file into a series of scans and not supported by many browsers still.
        /// </remarks>
        /// <param name="postedfile">The HttpPosted File instance</param>
        /// <returns>
        ///     <c>true</c> if is; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// </example>
        public static bool IsUploadedFileWebFormatImage(this HttpPostedFile postedfile)
        {
            if (postedfile != null)
            {
                return postedfile.ContentType == VContentTypes.Jpeg || postedfile.ContentType == VContentTypes.Jpg
                       || postedfile.ContentType == VContentTypes.Gif || postedfile.ContentType == VContentTypes.Png;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is uploaded file JPEG] [the specified posted file].
        /// </summary>
        /// <param name="postedfile">The posted file.</param>
        /// <returns>
        ///   <c>true</c> if [is uploaded file JPEG] [the specified posted file]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUploadedFileJpeg(this HttpPostedFile postedfile)
        {
            if (postedfile != null)
            {
                return postedfile.ContentType == VContentTypes.Jpeg || postedfile.ContentType == VContentTypes.Jpg;
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the uploaded file is Web format Image (Jpeg or Gif  or Png).
        /// </summary>
        /// <remarks>
        ///     The progressive Jpeg are valid. <br />
        ///  A simple or "baseline" JPEG file is stored as one top-to-bottom scan of the
        /// image.  Progressive JPEG divides the file into a series of scans and not supported by many browsers still.
        /// </remarks>
        /// <param name="postedfile">The HttpPosted File instance</param>
        /// <param name="maxwidth">The max width of the image</param>
        /// <param name="maxheight">The max height of the image</param>
        /// <returns>
        ///     <c>true</c> if is; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// </example>
        public static bool IsUploadedFileWebFormatImage(this HttpPostedFile postedfile, int maxwidth, int maxheight)
        {
            Ensure.IsNotNull(postedfile, "The HttpPosted file is NULL!");
            Ensure.IsTrue(maxwidth > 0, "The Max Width of the image must be greater then 0!");
            Ensure.IsTrue(maxheight > 0, "The Max height of the image must be greater then 0!");

            if (postedfile.IsUploadedFileWebFormatImage())
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(postedfile.InputStream))
                {
                    return image.Width <= maxwidth && image.Height <= maxheight;
                }
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the uploaded file is Web format Image (Jpeg or Gif  or Png).
        /// </summary>
        /// <remarks>
        ///     The progressive Jpeg are valid. <br />
        ///  A simple or "baseline" JPEG file is stored as one top-to-bottom scan of the
        /// image.  Progressive JPEG divides the file into a series of scans and not supported by many browsers still.
        /// </remarks>
        /// <param name="postedfile">The HttpPosted File instance</param>
        /// <param name="maxwidth">The max width of the image</param>
        /// <param name="maxheight">The max height of the image</param>
        /// <param name="maxsize">The max size of the image in bytes  like '100000' which is around 100kb</param>
        /// <returns>
        ///     <c>true</c> if is; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// </example>
        public static bool IsUploadedFileWebFormatImage(this HttpPostedFile postedfile, int maxwidth, int maxheight, int maxsize)
        {
            Ensure.IsNotNull(postedfile, "The HttpPosted file is NULL!");
            Ensure.IsTrue(maxwidth > 0, "The Max Width of the image must be greater then 0!");
            Ensure.IsTrue(maxheight > 0, "The Max height of the image must be greater then 0!");
            Ensure.IsTrue(maxsize > 0, "The Max size of the image must be greater then 0!");

            if (postedfile.IsUploadedFileWebFormatImage())
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(postedfile.InputStream))
                {
                    return image.Width <= maxwidth && image.Height <= maxheight && postedfile.ContentLength <= maxsize;
                }
            }

            return false;
        }
    }
}