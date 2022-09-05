using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers
{
    public static class SourceParser
    {
        internal static void ParseSource(this ResultContainer resultContainer, GedcomChunk sourChunk)
        {
            var source = new Source {Id = sourChunk.Id};

            foreach (var chunk in sourChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "TITL":
                        source.Title = chunk.Data;
                        break;

                    case "ABBR":
                        source.AbbreviatedTitle = chunk.Data;
                        break;

                    case "AUTH":
                        source.Author = chunk.Data;
                        break;

                    case "PUBL":
                        source.Publisher = chunk.Data;
                        source.Published = resultContainer.ParseDatePlace(chunk);
                        break;
                    
                    case "REPO":
                        var repository = resultContainer.Repositories.Find(r => r.Id == chunk.Reference);
                        if (repository != null)
                        {
                            var repositorySource = new RepositorySource { Repository = repository, Source = source };
                            resultContainer.RepositorySources.Add(repositorySource);
                        }
                        break;

                    // Deliberately skipped for now
                    case "DATA":
                    case "NAME":
                    case "TEXT":
                        resultContainer.Warnings.Add($"Skipped Source Type='{chunk.Type}'");
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Source Type='{chunk.Type}'");
                        break;
                }
            }

            resultContainer.Sources.Add(source);
        }

        internal static void ParseSourceCitation(this ResultContainer resultContainer, GedcomChunk sourChunk, Person person, string eventType)
        {
            var source = resultContainer.Sources.Find(s => s.Id == sourChunk.Reference);
            if (source != null)
            {
                var sourceCitation = new SourceCitation { Person = person, Source = source, EventType = eventType };

                foreach (var chunk in sourChunk.SubChunks)
                {
                    switch (chunk.Type)
                    {
                        case "PAGE":
                            sourceCitation.Page = chunk.Data;
                            break;
                        
                        case "DATA":
                            sourceCitation.Date = chunk.SubChunks.SingleOrDefault(c => c.Type == "DATE")?.Data;
                            sourceCitation.Note = chunk.SubChunks.SingleOrDefault(c => c.Type == "NOTE")?.Data;
                            break;

                        // Deliberately skipped for now
                        case "_APID":
                            resultContainer.Warnings.Add($"Skipped SourceCitation Type='{chunk.Type}'");
                            break;
                            
                        default:
                            resultContainer.Errors.Add($"Failed to handle SourceCitation Type='{chunk.Type}'");
                            break;
                    }
                }

                resultContainer.SourceCitations.Add(sourceCitation);
            }
        }
    }
}