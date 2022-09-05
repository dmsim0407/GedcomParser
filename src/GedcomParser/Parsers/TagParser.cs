using GedcomParser.Entities;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers
{
    public static class TagParser
    {
        internal static void ParseTag(this ResultContainer resultContainer, GedcomChunk tagChunk)
        {
            var tag = new Tag {Id = tagChunk.Id};

            foreach (var chunk in tagChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NAME":
                        tag.Name = chunk.Data;
                        break;

                    case "NOTE":
                        tag.Note = chunk.Data;
                        break;

                    // Deliberately skipped for now
                    case "RIN":
                        resultContainer.Warnings.Add($"Skipped Tag Type='{chunk.Type}'");
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Tag Type='{chunk.Type}'");
                        break;
                }
            }

            resultContainer.Tags.Add(tag);
        }
    }
}