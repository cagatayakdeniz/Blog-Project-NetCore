using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.DataAccess.Concrete.EfCore.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.AuthorName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.AuthorEmail).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();

            builder.HasOne(x => x.ParentComment).WithMany(x => x.SubComments).HasForeignKey(x => x.ParentCommentId);
        }
    }
}
