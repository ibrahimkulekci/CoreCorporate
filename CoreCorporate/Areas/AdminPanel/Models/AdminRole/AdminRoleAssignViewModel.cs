using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.AdminRole
{
    public class AdminRoleAssignViewModel
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }
    }
}
