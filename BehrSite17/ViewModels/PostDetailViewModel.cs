using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BehrSite17.ViewModels
{
    public class PostDetailViewModel
    {
        //Posts
        public int ID { get; set; }
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }
        [Display(Name = "Post Author")]
        public string PostAuthor { get; set; }
        [Display(Name = "Post Tags")]
        public string PostTags { get; set; }
        [Display(Name = "Post Text")]
        public string PostText { get; set; }
        public string TitlePic { get; set; }
        [Display(Name = "Edited Date")]
        public string EditDate { get; set; }

        //Picts

        public IEnumerable<Models.BlogPicts> BlogPicts { get; set; }

        //public int ID { get; set; }
        //public int PostFK { get; set; }
        //public string PicTitle { get; set; }
        //public string EditDate { get; set; }
        //public string PictPict { get; set; }

    }
}