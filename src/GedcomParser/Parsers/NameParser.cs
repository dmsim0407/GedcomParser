using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers
{
    public static class NameParser
    {
        internal static Name ParseName(this ResultContainer resultContainer, GedcomChunk chunk)
        {
            var name = new Name
            {
                GivenNames = chunk.SubChunks.FirstOrDefault(c => c.Type == "GIVN")?.Data,
                Surname = chunk.SubChunks.FirstOrDefault(c => c.Type == "SURN")?.Data
            };

            return name;
        }
    }
}