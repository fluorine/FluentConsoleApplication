using System;
using System.Collections.Generic;
using System.Dynamic;

namespace FluentConsole
{
    /// <inheritdoc/>
    internal class RunnableCommand : IRunnableCommand
    {
        public IDefinedCommand DefinedCommand { get; }
        public Action<dynamic> Action { get; }

        public RunnableCommand(IDefinedCommand definedCommand, Action<dynamic> commandAction)
        {
            DefinedCommand = definedCommand;
            Action = commandAction;
        }

        public void Run(IEnumerable<string> arguments, IFluentConsoleApplication application)
        {
            dynamic packedArguments = new ExpandoObject();
            var keyValueArguments = packedArguments as IDictionary<string, object>;

            // Add current application as part of the arguments
            keyValueArguments.Add("Application", application);

            // Add parameters as fields and associate provided values
            var parametersEnumerator = DefinedCommand.Parameters.GetEnumerator();
            var argumentsEnumerator = arguments.GetEnumerator();

            while (parametersEnumerator.MoveNext() && argumentsEnumerator.MoveNext())
            {
                var currentParameter = parametersEnumerator.Current;
                var argumentType = currentParameter.Type;
                var argumentName = currentParameter.Name;
                var argumentValue = Convert.ChangeType(currentParameter.GetValue(argumentsEnumerator.Current), argumentType);
                
                keyValueArguments.Add(argumentName, argumentValue);
            }

            // Run
            Action(packedArguments);
        }
    }
}
