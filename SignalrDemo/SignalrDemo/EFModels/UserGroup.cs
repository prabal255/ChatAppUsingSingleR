using System;
using System.Collections.Generic;

#nullable disable

namespace SignalrDemo.EFModels
{
    public partial class UserGroup
    {
        public int UserGroupId { get; set; }
        public int? UseId { get; set; }
        public int? GroupId { get; set; }
    }
}
