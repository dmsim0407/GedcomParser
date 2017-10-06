﻿using System.IO;
using System.Linq;
using Gedcom.Entities;
using Gedcom.Entities.Gedcom;
using Gedcom.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcom.Test
{
    [TestClass]
    public class ImportTests
    {
        [TestMethod]
        public void CanParseLine()
        {
            // Arrange
            const string line = "3 ADDR 7108 South Pine Cone Street";

            // Act
            var gedcomLine = GedcomLine.Parse(line);

            // Assert
            Assert.AreEqual(3, gedcomLine.Level);
            Assert.AreEqual("ADDR", gedcomLine.Type);
            Assert.AreEqual("7108 South Pine Cone Street", gedcomLine.Data);
        }

        [TestMethod]
        public void CanParseShortLine()
        {
            // Arrange
            const string line = "3 ADDR";

            // Act
            var gedcomLine = GedcomLine.Parse(line);

            // Assert
            Assert.AreEqual(3, gedcomLine.Level);
            Assert.AreEqual("ADDR", gedcomLine.Type);
            Assert.AreEqual(null, gedcomLine.Data);
        }

        [TestMethod]
        public void CanParse()
        {
            // Arrange
            var currentDir = Directory.GetCurrentDirectory();
            var baseDir = currentDir.Substring(0, currentDir.IndexOf(@"\bin\Debug"));
            var filePath = Path.Combine(baseDir, "Resources", "Windsor.ged");
            //var filePath = Path.Combine(baseDir, "Resources", "Kennedy.ged");
            //var filePath = Path.Combine(baseDir, "Resources", "TGC551.ged");
            var gedcomParser = new GedcomParser();

            // Act
            gedcomParser.Parse(filePath);

            // Assert
            Assert.IsTrue(gedcomParser.Persons.Count > 0);
            Assert.IsTrue(gedcomParser.Relations.Count > 0);
            var childStatusChildren = gedcomParser.Relations.OfType<ChildRelation>().Where(c => !string.IsNullOrEmpty(c.Pedigree)).ToList();
            Assert.IsTrue(childStatusChildren.Count > 0);
        }
    }
}
