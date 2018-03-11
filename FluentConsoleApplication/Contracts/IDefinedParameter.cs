using System;

namespace FluentConsole
{
    /// <summary>
    /// A parameter with a name, description and type associated.
    /// </summary>
    public interface IDefinedParameter
    {
        /// <summary>
        /// Name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of the parameter.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Type of parameter.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Return a parameter as <see cref="object"/>, provided a <see cref="string"/> value.
        /// </summary>
        /// <param name="inputValue">Parameter's input value as <see cref="string"/>.</param>
        /// <returns><see cref="object"/> parsed from the provided <param cref="value"/>.</returns>
        object GetValue(string inputValue);
    }
}
