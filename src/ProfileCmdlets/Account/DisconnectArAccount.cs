using System.Management.Automation;
using Arvan.PowerShell.Context.Authentication;

namespace Arvan.PowerShell.ProfileCmdlets.Account
{
    [OutputType(typeof(string))]
    [Cmdlet(VerbsCommunications.Disconnect, "ArAccount", SupportsShouldProcess = true)]
    public class DisconnectArAccount : Cmdlet
    {
        private bool _persistent;

        [Parameter]
        public SwitchParameter Persistent
        {
            get => _persistent;
            set => _persistent = value;
        }

        protected override void ProcessRecord()
        {
            if (!ShouldProcess("account", "clear"))
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
            return new AuthenticationContextEntity
            {
                ApiKey = null,
                BearerToken = null
            };
        }
    }
}