using System;
using McMaster.Extensions.CommandLineUtils;

namespace MrBogomips.JiraCLI
{
  partial class Program
    {
        [Option(Description="The subject")]
        public string Subject {get;}

        private void OnExecute()
        {
            var subject = Subject ?? "world";
            Console.WriteLine($"Hello {subject}");
        }
    }
}
