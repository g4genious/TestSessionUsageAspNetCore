using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace testSessionUsage.Models
{
    public partial class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please fill NAME field.")]
        [MaxLength(10)]
        [Display(Name = "Student Name")]
        public string Name { get; set; }


        [RegularExpression(@"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$",ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

       [DataType(DataType.Date)]
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Cnic { get; set; }
        public string Gender { get; set; }
        public string Cv { get; set; }
        public string ProflePicture { get; set; }
    }
}
