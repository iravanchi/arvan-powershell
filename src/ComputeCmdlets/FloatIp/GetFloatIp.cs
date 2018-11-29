using System.Management.Automation;
using Arvan.PowerShell.Context.Authentication;
using Arvan.Proxy;
using Arvan.Proxy.Authorization;
using Arvan.Proxy.Products.IaaS;

namespace Arvan.PowerShell.ComputeCmdlets.FloatIp
{
    [OutputType(typeof(FloatingIpInfo))]
    [Cmdlet(VerbsCommon.Get, "ArFloatIp")]
    public class GetFloatIp : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var arvan = new ArvanClient(new ApiKeyRequestAuthorization(AuthenticationContext.Current.ApiKey));
            var result = arvan.IaaS.GetFloatingIpList().GetAwaiter().GetResult();

            if (result == null)
            {
                WriteDebug("NULL Result");
                return;
            }
            
            if (!result.Success)
            {
                WriteDebug("Not successful");
                return;
            }
            
            foreach (var info in result.Result.Data)
            {
                WriteObject(info);
            }
        }
    }
}