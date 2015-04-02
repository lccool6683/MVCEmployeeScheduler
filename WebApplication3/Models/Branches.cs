using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCEmployeeScheduler.Models
{
    public partial class Branches
    {
        
        public string ParentBranch { get; set; }
        [Required]
        [StringLength(30)]
        [Key]
        //[System.Web.Mvc.Remote("IsExist", "Place", ErrorMessage = "name already exist!")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}