﻿using System.ComponentModel.DataAnnotations;
using BusinessInsights.Attributes;

namespace BusinessInsights.Models
{
    public class FacebookFriendViewModel
    {
        [Required]
        [FacebookMapping("id")]
        public string TaggingId { get; set; }

        [Required]
        [Display(Name = "Friend's Name")]
        [FacebookMapping("name")]
        public string Name { get; set; }

        [FacebookMapping("url", parent = "picture")]
        public string ImageURL { get; set; }
    }
}