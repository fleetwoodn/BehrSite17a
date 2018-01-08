using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BehrSite17.Models
{
    public class BlogPicts
    {
        public int ID { get; set; }
        public int PostFK { get; set; }
        public string PicTitle { get; set; }
        public string EditDate { get; set; }
        public string PictPict { get; set; }
    }
}