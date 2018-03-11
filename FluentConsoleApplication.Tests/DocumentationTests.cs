using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace FluentConsole.Tests
{
    [TestFixture]
    public class DocumentationTests
    {
        #region Get Usage documentation for DefinedCommand

        [Test]
        public void DefinedCommand_GetUsageDocumentationWithDescription()
        {
            // ARRANGE
            var application = FluentConsoleApplication.Create("Application")
                .DefineCommand("add", "Add two numbers")
                  .WithParameter<int>("X", "First operand")
                  .WithParameter<int>("Y", "Second operand")
                    .Does(args => Console.WriteLine("Total is " + (args.X + args.Y)));

            IDefinedCommand definedCommad = application.RunnableCommands.First().DefinedCommand;

            // ACT
            string documentation = definedCommad.GetUsageDocumentation(includeDescription: true);

            // ASSERT
            documentation.Should().BeEquivalentTo("add [X] [Y] - Add two numbers");
        }

        [Test]
        public void DefinedCommand_GetUsageDocumentationWithoutDescription()
        {
            // ARRANGE
            var application = FluentConsoleApplication.Create("Application")
                .DefineCommand("add", "Add two numbers")
                  .WithParameter<int>("X", "First operand")
                  .WithParameter<int>("Y", "Second operand")
                    .Does(args => Console.WriteLine("Total is " + (args.X + args.Y)));

            IDefinedCommand definedCommad = application.RunnableCommands.First().DefinedCommand;

            // ACT
            string documentation = definedCommad.GetUsageDocumentation(includeDescription: false);

            // ASSERT
            documentation.Should().BeEquivalentTo("add [X] [Y]");
        }

        #endregion

        #region Get Usage documentation for DefinedParameter

        [Test]
        public void DefinedParameter_GetUsageDocumentationWithDescriptionAndType()
        {
            // ARRANGE
            IDefinedParameter definedParameter = new DefinedParameter<int>("test", "Parameter with testing int value", str => 5);

            // ACT
            string documentation = definedParameter.GetUsageDocumentation(includeType: true);

            // ASSERT
            documentation.Should().BeEquivalentTo("[test] (Int32) Parameter with testing int value");
        }

        [Test]
        public void DefinedParameter_GetUsageDocumentationWithoutDescription()
        {
            // ARRANGE
            IDefinedParameter definedParameter = new DefinedParameter<int>("test", null, str => 5);

            // ACT
            string documentation = definedParameter.GetUsageDocumentation();

            // ASSERT
            documentation.Should().BeEquivalentTo("[test] (Int32)");
        }

        [Test]
        public void DefinedParameter_GetUsageDocumentationWithoutType()
        {
            // ARRANGE
            IDefinedParameter definedParameter = new DefinedParameter<int>("test", "Parameter with testing int value", str => 5);

            // ACT
            string documentation = definedParameter.GetUsageDocumentation(includeType: false, includeDescription: true);

            // ASSERT
            documentation.Should().BeEquivalentTo("[test] Parameter with testing int value");
        }

        [Test]
        public void DefinedParameter_GetUsageDocumentationWithoutDescriptionNorType()
        {
            // ARRANGE
            var definedParameter = new DefinedParameter<int>("test", "Parameter with testing int value", str => 5);

            // ACT
            string documentation = definedParameter.GetUsageDocumentation(includeType: false, includeDescription: false);

            // ASSERT
            documentation.Should().BeEquivalentTo("[test]");
        }

        #endregion

        [Test]
        public void DefinedApplication_GetApplicationDocumentation()
        {
            // ARRANGE
            var application = FluentConsoleApplication.Create("Calculator", "Used to calculate.")
              .DefineCommand("add", "Add two numbers")
                .WithParameter<int>("X", "First operand")
                .WithParameter<int>("Y", "Second operand")
                  .Does(args => Console.WriteLine("Total is " + (args.X + args.Y)))
              .DefineCommand("mult", "Multiply two numbers")
                .WithParameter<double>("X", "First operand")
                .WithParameter<double>("Y", "Second operand")
                  .Does(args => Console.WriteLine("Total is " + (args.X * args.Y)));

            // ACT
            var applicationUsageDocumentation = application.GetDocumentation();

            // ASSERT
            var expectedDocumentation =
                  "Calculator: Used to calculate." + Environment.NewLine
                + " - add [X] [Y] - Add two numbers" + Environment.NewLine
                + " - mult [X] [Y] - Multiply two numbers" + Environment.NewLine;

            applicationUsageDocumentation.Should().BeEquivalentTo(expectedDocumentation);
        }
    }
}
