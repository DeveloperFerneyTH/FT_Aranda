using System;

namespace FT_Aranda.Models.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Attemps { get; set; }
        public bool? Blocked { get; set; }
        public DateTime? LastEntryDate { get; set; }
    }
}
