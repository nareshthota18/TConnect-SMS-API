using RSMS.Data.Models.SecurityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Data.Models.Others
{
    [Table("NotificationAudit", Schema = "rsms")]
    public class NotificationAudit : BaseEntity
    {
        public Guid? RSHostelId { get; set; }
        public Guid? NotificationId { get; set; }
        public bool SeenAt { get; set; } = false;
        public bool ReadAt { get; set; } = false;

        [ForeignKey(nameof(NotificationId))]
        public Notification? Notification { get; set; }
    }
}
