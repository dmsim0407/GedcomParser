using GedcomParser.Entities;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers
{
    public static class RepositoryParser
    {
        internal static void ParseRepository(this ResultContainer resultContainer, GedcomChunk repoChunk)
        {
            var repository = new Repository {Id = repoChunk.Id};

            foreach (var chunk in repoChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NAME":
                        repository.Name = chunk.Data;
                        break;

                    case "ADDR":
                        repository.Address = resultContainer.ParseAddress(chunk);
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Repository Type='{chunk.Type}'");
                        break;
                }
            }

            resultContainer.Repositories.Add(repository);
        }
    }
}