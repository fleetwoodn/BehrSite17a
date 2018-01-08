using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BehrSite17.Models
{
    public class Backgrounds
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Site Location")]
        public string SiteLocation { get; set; }
        [DisplayName("Background Image")]
        public string BackImage { get; set; }
    }
}