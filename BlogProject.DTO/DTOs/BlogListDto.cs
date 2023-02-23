using BlogProject.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.DTOs
{
    public class BlogListDto: IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
