using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class ChunkParser
    {
        internal static ResultContainer ParseChunks(this IEnumerable<GedcomChunk> chunks)
        {
            var resultContainer = new ResultContainer();

            foreach (var chunk in chunks.OrderBy(Priority))
            {
                switch (chunk.Type)
                {
                    case "FAM":
                        resultContainer.ParseFamily(chunk);
                        break;

                    case "INDI":
                        resultContainer.ParsePerson(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "SOUR":
                        resultContainer.ParseSource(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "REPO":
                        resultContainer.ParseRepository(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "NOTE":
                    case "OBJE":
                    case "SUBM":
                    case "SUBN":
                        resultContainer.AddIdChunk(chunk);
                        break;

                    // Deliberately skipped for now
                    case "HEAD":
                    case "TRLR":
                    case "CSTA": // Child status; used as 'enum' by Reunion software
                        break;

                    case "_GRP":
                    case "_PLC":
                    case "GEDC":
                    default:
                        resultContainer.Errors.Add($"Failed to handle top level Type='{chunk.Type}'");
                        break;
                }
            }

            return resultContainer;
        }

        private static int Priority(GedcomChunk chunk)
        {
            switch (chunk.Type)
            {
                case "REPO":
                    return 0;
                case "SOUR":
                    return 1;
                case "NOTE":
                    return 2;
                case "INDI":
                    return 3;
                case "FAM":
                    return 4;
                default:
                    return 0;
            }
        }
    }
}