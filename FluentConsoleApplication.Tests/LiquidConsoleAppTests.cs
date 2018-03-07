using FluentAssertions;
using NUnit.Framework;
using System.Collections;

namespace FluentConsoleApplication.Tests
{
    [TestFixture]
    public class LiquidConsoleApplicationTests
    {
        [Test]
        public void LiquidConsoleApp_DefineConsoleApp_TotalOfCalculation()
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
    }
}
