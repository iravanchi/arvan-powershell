using System.Management.Automation;
using Arvan.PowerShell.Common.Cmdlet;
using Arvan.PowerShell.Common.Utils;
using Arvan.Proxy.Products.IaaS;

namespace Arvan.PowerShell.ComputeCmdlets.FloatIp
{
    [OutputType(typeof(FloatingIpInfo))]
    [Cmdlet(VerbsCommon.Get, "ArFloatIp")]
    public class GetArFloatIp : ArvanCmdlet
    {
        protected override void ProcessRecord()
        {
            var result = Arvan.IaaS.GetFloatingIpList().EnsureSuccess();

            if (result.Data == null)
                return;
            
            foreach (var info in result.Data)
            {
                WriteObject(info);
            }
        }

    }
}