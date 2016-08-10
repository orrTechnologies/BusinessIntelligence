using System.ComponentModel.DataAnnotations;

namespace BusinessInsights.Models
{
    public class FacebookViewModel
    {
        [Required]
        [Display(Name="Friend's Name")]
        public string Name { get; set; }

        //[FacebookFieldModifier("type(large)")]
        //public string ImageURL { get; set; }
    }
}