using BlogProject.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DTO.DTOs
{
    public class AppUserLoginDto: IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
