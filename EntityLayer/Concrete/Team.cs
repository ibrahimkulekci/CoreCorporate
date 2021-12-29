﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamPosition { get; set; }
        public string TeamDescription { get; set; }
        public string TeamImage { get; set; }
        public bool TeamStatus { get; set; }
        public DateTime TeamCreatedDate { get; set; }
        public DateTime TeamUpdatedDate { get; set; }
    }
}
