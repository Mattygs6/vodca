//-----------------------------------------------------------------------------
// <copyright company="genuine" file="VWebServices.cs">
//     Copyright (c) Genuine Interactive. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//        Developer:    J.B
//         Modified:    M.G
//             Date:    03/27/2012
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Web.Script.Services;
    using System.Web.Services;
    using Vodca;

    /// <summary>
    /// Vodca WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public partial class VWebServices : WebService
    {
        /// <summary>
        /// Runs the specified JSON.
        /// </summary>
        /// <typeparam name="TForm">The type of the form.</typeparam>
        /// <param name="json">The JSON.</param>
        /// <returns>The VJsonResponse of the form validation or pipeline</returns>
        protected VJsonResponse Run<TForm>(string json) where TForm : VForm, IFormAction
        {
            try
            {
                Ensure.IsNotNullOrEmpty(json, "The json is NULL or Empty string!");

                var data = json.DeserializeFromJson<TForm>();
                Ensure.IsNotNull(data, "The JSON serialization failed!");

                if (data.Validate())
                {
                    return data.Submit();
                }

                return VJsonResponse.DataIsInvalid(data.GetErrorList());
            }
            catch (Exception ex)
            {
                ex.LogException();
#if DEBUG
                return VJsonResponse.Fail(ex);
#else
                return VJsonResponse.Fail(ex.Message);
#endif
            }
        }

        /// <summary>
        /// Runs the specified JSON.
        /// </summary>
        /// <typeparam name="TForm">The type of the form.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>
        /// The VJsonResponse of the form validation or pipeline
        /// </returns>
        protected VJsonResponse Run<TForm>(TForm data) where TForm : VForm, IFormAction
        {
            try
            {
                if (data.Validate())
                {
                    return data.Submit();
                }

                return VJsonResponse.DataIsInvalid(data.GetErrorList());
            }
            catch (Exception ex)
            {
                ex.LogException();
#if DEBUG
                return VJsonResponse.Fail(ex);
#else
                return VJsonResponse.Fail(ex.Message);
#endif
            }
        }
    }
}