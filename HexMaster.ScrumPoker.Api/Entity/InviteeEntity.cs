using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexMaster.ScrumPoker.Api.Entity
{
    public class InviteeEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}
