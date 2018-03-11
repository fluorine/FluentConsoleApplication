using System;
using System.Collections.Generic;

namespace FluentConsole
{
    public interface IRunnableCommand
    {
        /// <summary>
        /// The command associated to <see cref="this"/>'s <see cref="Action"/>.
        /// </summary>
        IDefinedCommand DefinedCommand { get; }

        /// <summary>
        /// Action performed by this command. Arguments are
        /// privided as fields of a dynamic object generated at runtime.
        /// </summary>
        Action<dynamic> Action { get; }

        /// <summary>
        /// Run this <see cref="IRunnableCommand"/> with arguments as unparsed
        /// <see cref="string"/> tokens.
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="application">Application running this <see cref="IRunnableCommand"/></param>
        void Run(IEnumerable<string> arguments, IFluentConsoleApplication application);
    }
}
