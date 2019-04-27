using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomesticManagement.Database.Entities
{

    public class Role : IdentityRole<Guid>
    {
        public Role(string roleName) : base(roleName)
        {

        }
        public Role()
        {

        }
        public string Description { get; set; }
        [NotMapped]
        public string SelectedPrivileges { get; set; }
        [NotMapped]
        public string SelectedPortalPrivileges { get; set; }
    }
}
