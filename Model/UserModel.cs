using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class UserModel
    {
        [Key]
        public Guid UserID { get; set; }


        public string UserName { get; set; }

        public string Sex { get; set; }


        public int Age { get; set; }


        public string Adress { get; set; }

    }
}
