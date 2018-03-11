using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentConsole
{
    /// <inheritdoc/>
    public class DefinedCommand : IDefinedCommand
    {
        public string Name { get; }
        public string Description { get; }

        public IFluentConsoleApplication Application { get; }

        public IEnumerable<IDefinedParameter> Parameters { get; }

        public DefinedCommand(IFluentConsoleApplication application, string commandName, string commandDescription)
        {
            Application = application;
            Name = commandName;
            Description = commandDescription;
            Parameters = Enumerable.Empty<IDefinedParameter>();
        }

        public DefinedCommand(IDefinedCommand definedCommand, IDefinedParameter definedParameter)
            : this(definedCommand.Application, definedCommand.Name, definedCommand.Description)
        {
            Parameters = definedCommand.Parameters.Concat(new[] { definedParameter });
        }

        public string GetUsageDocumentation()
        {
            var usageDocumentation = new StringBuilder();
            usageDocumentation.Append(Name);

            if (Parameters.Any())
            {
                usageDocumentation.Append(" ");

                var parametersUsages = new List<string>();
                foreach (var parameter in Parameters)
                {
                    parametersUsages.Add(parameter.GetUsageDocumentation());
                }

                usageDocumentation.Append(string.Join(" ", parametersUsages));
            }

            return usageDocumentation.ToString();
        }
    }
}
