using System;
using System.IO;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace MrBogomips.JiraCLI
{
  [HelpOption("--help")]
  abstract class CommandBase {
    protected ILogger Logger {get;}
    protected IConsole Console {get;}

    /*
    [Option(CommandOptionType.SingleValue, 
      ShortName="p", LongName="profile", ValueName = "profile name",
      Description = "local profile name", 
      ShowInHelpText = true)] */
    [Option("-p|--profile <profile_name>", "Local profile name", CommandOptionType.SingleValue)]
    public string Profile {get; set; } = Constants.DefaultProfile;

    protected CommandBase(ILogger logger, IConsole console) {
      Logger = logger;
      Console = console;
    }

    protected virtual Task<int> OnExecute(CommandLineApplication app) {
      return Task.FromResult(0);
    }

    protected static string ProfileFolder
    {
      get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.Create), $".{Constants.AppName}");
    }

    protected static string GetProfileFilePath(string file) 
    => Path.Combine(ProfileFolder, file);
  } 
} 