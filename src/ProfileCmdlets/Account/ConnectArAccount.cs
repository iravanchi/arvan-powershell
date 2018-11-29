using System;
using System.Management.Automation;
using System.Security;
using Arvan.PowerShell.Context.Authentication;

namespace Arvan.PowerShell.ProfileCmdlets.Account
{
    [OutputType(typeof(string))]
    [Cmdlet(VerbsCommunications.Connect, "ArAccount", SupportsShouldProcess = true, DefaultParameterSetName = "ApiKey")]
    public class ConnectArAccount : Cmdlet
    {
        private const string ApiKeyPrefix = "Apikey ";
        private const string BearerPrefix = "Bearer ";
        
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "ApiKey", HelpMessage = "The API Key connected to the account, provided by Arvan")]
        public string ApiKey { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "Bearer")]
        public string BearerToken { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "Username")]
        public string Username { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ParameterSetName = "Username")]
        public SecureString Password { get; set; }

        private bool _persistent;

        [Parameter]
        public SwitchParameter Persistent
        {
            get => _persistent;
            set => _persistent = value;
        }

        protected override void ProcessRecord()
        {
            if (!string.IsNullOrWhiteSpace(Username))
            {
                throw new NotImplementedException("Using username and password login is not supported yet.");                
            }
            
            if (!ShouldProcess("account", "set"))
                return;

            if (_persistent)
            {
                AuthenticationContext.PersistentContext = BuildAuthenticationContextEntity();
            }
            else
            {
                AuthenticationContext.InMemoryContext = BuildAuthenticationContextEntity();
            }
        }

        private AuthenticationContextEntity BuildAuthenticationContextEntity()
        {
            if (!string.IsNullOrWhiteSpace(ApiKey) &&
                ApiKey.StartsWith(ApiKeyPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                ApiKey = ApiKey.Substring(ApiKeyPrefix.Length);
            }

            if (!string.IsNullOrWhiteSpace(BearerToken) &&
                BearerToken.StartsWith(BearerPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                BearerToken = BearerToken.Substring(BearerPrefix.Length);
            }
            
            return new AuthenticationContextEntity
            {
                ApiKey = ApiKey,
                BearerToken = BearerToken
            };
        }
    }
}