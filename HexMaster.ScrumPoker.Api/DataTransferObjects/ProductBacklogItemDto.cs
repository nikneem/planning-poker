using System;
using System.Collections.Generic;
using System.Linq;
using HexMaster.Helpers.DomainModels;

namespace HexMaster.ScrumPoker.Api.DataTransferObjects
{
    public class ProductBacklogItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkUrl { get; set; }
        public bool IsRefined { get; set; }
        public int? StoryPoints { get; set; }
        public List<VoteDto> Votes { get; set; }


    }
}
