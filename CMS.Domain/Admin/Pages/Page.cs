using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Admin.Pages
{
    public class Page : BaseDomain
    {
        [Required, MinLength(3, ErrorMessage = "حداقل 3 کاراکتر")]
        public string Title { get; set; }

        public string? Slug { get; set; }

        [Required, MinLength(10, ErrorMessage = "حداقل 10 کاراکتر")]
        public string Body { get; set; }

        public int Order { get; set; } = 100;
    }
}
