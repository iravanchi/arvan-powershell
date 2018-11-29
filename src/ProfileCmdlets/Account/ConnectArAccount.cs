using System.Management.Automation;
using Arvan.PowerShell.Context.Authentication;

namespace Arvan.PowerShell.ProfileCmdlets.Account
{
    [OutputType(typeof(string))]
    [Cmdlet(VerbsCommunications.Connect, "ArAccount", SupportsShouldProcess = true)]
    public class ConnectArAccount : Cmdlet
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, HelpMessage = "The API Key connected to the account, provided by Arvan")]
        public string ApiKey { get; set; }

        protected override void ProcessRecord()
        {
            if (!ShouldProcess("account", "set"))
                return;
            
            AuthenticationContext.PersistentContext = new AuthenticationContextEntity
            {
                ApiKey = ApiKey,
                BearerToken = null
            };
        }
    }
}