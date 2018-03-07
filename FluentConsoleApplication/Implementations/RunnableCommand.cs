using System;
using System.Collections.Generic;
using System.Dynamic;

namespace FluentConsoleApplication
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

        public void Run(IEnumerable<string> arguments)
        {
            dynamic packedArguments = new ExpandoObject();
            var keyValueArguments = packedArguments as IDictionary<string, object>;

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
