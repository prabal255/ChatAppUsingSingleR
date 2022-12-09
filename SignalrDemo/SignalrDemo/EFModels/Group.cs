using System;
using System.Collections.Generic;

#nullable disable

namespace SignalrDemo.EFModels
{
    public partial class Group
    {
        public Group()
        {
            MessageDetails = new HashSet<MessageDetail>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<MessageDetail> MessageDetails { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
