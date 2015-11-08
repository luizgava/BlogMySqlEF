using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog1.DB.Classes;

namespace Blog1.DB.Mapeamento
{
    public class PostTagConfig : EntityTypeConfiguration<PostTag>
    {
        public PostTagConfig()
        {
            ToTable("POSTTAG");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDPOSTTAG")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            Property(x => x.Tag)
                .HasColumnName("TAG")
                .HasMaxLength(20)
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);

            HasRequired(x => x.TagClass)
                .WithMany()
                .HasForeignKey(x => x.Tag);
        }
    }
}
