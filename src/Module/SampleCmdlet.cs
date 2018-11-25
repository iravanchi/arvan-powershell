using System.Management.Automation;

namespace Arvan.PowerShell.Module
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
    [OutputType(typeof(string))]
    [Cmdlet(VerbsCommon.Get, "Greeting")]
    public class SampleCmdlet : Cmdlet
    {
        /// <summary>
        ///     The name of the person to greet.
        /// </summary>
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the person to greet")]
        public string Name { get; set; }

        /// <summary>
        ///     Perform Cmdlet processing.
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteObject($"Hello, {Name}!");
        }
    }
}