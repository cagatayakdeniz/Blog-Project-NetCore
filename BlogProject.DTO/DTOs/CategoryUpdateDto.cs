using BlogProject.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.DTOs
{
    public class CategoryUpdateDto: IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
