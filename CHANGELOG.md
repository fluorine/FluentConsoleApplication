# v0.2.0 beta
March 11, 2018
- Generate user documentation for the Application
- Generate user documentation for `DefinedCommand` and `DefinedParameter`
- Add automatic dynamic field as argument, called `Application`, with a
  pointer to the current Application being run.

# v0.1.0 beta
March 10, 2018, Minimum Viable Product
- Define a Command-line Application, with name and description, using `FluentConsoleApplication.Create` factory method.
- Define an application's commands with name and description, using the `DefineCommand` method
- Define command's parameters with name, description and parsing delegate, using the `WithParameter<T>` method
- Bind an action to a command using the `Does` method
- Run a `FluentConsoleApplication` instance using the `Run` method
