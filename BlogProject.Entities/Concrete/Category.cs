using BlogProject.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Entities.Concrete
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CategoryBlog> CategoryBlogs { get; set; }
    }
}
