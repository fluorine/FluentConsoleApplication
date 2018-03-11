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

        /// <summary>
        /// Get usage documentation for a <see cref="IDefinedParameter"/>
        /// </summary>
        /// <param name="includeType">Include type as part of documentation</param>
        /// <param name="includeDescription">Include description as part of documentation, if provided</param>
        /// <returns></returns>
        string GetUsageDocumentation(bool includeType = true, bool includeDescription = true);
    }
}
