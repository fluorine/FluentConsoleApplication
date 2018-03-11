using System;

namespace FluentConsole
{
    /// <summary>
    /// Extension methods required to chain multiple methods
    /// used to define a full <see cref="FluentConsoleApplication"/>
    /// using a fluent sintax.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Define a command for a <see cref="IFluentConsoleApplication"/>.
        /// </summary>
        /// <param name="application">Application to add the command being defined</param>
        /// <param name="commandName">Command's name</param>
        /// <param name="commandDescription">Command's description</param>
        /// <returns></returns>
        public static IDefinedCommand DefineCommand(this IFluentConsoleApplication application, string commandName, string commandDescription = null)
        {
            return new DefinedCommand(application, commandName, commandDescription);
        }

        /// <summary>
        /// Define a parameter for a <see cref="IDefinedCommand"/> 
        /// </summary>
        /// <typeparam name="T">Type that the user's argument shall be parsed to.</typeparam>
        /// <param name="definedCommand"><see cref="IDefinedCommand"/> that will have the parameter being created</param>
        /// <param name="parameterName">Name of the parameter to be created</param>
        /// <param name="parameterDescription">Description of the parameter to be created</param>
        /// <param name="parameterParser">Logic to convert a <see cref="string"/>
        /// as input argument into <typeparamref name="T"/></param>
        /// <returns></returns>
        public static IDefinedCommand WithParameter<T>(this IDefinedCommand definedCommand, string parameterName, string parameterDescription, Func<string, T> parameterParser)
        {
            var definedParameter = new DefinedParameter<T>(parameterName, parameterDescription, parameterParser);
            return new DefinedCommand(definedCommand, definedParameter);
        }

        /// <summary>
        /// Define a parameter for a <see cref="IDefinedCommand"/> without any description.
        /// </summary>
        /// <typeparam name="T">Type that the user's argument shall be parsed to.</typeparam>
        /// <param name="definedCommand"><see cref="IDefinedCommand"/> that will have the parameter being created</param>
        /// <param name="parameterName">Name of the parameter to be created</param>
        /// <param name="parameterParser">Logic to convert a <see cref="string"/>
        /// as input argument into <typeparamref name="T"/></param>
        /// <returns></returns>
        public static IDefinedCommand WithParameter<T>(this IDefinedCommand definedCommand, string parameterName, Func<string, T> parameterParser)
        {
            var definedParameter = new DefinedParameter<T>(parameterName, null, parameterParser);
            return new DefinedCommand(definedCommand, definedParameter);
        }
        
        /// <summary>
        /// Define a parameter for a <see cref="IDefinedCommand"/> without any parsing delegate or type.
        /// </summary>
        /// <param name="definedCommand"><see cref="IDefinedCommand"/> that will have the parameter being created</param>
        /// <param name="parameterName">Name of the parameter to be created</param>
        /// <param name="parameterDescription">Description of the parameter to be created</param>
        /// <returns></returns>
        public static IDefinedCommand WithParameter(this IDefinedCommand definedCommand, string parameterName, string parameterDescription = null)
        {
            return WithParameter(definedCommand, parameterName, parameterDescription, input => input);
        }

        /// <summary>
        /// Define a parameter for a <see cref="IDefinedCommand"/> with optional description and implicit value conversion.
        /// </summary>
        /// <typeparam name="T">Type that the user's argument shall be parsed to.</typeparam>
        /// <param name="definedCommand"><see cref="IDefinedCommand"/> that will have the parameter being created</param>
        /// <param name="parameterName">Name of the parameter to be created</param>
        /// <param name="valueType">Optional parameter to get the type, not intended to be used directly by the user</param>
        /// <returns></returns>
        public static IDefinedCommand WithParameter<T>(this IDefinedCommand definedCommand, string parameterName, string parameterDescription = null, T valueType = default(T))
        {
            var type = typeof(T);
            return WithParameter(definedCommand, parameterName, parameterDescription, input => (T)Convert.ChangeType(input, typeof(T)));
        }

        /// <summary>
        /// Link an action to a <see cref="IDefinedCommand"/>
        /// </summary>
        /// <param name="definedCommand"></param>
        /// <param name="commandAction"></param>
        /// <returns></returns>
        public static IFluentConsoleApplication Does(this IDefinedCommand definedCommand, Action<dynamic> commandAction)
        {
            IRunnableCommand runnableCommand = new RunnableCommand(definedCommand, commandAction);

            return new FluentConsoleApplication(runnableCommand);
        }
    }
}
