using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentConsole
{
    /// <inheritdoc/>
    public class FluentConsoleApplication : IFluentConsoleApplication
    {
        public IEnumerable<IRunnableCommand> RunnableCommands { get; }

        public string Name { get; }
        public string Description { get; }

        public FluentConsoleApplication(string applicationName, string applicationDescription)
        {
            Name = applicationName;
            Description = applicationDescription;
            RunnableCommands = Enumerable.Empty<IRunnableCommand>();
        }

        public FluentConsoleApplication(IFluentConsoleApplication application, IRunnableCommand command)
            : this(application.Name, application.Description)
        {
            RunnableCommands = application.RunnableCommands.Concat(new[] { command });
        }

        public FluentConsoleApplication(IRunnableCommand runnableCommand) 
            : this(runnableCommand.DefinedCommand.Application, runnableCommand)
        {
        }

        public static IFluentConsoleApplication Create(string applicationName, string applicationDescription = null)
        {
            return new FluentConsoleApplication(applicationName, applicationDescription);
        }

        public void Run(string fullCommand)
        {
            // TOOD: consider quotes when splitting
            var tokens = fullCommand.Split();

            Run(tokens);
        }

        public void Run(IEnumerable<string> tokens)
        {
            // TODO: validate full command's string format
            var command = tokens.First();
            var arguments = tokens.Skip(1);
            var runnableCommand = RunnableCommands.FirstOrDefault(cmd => cmd.DefinedCommand.Name.Equals(command));

            if (runnableCommand == null) throw new InvalidOperationException("Command does not exist.");

            runnableCommand.Run(arguments, this);
        }

        public string GetDocumentation()
        {
            var output = new StringBuilder();
            output.Append(ToString() + Environment.NewLine);

            foreach(var runnableCommand in RunnableCommands)
            {
                output.Append($" - {runnableCommand.DefinedCommand.GetUsageDocumentation(includeDescription: true)}{Environment.NewLine}" );
            }

            return output.ToString();
        }

        public override string ToString()
        {
            if(string.IsNullOrWhiteSpace(Description))
            {
                return $"{Name}";
            } else
            {
                return $"{Name}: {Description}";
            }
        }
    }
}
