using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }


        public string UserName { get; set; }

    }
}
