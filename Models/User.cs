using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
