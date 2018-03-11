# Introduction
This library for .NET uses a fluent interface of chained methods for building complete Console
Applications. Software Developers often reinvent command-line interfaces for their tools, which
is very easy until the application's interface becomes too complex to be modified effectively.
This tool is an elegant and quick way to do this and let developers focus of higher tasks.

An application can be used in many ways:

- Directly from the code, as in the following usage example
- Called with arguments provided from the console
- In a Read-Evaluate-Parse Loop (REPL)

# Usage example

**Defining an application**, with all its commands.

```C#
var application = FluentConsoleApplication.Create("Calculator", "Application to calculate.")
  .DefineCommand("add", "Add two numbers")
    .WithParameter<int>("X", "First operand")
    .WithParameter<int>("Y", "Second operand")
      .Does(args => Console.WriteLine("Total is " + (args.X + args.Y)))
  .DefineCommand("mult", "Multiply two numbers")
    .WithParameter<double>("X", "First operand")
    .WithParameter<double>("Y", "Second operand")
      .Does(args => Console.WriteLine("Total is " + (args.X * args.Y)));
  .DefineCommand("help")
      .Does(args => Console.WriteLine(args.Application.GetDocumentation()));
```

**Using the application** by invoking a command with its required arguments, if any:

```C#
application.Run("add 5 2");
```

Output:

```
Total is 7
```

**Automatic documentation** can also be generated for an Application. This documentation is
based on the names, types and descriptions provided when the application was defined.

In this example, the `help` command generates documentation for the currently running
application.

Output:

```
Calculator: Application to calculate.
 - add [X] [Y] - Add two numbers
 - mult [X] [Y] - Multiply two numbers
 - help
```