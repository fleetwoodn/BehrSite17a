using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BehrSite17.Models
{
    public class Specials
    {
        public int ID { get; set; }
        [DisplayName("Section")]
        public string Section { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Site Location")]
        public string SiteLocation { get; set; }
        [DisplayName("Content")]
        public string Content { get; set; }
        [DisplayName("Button Title")]
        public string ButtonTitle { get; set; }
        [DisplayName("Link URL")]
        public string LinkUrl { get; set; }
        [DisplayName("Special Image")]
        public string SpecialImage { get; set; }

    }
}