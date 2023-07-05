using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers
{
    public static class NameParser
    {
        internal static Name ParseName(this ResultContainer resultContainer, GedcomChunk chunk)
        {
            var name = new Name();

            // If the PERSONAL_NAME_STRUCTURE contains PERSON_NAME_PIECES (GIVN and SURN) then parse those
            if (chunk.SubChunks.Any(c => c.Type == "GIVN") && chunk.SubChunks.Any(c => c.Type == "SURN"))
            {
                name = new Name
                {
                    GivenNames = chunk.SubChunks.FirstOrDefault(c => c.Type == "GIVN")?.Data,
                    Surname = chunk.SubChunks.FirstOrDefault(c => c.Type == "SURN")?.Data
                };
            } else {
                // Otherwise parse the surname / family name using the "/" separator
                var nameParts = chunk.Data.Split("/");
                if (nameParts.Length > 1)
                {
                    name = new Name
                    {
                        GivenNames = nameParts[0].Trim(),
                        Surname = nameParts[1].Trim()
                    };
                }
            }

            return name;
        }
    }
}