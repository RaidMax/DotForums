using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotForums.Models
{
    public class AttributeModel : ForumObjectModel
    {
        [ForeignKey("Title")]
        public ulong TitleID { get; set; }
        [Required]
        public AttributeTitleModel Title { get; set; }
        private string _value;
        [Required]
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;

                if (Title.Link != null)
                    Link = Title.Link.Replace("{%LINK%}", value);
                else
                    Link = null;
            }
        }

        public string Link { get; set; }
    }

    public class AttributeTitleModel : ForumObjectModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
