﻿using MovieTicketVendor.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketVendor.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }
        //Relationships
        public List<Actor_Movie> Actor_Movie { get; set; }
    }
}
