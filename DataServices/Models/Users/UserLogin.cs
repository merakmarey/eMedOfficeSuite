// using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataEntities.UserEntity
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
