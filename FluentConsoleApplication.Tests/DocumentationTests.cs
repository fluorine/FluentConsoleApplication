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
        #region Usage documentation

        [Test]
        public void DefinedParameter_GetUsageDocumentation()
        {
            // ARRANGE
            var definedParameter = new DefinedParameter<int>("test", "Parameter with testing int value", str => 5);

            // ACT
            string documentation = definedParameter.GetUsageDocumentation();

            // ASSERT
            documentation.Should().BeEquivalentTo("[test]");
        }

        [Test]
        public void DefinedCommand_GetUsageDocumentation()
        {
            // ARRANGE
            var application = FluentConsoleApplication.Create("Application")
                .DefineCommand("add", "Add two numbers")
                  .WithParameter<int>("X", "First operand")
                  .WithParameter<int>("Y", "Second operand")
                    .Does(args => Console.WriteLine("Total is " + (args.X + args.Y)));

            IDefinedCommand definedCommad = application.RunnableCommands.First().DefinedCommand;

            // ACT
            string documentation = definedCommad.GetUsageDocumentation();

            // ASSERT
            documentation.Should().BeEquivalentTo("add [X] [Y]");
        }

        #endregion

        #region Detail documentation

        [Test]
        public void DefinedParameter_GetFullDetailDocumentation()
        {
            // ARRANGE
            IDefinedParameter definedParameter = new DefinedParameter<int>("test", "Parameter with testing int value", str => 5);

            // ACT
            string documentation = definedParameter.GetDetailDocumentation();

            // ASSERT
            documentation.Should().BeEquivalentTo("[test] (Int32) Parameter with testing int value");
        }

        [Test]
        public void DefinedParameter_GetDetailDocumentationWithoutDescription()
        {
            // ARRANGE
            IDefinedParameter definedParameter = new DefinedParameter<int>("test", null, str => 5);

            // ACT
            string documentation = definedParameter.GetDetailDocumentation();

            // ASSERT
            documentation.Should().BeEquivalentTo("[test] (Int32)");
        }


        [Test]
        public void DefinedParameter_GetDetailDocumentationWithoutDescriptionNorType()
        {
            // ARRANGE
            IDefinedParameter definedParameter = new DefinedParameter<int>("test", null, str => 5);

            // ACT
            string documentation = definedParameter.GetDetailDocumentation(includeType: false);

            // ASSERT
            documentation.Should().BeEquivalentTo("[test]");
        }

        [Test]
        public void DefinedParameter_GetDetailDocumentationWithDescriptionWithoutType()
        {
            // ARRANGE
            IDefinedParameter definedParameter = new DefinedParameter<int>("test", "Parameter with testing int value", str => 5);

            // ACT
            string documentation = definedParameter.GetDetailDocumentation();

            // ASSERT
            documentation.Should().BeEquivalentTo("[test] (Int32) Parameter with testing int value");
        }

        #endregion

    }
}
