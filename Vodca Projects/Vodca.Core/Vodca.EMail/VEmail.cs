//-----------------------------------------------------------------------------
// <copyright file="VEmail.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//   Date:      11/08/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Web.Configuration;

    /// <summary>
    /// The Email functionality for the site
    /// </summary>
    /// <remarks>
    /// <strong>Important:</strong> 
    /// Add to the AppSettings section key/value pairs System.Net.Mail.DefaultSendersEmail and System.Net.Mail.SmtpServer
    /// </remarks>
    /// <example>View code: <br />
    /// <code title="Web.config" lang="xml" >
    /// <![CDATA[
    /// <appSettings>
    ///     <add key="Vodca.VEmail.SmtpServer" value="webserver"/>
    ///     <add key="Vodca.VEmail.DefaultSendersEmail" value="development@genuineinteractive.com"/>
    /// </appSettings>
    /// ]]>
    /// </code>
    /// </example>
    public sealed partial class VEmail : MailMessage, IValidate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VEmail"/> class.
        /// </summary>
        public VEmail()
        {
            this.Sender = GetDefaultSendersEmail();

            this.Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VEmail"/> class.
        /// </summary>
        /// <param name="from">A <see cref="T:System.Net.Mail.MailAddress"/> that contains the address of the sender of the e-mail message.</param>
        /// <param name="to">A <see cref="T:System.Net.Mail.MailAddress"/> that contains the address of the recipient of the e-mail message.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="from"/> is null.-or-<paramref name="to"/> is null.</exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="from"/> or <paramref name="to"/> is malformed.</exception>
        public VEmail(MailAddress from, MailAddress to)
            : base(from, to)
        {
            this.Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VEmail"/> class.
        /// </summary>
        /// <param name="from">A <see cref="T:System.Net.Mail.MailAddress"/> that contains the address of the sender of the e-mail.</param>
        /// <param name="to">A list<see cref="T:System.Net.Mail.MailAddress"/> that contains the address of the recipients of the e-mails.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="from"/> is null.-or-<paramref name="to"/> is null.</exception>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="from"/> or <paramref name="to"/> is malformed.</exception>
        public VEmail(MailAddress from, IEnumerable<MailAddress> to)
        {
            this.Init();
            this.From = from;
            this.To.AddRangeUnique(to);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VEmail"/> class.
        /// </summary>
        /// <param name="from">The senders of the e-mail address.</param>
        /// <param name="to">The recipient of the e-mail address.</param>
        public VEmail(string from, string to)
            : base(from, to)
        {
            this.Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VEmail"/> class.
        /// </summary>
        /// <param name="from">The senders of the e-mail address.</param>
        /// <param name="to">The recipient of the e-mail address.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        public VEmail(string from, string to, string subject, string body)
            : base(from, to, subject, body)
        {
            this.Init();
        }

        /// <summary>
        /// Gets or sets the resolve default senders email.
        /// </summary>
        /// <value>
        /// The resolve default senders email.
        /// </value>
        public static Func<string> ResolveSendersEmail { get; set; }

        /// <summary>
        /// Gets or sets the resolve SMTP server.
        /// </summary>
        /// <value>
        /// The resolve SMTP server.
        /// </value>
        public static Func<string> ResolveSmtpServer { get; set; }

        /// <summary>
        /// Gets or sets the on exception.
        /// </summary>
        /// <value>
        /// The on exception.
        /// </value>
        public Action<Exception> OnException { get; set; }

        /// <summary>
        /// Gets or sets the senders email.
        /// </summary>
        /// <value>
        /// The senders email.
        /// </value>
        public string SendersEmail
        {
            get
            {
                return string.Concat(this.From);
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.From = new MailAddress(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the SMTP client.
        /// </summary>
        /// <value>
        /// The SMTP client.
        /// </value>
        public SmtpClient SmtpClient { get; set; }

        #region IValidate Members

        /// <summary>
        /// Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>True if valid otherwise false</returns>
        public bool Validate()
        {
            return this.SmtpClient != null && this.From.IsNotNull() && this.To.IsNotNull() && !string.IsNullOrWhiteSpace(this.Body) && !string.IsNullOrWhiteSpace(this.Subject);
        }

        #endregion

        /// <summary>
        /// Sends this Email message.
        /// </summary>
        /// <returns>True if success otherwise false</returns>
        public VEmailResponse SendMail()
        {
            var response = new VEmailResponse();

            if (this.Validate())
            {
                try
                {
                    this.SmtpClient.Send(this);

                    response.Success = true;
                    response.Response = "Success";
                }
                catch (InvalidOperationException invalidoperationexception)
                {
                    this.AddEmailExceptionGenericInfo(invalidoperationexception);

                    if (this.OnException != null)
                    {
                        this.OnException(invalidoperationexception);
                    }

                    response.Response = invalidoperationexception.Message;
                }
                catch (SmtpFailedRecipientsException failedrecipients)
                {
                    this.AddEmailExceptionGenericInfo(failedrecipients);

                    if (this.OnException != null)
                    {
                        this.OnException(failedrecipients);
                    }

                    response.Response = failedrecipients.Message;
                    response.SmtpStatusCode = failedrecipients.StatusCode;
                }
                catch (SmtpException smtpexception)
                {
                    this.AddEmailExceptionGenericInfo(smtpexception);

                    if (this.OnException != null)
                    {
                        this.OnException(smtpexception);
                    }

                    response.Response = smtpexception.Message;
                    response.SmtpStatusCode = smtpexception.StatusCode;
                }
                catch (Exception ex)
                {
                    ex.Data["Logger.Severity.Error"] = string.Format("Failed to send Email: From: {0}, To: {1}, Subject: {2}", this.From, this.To, this.Subject);

                    if (this.OnException != null)
                    {
                        this.OnException(ex);
                    }

                    response.Response = ex.Message;
                }
            }
            else
            {
                if (this.From.IsNotNull())
                {
                    response.Response = "From address is missing! ";
                }

                if (this.To.IsNotNull())
                {
                    response.Response += "To address is missing! ";
                }

                if (this.Subject.IsNotNullOrEmpty())
                {
                    response.Response += "The message subject is missing! ";
                }

                if (this.Body.IsNotNullOrEmpty())
                {
                    response.Response += "The message body is missing! ";
                }

                if (this.SmtpClient == null)
                {
                    response.Response += "The server SmtpClient is missing! ";
                }
            }

            return response;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.MailMessage"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this.SmtpClient != null)
            {
                this.SmtpClient.Dispose();
            }
        }

        /// <summary>
        /// Gets the SMTP client.
        /// </summary>
        /// <param name="smtpserver">The SMTP server.</param>
        /// <returns>
        /// The SmtpClient instance
        /// </returns>
        private static SmtpClient GetSmtpClient(string smtpserver)
        {
            if (!string.IsNullOrWhiteSpace(smtpserver))
            {
                return new SmtpClient(smtpserver);
            }

            /* Use 
                <system.net>
                <mailSettings>
                    <smtp deliveryMethod="network">
                    <network
                        host="localhost"
                        port="25"
                        defaultCredentials="true"
                    />
                    </smtp>
                </mailSettings>
                </system.net>
             */

            return new SmtpClient();
        }

        /// <summary>
        /// Gets the default senders email.
        /// </summary>
        /// <returns>The default senders email.</returns>
        private static MailAddress GetDefaultSendersEmail()
        {
            string setting = ResolveSendersEmail != null
                ? ResolveSendersEmail()
                : WebConfigurationManager.AppSettings["Vodca.VEmail.DefaultSendersEmail"];

            Ensure.IsNotNullOrEmpty(setting, "Vodca.VEmail.DefaultSendersEmail");

            return new MailAddress(setting);
        }

        /// <summary>
        /// Gets the SMTP server.
        /// </summary>
        /// <returns>The SMTP server.</returns>
        private static string GetDefaultSmtpServer()
        {
            string setting = ResolveSmtpServer != null
                            ? ResolveSmtpServer()
                            : WebConfigurationManager.AppSettings["Vodca.VEmail.SmtpServer"];

            return setting;
        }

        /// <summary>
        /// Initialize default settings.
        /// </summary>
        private void Init()
        {
            this.BodyEncoding = System.Text.Encoding.UTF8;
            this.IsBodyHtml = true;
            this.SmtpClient = GetSmtpClient(GetDefaultSmtpServer());

            this.OnException += VLog.LogException;
        }

        /// <summary>
        /// Adds the email exception generic info.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private void AddEmailExceptionGenericInfo(Exception exception)
        {
            exception.Data["Logger.Severity.Error.EmailFrom"] = this.From;
            exception.Data["Logger.Severity.Error.EmailTo"] = this.To;
            exception.Data["Logger.Severity.Error.EmailSubject"] = this.Subject;
        }
    }
}