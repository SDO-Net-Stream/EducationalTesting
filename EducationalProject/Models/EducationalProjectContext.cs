using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EducationalProject.Models
{
    public class EducationalProjectContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookSection> BookSections { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //one-to-many 
            modelBuilder.Entity<Book>().HasRequired<BookSection>(s => s.BookSection)
            .WithMany(s => s.BookList).HasForeignKey(s => s.BookSectionId);

        }
    }
}