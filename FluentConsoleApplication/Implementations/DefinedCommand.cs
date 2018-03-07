using System.Collections.Generic;
using System.Linq;

namespace FluentConsoleApplication
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
    }
}
