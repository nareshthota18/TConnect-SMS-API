using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Data.Models.Others
{
    [Table("Notification", Schema = "rsms")]
    public class Notification
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
    }
}
