using System.Management.Automation;

namespace Arvan.PowerShell.ProfileCmdlets.Account
{
    [OutputType(typeof(string))]
    [Cmdlet(VerbsCommunications.Connect, "ArAccount")]
    public class ConnectArAccount : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject($"Logging in...");
        }
    }
}