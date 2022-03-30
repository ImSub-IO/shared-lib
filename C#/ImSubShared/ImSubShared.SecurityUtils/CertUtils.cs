using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ImSubShared.SecurityUtils
{
    /// <summary>
    /// Wrapper class for some common certificate authentication actions
    /// </summary>
    public static class CertUtils
    {
        /// <summary>
        /// Action for handling additional client cert validation actions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="validClientCertThumbprint"></param>
        /// <param name="validIntermediateThumbprint"></param>
        /// <param name="validCAThumbprint"></param>
        /// <param name="loggerFunction">Pass your logger function (using an <see cref="Action"/> makes the method reusable with many different loggers)</param>
        public static Task ClientCertAuthHandler(
            CertificateValidatedContext context,
            string validClientCertThumbprint,
            string validIntermediateThumbprint,
            string validCAThumbprint,
            Action<string, string> loggerFunction)
        {
            var cert = context.ClientCertificate;
            if (cert == null)
            {
                string message = "Certificate not present";
                loggerFunction(message, null);
                context.Fail(message);
                return Task.CompletedTask;
            }

            var x509Chain = new X509Chain();
            x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

            if (!x509Chain.Build(cert))
            {
                var message = "Invalid certificate chain";
                loggerFunction(message, string.Join("\n", x509Chain.ChainStatus));
                context.Fail(message);
                return Task.CompletedTask;
            }

            // Thumbprints
            string clientCertThumbprint = x509Chain.ChainElements[0].Certificate.Thumbprint;
            string intermediateCertThumbprint = x509Chain.ChainElements[1].Certificate.Thumbprint;
            string rootCertThumbprint = x509Chain.ChainElements[2].Certificate.Thumbprint;

            // Checks
            bool clientCertThumbprintMatch = clientCertThumbprint == validClientCertThumbprint;
            bool intermediateCertThumbprintMatch = intermediateCertThumbprint == validIntermediateThumbprint;
            bool rootCertThumbprintMatch = rootCertThumbprint == validCAThumbprint;

            if ( !clientCertThumbprintMatch || !intermediateCertThumbprintMatch || !rootCertThumbprintMatch)
            {
                var message = "One or more certificate's thumbprint in the chain doesn't match";
                var details = BuildDetailsString(clientCertThumbprint, intermediateCertThumbprint, rootCertThumbprint,
                                                 clientCertThumbprintMatch, intermediateCertThumbprintMatch, rootCertThumbprintMatch);
                loggerFunction(message, details);
                context.Fail(message);
                return Task.CompletedTask;
            }

            context.Success();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Action for handling error in client cert validation
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggerFunction">Pass your logger function (using an <see cref="Action"/> makes the method reusable with many different loggers)</param>
        /// <returns></returns>
        public static Task ClientCertAuthErrorHandler(CertificateAuthenticationFailedContext context, Action<string, string> loggerFunction)
        {
            loggerFunction(context.Exception.Message, context.Exception.ToString());
            context.Fail("Invalid certificate");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Setup basic <see cref="CertificateAuthenticationOptions"/>
        /// </summary>
        /// <param name="options"></param>
        public static void SetupCertAuthOptions(CertificateAuthenticationOptions options)
        {
            options.AllowedCertificateTypes = CertificateTypes.Chained;
            options.RevocationMode = X509RevocationMode.NoCheck;
            options.ValidateValidityPeriod = true;
        }

        private static string BuildDetailsString(string clientCertThumbprint, string intermediateCertThumbprint, string rootCertThumbprint,
                                               bool clientCertThumbprintMatch, bool intermediateCertThumbprintMatch, bool rootCertThumbprintMatch)
        {
            var details = new StringBuilder();
            details.AppendFormat("ClientCertThumbprintMatch: {0} -- Thumbprint of the given client certificate: {1}\n", clientCertThumbprintMatch, clientCertThumbprint);
            details.AppendFormat("IntermediateCertThumbprintMatch: {0} -- Thumbprint of the given intermediate certificate: {1}\n", intermediateCertThumbprintMatch, intermediateCertThumbprint);
            details.AppendFormat("RootCertThumbprintMatch: {0} -- Thumbprint of the root given certificate: {1}", rootCertThumbprintMatch, rootCertThumbprint);
            return details.ToString();
        }
    }
}
