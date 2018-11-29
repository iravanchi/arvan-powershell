using System;
using System.Management.Automation;
using Arvan.PowerShell.Common.Authentication;
using Arvan.Proxy;
using Arvan.Proxy.Authorization;
using Arvan.Proxy.Products.IaaS;

namespace Arvan.PowerShell.ComputeCmdlets.FloatIp
{
    [OutputType(typeof(FloatingIpInfo))]
    [Cmdlet(VerbsCommon.Get, "ArFloatIp")]
    public class GetArFloatIp : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var arvan = GetArvanClient();
            var result = arvan.IaaS.GetFloatingIpList().EnsureSuccess();

            if (result.Data == null)
                return;
            
            foreach (var info in result.Data)
            {
                WriteObject(info);
            }
        }

        private ArvanClient GetArvanClient()
        {
            RequestAuthorizationBase authorization = null;
            var authenticationContext = AuthenticationContext.Current;
            
            if (authenticationContext != null)
            {
                if (!string.IsNullOrWhiteSpace(authenticationContext.ApiKey))
                    authorization = new ApiKeyRequestAuthorization(authenticationContext.ApiKey);
                else if (!string.IsNullOrWhiteSpace(authenticationContext.BearerToken))
                    authorization = new BearerRequestAuthorization(authenticationContext.BearerToken);
            }
            
            if (authorization == null)
                throw new InvalidOperationException("Authentication information is not present. Use Connect-ArAccount to set authentication context before invoking this Cmdlet.");
            
            return new ArvanClient(authorization);
        }
    }
}