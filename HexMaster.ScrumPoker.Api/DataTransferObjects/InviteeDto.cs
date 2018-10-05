using System;
using HexMaster.Helpers.DomainModels;

namespace HexMaster.ScrumPoker.Api.DataTransferObjects
{
    public class InviteeDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }


    }
}
