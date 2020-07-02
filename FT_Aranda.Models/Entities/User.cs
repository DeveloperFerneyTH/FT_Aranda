using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FT_Aranda.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Role Role { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
