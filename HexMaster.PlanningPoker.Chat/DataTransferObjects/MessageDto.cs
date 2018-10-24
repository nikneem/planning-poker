using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexMaster.PlanningPoker.Chat.DataTransferObjects
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public bool IsNew { get; set; }
    }
}
