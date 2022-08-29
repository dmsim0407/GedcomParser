using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class PersonParser
    {
        internal static void ParsePerson(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var person = new Person {Id = indiChunk.Id};

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "_UID":
                        person.Uid = chunk.Data;
                        break;

                    case "ADOP":
                        person.Adoption = resultContainer.ParseAdoption(chunk);
                        break;

                    case "BAPM":
                        person.Baptized = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "BIRT":
                        person.Birth = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "BURI":
                        person.Buried = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "CHAN":
                        person.Changed = chunk.ParseDateTime();
                        break;

                    case "CHR":
                        person.Baptized = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "DEAT":
                        person.Death = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "EDUC":
                        person.Education = chunk.Data;
                        break;

                    case "EMIG":
                        person.Emigrated.Add(resultContainer.ParseDatePlace(chunk, person));
                        break;

                    case "FACT":
                        person.Note = resultContainer.ParseNote(person.Note, chunk);
                        break;

                    case "GRAD":
                        person.Graduation = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "HEAL":
                        person.Health = chunk.Data;
                        break;

                    case "IDNO":
                        person.IdNumber = chunk.Data;
                        break;

                    case "IMMI":
                        person.Immigrated.Add(resultContainer.ParseDatePlace(chunk, person));
                        break;

                    case "NAME":
                        person.Name = resultContainer.ParseName(chunk);
                        break;

                    case "NATU":
                        person.BecomingCitizen = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "NOTE":
                        person.Note = resultContainer.ParseNote(person.Note, chunk);
                        break;

                    case "OCCU":
                        person.Occupation = chunk.Data;
                        break;

                    case "RESI":
                        person.Residence = resultContainer.ParseDatePlace(chunk, person);
                        break;

                    case "RELI":
                        person.Religion = chunk.Data;
                        break;

                    case "SEX":
                        person.Gender = chunk.Data;
                        break;

                    case "TITL":
                        person.Title = chunk.Data;
                        break;

                    // Deliberately skipped for now
                    case "_GRP":
                    case "CONF":
                    case "DSCR":
                    case "EVEN":
                    case "FAMS":
                    case "FAMC":
                    case "HIST":
                    case "NCHI":
                    case "NMR":
                    case "OBJE":
                    case "PAGE":
                    case "RIN":
                    case "SOUR":
                        resultContainer.Warnings.Add($"Skipped Person Type='{chunk.Type}'");
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Person Type='{chunk.Type}'");
                        break;
                }
            }

            resultContainer.Persons.Add(person);
        }
    }
}