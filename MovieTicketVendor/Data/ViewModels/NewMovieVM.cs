using MovieTicketVendor.Data.Base;
using MovieTicketVendor.Data.Enums;
using MovieTicketVendor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketVendor.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Movie Description is Required")]
        public string Description { get; set; }
        [Display(Name = "Movie Price")]
        [Required(ErrorMessage = "Movie Price is Required")]
        public double Price { get; set; }
        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Poster is Required")]
        public string ImageURL { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is Required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is Required")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = "Category is Required")]
        public MovieCategory MovieCategory { get; set; }
        //Relationships
        [Display(Name = "Select Actor(s)")]
        [Required(ErrorMessage = "Actor(s) is Required")]
        public List<int> ActorIds { get; set; }
        [Display(Name = "Select Cinema")]
        [Required(ErrorMessage = "Cinema is Required")]
        public int CinemaId { get; set; }
        [Display(Name = "Select Producer")]
        [Required(ErrorMessage = "Producer is Required")]
        public int ProducerId { get; set; }        
    }
}
