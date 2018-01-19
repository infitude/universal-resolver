using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace UniResolver.ViewModels
{
    public class ResolverViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage= "DID is too short")]
        public string Did { get; set; }
    }
}
