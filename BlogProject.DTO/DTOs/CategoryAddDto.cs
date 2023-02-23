using BlogProject.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.DTOs
{
    public class CategoryAddDto: IDto
    {
        public string Name { get; set; }
    }
}
