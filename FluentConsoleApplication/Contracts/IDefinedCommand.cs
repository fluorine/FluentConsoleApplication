using System.Collections.Generic;

namespace FluentConsoleApplication
{
    /// <summary>
    /// Defined commmand to be used in the application.
    /// </summary>
    public interface IDefinedCommand
    {
        /// <summary>
        /// Previous Application's instance associated to this command.
        /// </summary>
        IFluentConsoleApplication Application { get; }

        /// <summary>
        /// Name of the command, used by the user to invoke it.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Brief description of the command.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Parameters define the arguments required for this command to run.
        /// </summary>
        IEnumerable<IDefinedParameter> Parameters { get; }
    }
}
