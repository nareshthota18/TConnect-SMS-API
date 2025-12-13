using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Common.DTO
{
    public class NotificationAuditDTO
    {
        public bool SeenAt { get; set; } = false;
        public bool ReadAt { get; set; } = false;
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
