using System;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Utils;

namespace GedcomParser.Parsers
{
    public static class DateParser
    {
        internal static DatePlace ParseDatePlace(this ResultContainer resultContainer, GedcomChunk chunk, Person[] people = null)
        {
            var datePlace = new DatePlace
            {
                DateAsString = chunk.SubChunks.SingleOrDefault(c => c.Type == "DATE")?.Data,
                Place = chunk.SubChunks.SingleOrDefault(c => c.Type == "PLAC")?.Data
            };
            var map = chunk.SubChunks.SingleOrDefault(c => c.Type == "MAP");
            if (map != null)
            {
                datePlace.Latitude = map.SubChunks.SingleOrDefault(c => c.Type == "LATI")?.Data;
                datePlace.Longitude = map.SubChunks.SingleOrDefault(c => c.Type == "LONG")?.Data;
            }

            var note = chunk.SubChunks.SingleOrDefault(c => c.Type == "NOTE");
            if (note != null)
            {
                datePlace.Note = resultContainer.ParseNote(datePlace.Note, note);
            }

            if (people != null)
            {
                foreach (var sourceCitation in chunk.SubChunks.FindAll(c => c.Type == "SOUR"))
                {
                    foreach (var person in people)
                    {
                        resultContainer.ParseSourceCitation(sourceCitation, person, chunk.Type);
                    }
                }
            }

            if (datePlace.DateAsString != null)
            {
                datePlace.Date = ConvertToDate(resultContainer, datePlace.DateAsString);
            }
            return datePlace;
        }

        internal static string ParseDateTime(this GedcomChunk chunk)
        {
            return (chunk.SubChunks.SingleOrDefault(c => c.Type == "DATE")?.Data + " " + chunk.SubChunks.SingleOrDefault(c => c.Type == "TIME")?.Data).Trim();
        }

        private static DateOnly? ConvertToDate(ResultContainer resultContainer, string dateAsString)
        {
            var date = DateConvertor.ConvertToDate(dateAsString);

            if (date != null) {
                return date;
            } else {
                resultContainer.Warnings.Add($"{dateAsString} is not a supported date format or valid date");
                return null;
            }
        }
    }
}