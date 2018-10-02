using System;
using System.Collections.Generic;
using System.Linq;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;

namespace HexMaster.ScrumPoker.Api.DomainModels
{
    public class ProductBacklogItem : DomainModelBase<Guid>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkUrl { get; set; }
        public bool IsRefined { get; set; }
        public int StoryPoints { get; set; }
        public double? Average => Votes?.Average(x => x.StoryPoints);
        public List<Vote> Votes { get; set; }

        public ProductBacklogItem(Guid id, string name, string title, string description, string link, bool isRefined,
            int storyPoints, List<Vote> votes) : base(id)
        {
            Name = name;
            Title = title;
            Description = description;
            LinkUrl = link;
            IsRefined = isRefined;
            StoryPoints = storyPoints;
            Votes = votes;
        }
    }
}
