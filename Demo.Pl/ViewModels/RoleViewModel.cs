using System;
using System.ComponentModel;

namespace Demo.Pl.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
