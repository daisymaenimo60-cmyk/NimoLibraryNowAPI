using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NimoLibraryNowAPI.Models;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NimoLibraryNowAPI.Controlers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "The Last Session",
                Author = "Julia Bartz",
                Genre = "Psychological Thriller",
                Available = true,
                PublishedYear = 2025
            },
            new Book
            {
                Id = 2,
                Title = "The Mind Electric",
                Author = "Pria Anand",
                Genre = "Science / Neurology",
                Available = true,
                PublishedYear = 2025
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "succes",
                data = books,
                message = "Books Retrieved."

            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Books not found."
                });

            return Ok(new
            {
                status = "succes",
                data = book,
                message = "Books Retrieves."
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book created."
                });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {

            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book update."
            });

        }
        [HttpDelete ("{id}")]
        public IActionResult Delete(int id )
        {

            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book deleted."
            });

        }

    }
}

    
