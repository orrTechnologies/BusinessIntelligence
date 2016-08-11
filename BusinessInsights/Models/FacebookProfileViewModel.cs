﻿using System;
using System.ComponentModel.DataAnnotations;
using BusinessInsights.Attributes;

namespace BusinessInsights.Models
{
    public class FacebookProfileViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [FacebookMapping("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [FacebookMapping("last_name")]
        public string LastName { get; set; }

        [FacebookMapping("name")]
        public string Fullname { get; set; }

        public string ImageURL { get; set; }

        [FacebookMapping("link")]
        public string LinkURL { get; set; }

        [FacebookMapping("locale")]
        public string Locale { get; set; }

        [FacebookMapping("email")]
        public string email { get; set; }

        [FacebookMapping("birthday")]
        public DateTime birthdate { get; set; }

        [FacebookMapping("name", parent="location")]
        public string Location { get; set; }

        [FacebookMapping("gender")]
        public string gender { get; set; }

        [FacebookMapping("age_range")]
        public Facebook.JsonObject age_range { get; set; }

        [FacebookMapping("bio")]
        public string Bio { get; set; }
        
    }
}