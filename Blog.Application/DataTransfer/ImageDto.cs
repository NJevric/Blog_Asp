using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.DataTransfer
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
    }
}
