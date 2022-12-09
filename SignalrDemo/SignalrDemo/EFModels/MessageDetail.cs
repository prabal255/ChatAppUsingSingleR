using System;
using System.Collections.Generic;

#nullable disable

namespace SignalrDemo.EFModels
{
    public partial class MessageDetail
    {
        public int MessageId { get; set; }
        public int? SentBy { get; set; }
        public string Message { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Person SentByNavigation { get; set; }
    }
}
