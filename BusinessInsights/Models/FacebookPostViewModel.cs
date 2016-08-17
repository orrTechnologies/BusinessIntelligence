using System;
using System.ComponentModel.DataAnnotations;
using BusinessInsights.Attributes;

namespace BusinessInsights.Models
{
    public class FacebookPostViewModel
    {
        [Required]
        [FacebookMapping("id")]
        public string Id { get; set; }

        [FacebookMapping("created_time")]
        public DateTime CreatedTime { get; set; }

        [FacebookMapping("id", parent = "from")]
        public string FromId { get; set; }

        [FacebookMapping("name", parent = "from")]
        public string FromName { get; set; }

        [FacebookMapping("url", parent = "from")]
        public string FromPictureUrl { get; set; }
        
        [FacebookMapping("name", parent = "to")]
        public string ToName { get; set; }

        [FacebookMapping("story")]
        public string Story { get; set; }

        [FacebookMapping("message")]
        public string Message { get; set; }

        [FacebookMapping("picture")]
        public string PictureUrl { get; set; }

        [FacebookMapping("link")]
        public string Link { get; set; }

        [FacebookMapping("description")]
        public string Description { get; set; }

        [FacebookMapping("caption")]
        public string Caption { get; set; }

        [FacebookMapping("type")]
        public string Type { get; set; }

        [FacebookMapping("likes")]
        public dynamic Likes { get; set; }

        [FacebookMapping("comments")]
        public dynamic Comments { get; set; }

    }
}
