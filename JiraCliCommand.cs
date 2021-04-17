using static System.StringComparison;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MrBogomips.JiraCLI
{
    [Command(Name=Constants.AppName, OptionsComparison = InvariantCultureIgnoreCase)]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption("-h|-?|--help")] 
    class JiraCliCommand : CommandBase
    {

        public JiraCliCommand(ILogger<JiraCliCommand> logger, IConsole console)
        :base(logger, console) {

        }

        protected override Task<int> OnExecute(CommandLineApplication app) {
            app.ShowHelp();
            Console.WriteLine($"Profile folder: {ProfileFolder}");
            Console.WriteLine($"Profile filename: {GetProfileFilePath("ciccio.json")}");
            return Task.FromResult(0);
        }

        private static string GetVersion() 
            => typeof(JiraCliCommand).Assembly.GetName().Version.ToString();
    }
}