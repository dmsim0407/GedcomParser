﻿using System.Collections.Generic;


namespace GedcomParser.Entities
{
    public class Result
    {
        public List<Person> Persons { get; set; }
        public List<ChildRelation> ChildRelations { get; set; }
        public List<SpouseRelation> SpouseRelations { get; set; }
        public List<SiblingRelation> SiblingRelations { get; set; }
        public List<Source> Sources { get; set; }
        public List<CitationSource> CitationSources { get; set; }
        public List<Repository> Repositories { get; set; }
        public List<SourceRepository> SourceRepositories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PersonTag> PersonTags { get; set; }
        public HashSet<string> Warnings { get; set; }
        public HashSet<string> Errors { get; set; }

        public Result()
        {
            Persons = new List<Person>();
            ChildRelations = new List<ChildRelation>();
            SpouseRelations = new List<SpouseRelation>();
            SiblingRelations = new List<SiblingRelation>();
            Sources = new List<Source>();
            CitationSources = new List<CitationSource>();
            Repositories = new List<Repository>();
            SourceRepositories = new List<SourceRepository>();
            Tags = new List<Tag>();
            PersonTags = new List<PersonTag>();
            Warnings = new HashSet<string>();
            Errors = new HashSet<string>();
        }
    }
}