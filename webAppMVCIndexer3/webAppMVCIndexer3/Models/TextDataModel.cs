using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace webAppMVCIndexer3.Models
{

    public class TextDataModel
    {
        [Required]
        [Display(Name = "User name")]
        [StringLength(65535, ErrorMessage = "The text is too long.")]
        public string text { get; set; }
    }
}
