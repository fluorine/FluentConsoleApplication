using FluentAssertions;
using NUnit.Framework;
using System.Collections;

namespace FluentConsoleApplication.Tests
{
    [TestFixture]
    public class FluentConsoleApplicationTests
    {
        [Test]
        public void FluentConsoleApplication_CommandsFullyDefined_ExpectedResults()
        {
            // ARRANGE
            var numberA = 5;
            var numberB = 2;

            int sum = 0;
            double product = 0;
            IFluentConsoleApplication app =
                FluentConsoleApplication.Create("Calculator", "Use this application for math.")
                  .DefineCommand("add", "Add two numbers")
                    .WithParameter<int>("X", "First operand", input => int.Parse(input))
                    .WithParameter<int>("Y", "Second operand", input => int.Parse(input))
                      .Does(args => sum = args.X + args.Y)
                  .DefineCommand("mult", "Multiply two numbers")
                    .WithParameter<double>("X", "First operand", input => double.Parse(input))
                    .WithParameter<double>("Y", "Second operand", input => double.Parse(input))
                      .Does(args => product = args.X * args.Y);

            // ACT
            //   Emulate user input
            app.Run($"add {numberA} {numberB}");
            app.Run($"mult {numberA} {numberB}");

            // ASSERT
            sum.Should().Be(numberA + numberB);
            product.Should().BeApproximately(numberA * numberB, precision: 0.01);
        }

        [Test]
        public void FluentConsoleApplication_CommandsFullyDefinedRunWithTokens_ExpectedResults()
        {
            // ARRANGE
            var numberA = 5;
            var numberB = 2;

            int sum = 0;
            double product = 0;
            IFluentConsoleApplication app =
                FluentConsoleApplication.Create("Calculator", "Use this application for math.")
                  .DefineCommand("add", "Add two numbers")
                    .WithParameter<int>("X", "First operand", input => int.Parse(input))
                    .WithParameter<int>("Y", "Second operand", input => int.Parse(input))
                      .Does(args => sum = args.X + args.Y)
                  .DefineCommand("mult", "Multiply two numbers")
                    .WithParameter<double>("X", "First operand", input => double.Parse(input))
                    .WithParameter<double>("Y", "Second operand", input => double.Parse(input))
                      .Does(args => product = args.X * args.Y);

            // ACT
            //   Emulate user input
            app.Run(new string[] { "add", numberA.ToString(), numberB.ToString()});
            app.Run(new string[] { "mult", numberA.ToString(), numberB.ToString() });

            // ASSERT
            sum.Should().Be(numberA + numberB);
            product.Should().BeApproximately(numberA * numberB, precision: 0.01);
        }

        [Test]
        public void FluentConsoleApplication_CommandsWithImpliedParsingDelegate_TotalOfCalculation()
        {
            // ARRANGE
            var numberA = 5;
            var numberB = 2;

            long sum = 0;
            double product = 0;
            IFluentConsoleApplication app =
                FluentConsoleApplication.Create("Calculator", "Use this application for math.")
                  .DefineCommand("add", "Add two numbers")
                    .WithParameter<int>("X", "First operand")
                    .WithParameter<long>("Y", "Second operand")
                      .Does(args => sum = args.X + args.Y)
                  .DefineCommand("mult", "Multiply two numbers")
                    .WithParameter<double>("X", "First operand")
                    .WithParameter<decimal>("Y", "Second operand")
                      .Does(args => product = args.X * (double)args.Y);

            // ACT
            //   Emulate user input
            app.Run($"add {numberA} {numberB}");
            app.Run($"mult {numberA} {numberB}");

            // ASSERT
            sum.Should().Be(numberA + numberB);
            product.Should().BeApproximately(numberA * numberB, precision: 0.01);
        }
		
		[Test]
        public void FluentConsoleApplication_MethodsInvokedWithoutAnyDescriptionProvided_ExpectedResults()
        {
            // ARRANGE
            var numberA = 5;
            var numberB = 2;

            int sum = 0;
            double product = 0;
            IFluentConsoleApplication app =
                FluentConsoleApplication.Create("Calculator")
                  .DefineCommand("add")
                    .WithParameter("X", input => int.Parse(input))
                    .WithParameter<int>("Y", input => int.Parse(input))
                      .Does(args => sum = args.X + args.Y)
                  .DefineCommand("mult")
                    .WithParameter<double>("X")
                    .WithParameter("Y", input => double.Parse(input))
                      .Does(args => product = args.X * args.Y);

            // ACT
            //   Emulate user input
            app.Run($"add {numberA} {numberB}");
            app.Run($"mult {numberA} {numberB}");

            // ASSERT
            sum.Should().Be(numberA + numberB);
            product.Should().BeApproximately(numberA * numberB, precision: 0.01);
        }
		
		[Test]
        public void FluentConsoleApp_ParameterlessCommand_ActionIsRun()
        {
            // ARRANGE
			var actionRun = false;
            var app =
                FluentConsoleApplication.Create("test", "Use this application for testing.")
                  .DefineCommand("test", "A test command")
                      .Does(args => actionRun = true);

            // ACT
            //   Emulate user input
            app.Run($"test");

            // ASSERT
            actionRun.Should().BeTrue();
        }
		
		[Test]
        public void FluentConsoleApp_SimpleNonParsedTextParameters_ExpectedResult()
        {
            // ARRANGE
			var fullName = string.Empty;
            var app =
                FluentConsoleApplication.Create("Name", "Get a full name")
                  .DefineCommand("fullname", "Get a full name")
                    .WithParameter("FirstName", "First Name")
                    .WithParameter("LastName") // Description omitted
                      .Does(args => fullName = $"{args.FirstName} {args.LastName}");

            // ACT
            //   Emulate user input
            app.Run("fullname Joseph Anglada");

            // ASSERT
			fullName.Should().BeEquivalentTo("Joseph Anglada");
        }
    }
}
