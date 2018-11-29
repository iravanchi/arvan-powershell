using System;
using Arvan.PowerShell.Common.Authentication;
using Arvan.Proxy;
using Arvan.Proxy.Authorization;

namespace Arvan.PowerShell.Common.Cmdlet
{
    public class ArvanCmdlet : System.Management.Automation.Cmdlet
    {
        private ArvanClient _arvan;
        
        protected ArvanClient Arvan => _arvan ?? (_arvan = BuildArvanClient());

        protected virtual bool AuthenticationRequired => true;

        private ArvanClient BuildArvanClient()
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
            
            if (authorization == null && AuthenticationRequired)
                throw new InvalidOperationException("Authentication information is not present. Use Connect-ArAccount to set authentication context before invoking this Cmdlet.");
            
            return new ArvanClient(authorization);
        }
    }
}