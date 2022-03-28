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
    public static class CertUtils
    {
        /// <summary>
        /// Common client certificate autjentication middleware
        /// </summary>
        /// <param name="options"></param>
        /// <param name="validClientCertThumbprint"></param>
        /// <param name="validIntermediateThumbprint"></param>
        /// <param name="validCAThumbprint"></param>
        /// <param name="loggerFunction">Pass your logger function (using an <see cref="Action"/> makes the method reusable with many different loggers)</param>
        public static void CertClientAuth(
            CertificateAuthenticationOptions options,
            string validClientCertThumbprint,
            string validIntermediateThumbprint,
            string validCAThumbprint,
            Action<string, string> loggerFunction)
        {
            options.AllowedCertificateTypes = CertificateTypes.Chained;
            options.RevocationMode = X509RevocationMode.NoCheck;
            options.ValidateValidityPeriod = true;
            options.Events = new CertificateAuthenticationEvents
            {

                OnAuthenticationFailed = context =>
                {
                    loggerFunction(context.Exception.Message, context.Exception.ToString());
                    context.Fail("Invalid certificate");
                    return Task.CompletedTask;
                },
                OnCertificateValidated = context =>
                {
                    var cert = context.ClientCertificate;
                    if (cert == null)
                    {
                        loggerFunction("Certificate not present", null);
                        context.Fail("Invalid certificate");
                        return Task.CompletedTask;
                    }
                    var x509Chain = new X509Chain();
                    x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                    x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

                    if (!x509Chain.Build(cert) ||
                        x509Chain.ChainElements[0].Certificate.Thumbprint != validClientCertThumbprint ||
                        x509Chain.ChainElements[1].Certificate.Thumbprint != validIntermediateThumbprint ||
                        x509Chain.ChainElements[2].Certificate.Thumbprint != validCAThumbprint)
                    {
                        loggerFunction("Invalid certificate chain", string.Join("\n", x509Chain.ChainStatus));
                        context.Fail("Invalid certificate");
                        return Task.CompletedTask;
                    }

                    context.Success();
                    return Task.CompletedTask;
                }
            };
        }
    }
}
