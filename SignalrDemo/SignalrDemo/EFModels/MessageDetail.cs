using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SignalrDemo.EFModels
{
    public partial class MessageDetail
    {
        [Key]
        public int MessageId { get; set; }
        public int? SentBy { get; set; }
        public string Message { get; set; }
        public int? GroupId { get; set; }

        public virtual Person SentByNavigation { get; set; }
    }
}
