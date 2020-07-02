using System;
using System.Collections.Generic;

namespace FT_Aranda.Models.Entities
{
    public partial class Login
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Attemps { get; set; }
        public bool? Blocked { get; set; }
        public DateTime? LastEntryDate { get; set; }
    }
}
