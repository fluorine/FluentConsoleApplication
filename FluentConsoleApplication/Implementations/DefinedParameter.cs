using System;

namespace FluentConsole
{
    /// <inheritdoc/>
    public class DefinedParameter<T> : IDefinedParameter
    {
        public string Name { get; }
        public string Description { get; }

        public Type Type => typeof(T);

        private Func<string, T> parser;

        public DefinedParameter(string parameterName, string parameterDescription, Func<string, T> parser)
        {
            Name = parameterName;
            Description = parameterDescription;

            this.parser = parser;
        }

        public object GetValue(string value)
        {
            return parser(value);
        }
    }
}
