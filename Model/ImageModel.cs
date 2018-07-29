using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
   public class ImageModel
    {
        [Key]
        public Guid ImageID { get; set; }


        public DateTime CreateTime { get; set; }


        public string ImgURL { get; set; }




    }
}
