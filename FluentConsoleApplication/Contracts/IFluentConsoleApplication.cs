﻿using System.Collections.Generic;

namespace FluentConsoleApplication
{
    /// <summary>
    /// A Fluent Console Application
    /// </summary>
    public interface IFluentConsoleApplication
    {
        /// <summary>
        /// Name of the application.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Brief and general description of the application.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Commands found in this Application.
        /// </summary>
        IEnumerable<IRunnableCommand> RunnableCommands { get; }

        /// <summary>
        /// Run any command related to <<see cref="this"/> application.
        /// </summary>
        /// <param name="fullCommand">Raw input <see cref="string"/> for a command and arguments</param>
        void Run(string fullCommand);
    }
}
