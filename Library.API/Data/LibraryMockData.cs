using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Data
{
    public class LibraryMockData
    {
        // 获取 LibraryMockData 实例
        public static LibraryMockData Current { get; } = new LibraryMockData();
        public List<AuthorDto> Authors { get; set; }
        public List<BookDto> Books { get; set; }

        public LibraryMockData()
        {
            var authorId1 = Guid.NewGuid();
            var authorId2 = Guid.NewGuid();

            Authors = new List<AuthorDto>
            {
                new AuthorDto
                {
                    Id = authorId1,
                    Name = "Author 1",
                    Age = 46,
                    Email = "Author.com",
                },
                new AuthorDto
                {
                    Id = authorId2,
                    Name = "Author 2",
                    Age = 38,
                    Email = "Author2.com",
                }
            };

            Books = new List<BookDto>
            {
                new BookDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 1",
                    Description = "Description of Book 1",
                    Pages = 281,
                    AuthorId = authorId1,
                },                new BookDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 2",
                    Description = "Description of Book 2",
                    Pages = 370,
                    AuthorId = authorId2,
                }
            };
        }
    }
}
