using System;
using System.Linq;
using GedcomParser.Services;
using GedcomParser.Test.Extensions;
using GedcomParser.Test.Services;
using Shouldly;
using Xunit;


namespace GedcomParser.Test
{
    public class CustomSampleTests
    {
        [Fact]
        public void CanParseKennedyFamily()
        {
            // Arrange
            var lines = ResourceHelper.GetLines("CustomSample.Kennedy.ged");

            // Act
            var result = FileParser.ParseLines(lines);

            // Assert
            result.Errors.ShouldBeEmptyWithFeedback();
            // result.Warnings.ShouldBeEmptyWithFeedback();
            result.Warnings.Count.ShouldBe(4);
            result.Warnings.ShouldContain("Skipped Person Type='FAMC'");
            result.Warnings.ShouldContain("Skipped Person Type='FAMS'");
            result.Warnings.ShouldContain("Skipped Person Type='HIST'");
        }

        [Fact]
        public void CanParseStefanFamily()
        {
            // Arrange
            var lines = ResourceHelper.GetLines("CustomSample.Stefan.ged");

            // Act
            var result = FileParser.ParseLines(lines);

            // Assert
            // result.Errors.ShouldBeEmptyWithFeedback();
            result.Errors.Count.ShouldBe(4);
            result.Errors.ShouldContain("Failed to handle top level Type='_GRP'");
            result.Errors.ShouldContain("Failed to handle top level Type='_PLC'");
            // result.Warnings.ShouldBeEmptyWithFeedback();
            result.Warnings.Count.ShouldBe(524);
        }

        [Fact]
        public void CanParseWindsorFamily()
        {
            // Arrange
            var lines = ResourceHelper.GetLines("CustomSample.Windsor.ged");

            // Act
            var result = FileParser.ParseLines(lines);

            // Assert
            result.Errors.ShouldBeEmptyWithFeedback();
            // result.Warnings.ShouldBeEmptyWithFeedback();
            result.Warnings.Count.ShouldBe(1);
            result.Warnings.ShouldContain("Skipped Person Type='OBJE'");
        }

        [Fact]
        public void CanParseMultipleImmigrationEventsFamily()
        {
            // Arrange
            var lines = ResourceHelper.GetLines("CustomSample.MultipleImmigrationEvents.ged");

            // Act
            var result = FileParser.ParseLines(lines);

            // Assert
            result.Errors.ShouldBeEmptyWithFeedback();
            result.Warnings.ShouldContain("Skipped Person Type='FAMS'");
            Assert.Collection(result.Persons, person => { Assert.Equal("Travis", person.Name.GivenNames.Trim()); Assert.Equal(2, person.Immigrated.Count); },
                                              person => { Assert.Equal("Niles", person.Name.GivenNames.Trim()); Assert.Empty(person.Immigrated); });            
        }

        [Fact]
        public void CanParseMultipleEmigrationEventsFamily()
        {
            // Arrange
            var lines = ResourceHelper.GetLines("CustomSample.MultipleEmigrationEvents.ged");

            // Act
            var result = FileParser.ParseLines(lines);

            // Assert
            result.Errors.ShouldBeEmptyWithFeedback();
            result.Warnings.ShouldContain("Skipped Person Type='FAMS'");
            Assert.Collection(result.Persons, person => { Assert.Equal("Travis", person.Name.GivenNames.Trim()); Assert.Equal(2, person.Emigrated.Count); },
                                              person => { Assert.Equal("Niles", person.Name.GivenNames.Trim()); Assert.Empty(person.Emigrated); });
        }

        [Fact]
        public void CanParseSimpsonMcDowellFamily()
        {
            // Arrange
            var lines = ResourceHelper.GetLines("CustomSample.SimpsonMcDowell.ged");

            // Act
            var result = FileParser.ParseLines(lines);

            // Assert
            result.Errors.Count.ShouldBe(9);
            result.Errors.ShouldContain("Failed to handle Family Type='_SREL'");
            result.Errors.ShouldContain("Failed to handle Family Type='ENGA'");
            result.Errors.ShouldContain("Failed to handle Note Type='OBJE'");
            result.Errors.ShouldContain("Failed to handle Note Type='TYPE'");
            result.Errors.ShouldContain("Failed to handle Person Type='_MILT'");
            result.Errors.ShouldContain("Failed to handle Person Type='CREM'");
            result.Errors.ShouldContain("Failed to handle Person Type='MARR'");
            result.Errors.ShouldContain("Failed to handle Source Type='_APID'");
            result.Errors.ShouldContain("Failed to handle Source Type='NOTE'");

            result.Warnings.Count.ShouldBe(8);
            result.Warnings.ShouldContain("Skipped Note Type='SOUR'");
            result.Warnings.ShouldContain("Skipped Person Type='EVEN'");
            result.Warnings.ShouldContain("Skipped Person Type='FAMC'");
            result.Warnings.ShouldContain("Skipped Person Type='FAMS'");
            result.Warnings.ShouldContain("Skipped Person Type='OBJE'");
            result.Warnings.ShouldContain("Skipped Person Type='SOUR'");
            result.Warnings.ShouldContain("Skipped SourceCitation Type='_APID'");
            result.Warnings.ShouldContain("Skipped Tag Type='RIN'");

            result.Persons.Count.ShouldBe(362);
            result.Sources.Count.ShouldBe(63);
            result.Repositories.Count.ShouldBe(14);
            result.ChildRelations.Count.ShouldBe(477);
            result.SiblingRelations.Count.ShouldBe(830);
            result.SpouseRelations.Count.ShouldBe(110);
            result.RepositorySources.Count.ShouldBe(63);
            result.SourceCitations.Count.ShouldBe(461);
            result.Tags.Count.ShouldBe(23);
            result.PersonTags.Count.ShouldBe(124);
        }

        [Fact]
        public void CanParsePeterSimpsonFamilyTree()
        {
            // Arrange
            var lines = ResourceHelper.GetLines("CustomSample.PeterSimpsonFamilyTree.ged");

            // Act
            var result = FileParser.ParseLines(lines);

            // Assert
            result.Errors.Count.ShouldBe(5);

            result.Warnings.Count.ShouldBe(45);
        }
    }
}