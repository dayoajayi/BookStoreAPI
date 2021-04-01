using Bookstore.Data.Interfaces;
using Bookstore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookRepository books;

        public BooksController(IBookRepository _books)
        {
            books = _books;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return books.GetAllBooks();
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = books.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            if (books.AddNewBook(book))
            {
                return book;
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Book>> DeleteBook(int id)
        {
            if (books.Remove(id))
            {
                return books.GetAllBooks();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Book>> UpdateBook(int id, Book book)
        {
            var ubook = books.UpdateBook(id, book);

            if (ubook != null)
            {
                return ubook;
            }
            return NotFound();
        }

        [HttpGet]
        [Route("author/{name}")]
        [Route("authorname/{name}")]
        public ActionResult<IEnumerable<Book>> GetBooksByAuthor(string author)
        {
            var result = books.GetBooksByAuthor(author);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpGet]
        [Route("{id}/author")]
        [Route("authorid/{id}")]
        public ActionResult<string> GetAuthorById(int id)
        {
            var name = books.GetAuthorById(id);
            if (name == null)
            {
                return NotFound();
            }
            return name;
        }

        [HttpGet]
        [Route("author/{author}/year/{year}")]
        public ActionResult<Book> GetBookByAuthorAndYear(string author, int year)
        {
            var book = books.GetBookByAuthorAndYear(author,year);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPut]
        [Route("updatecost/{id}")]
        public ActionResult<Book> AddCost(int id, [FromForm] Cost cost)
        {
            Book result = books.AddCost(id, cost);
            if (result != null)
            {
                return result;
            }
            return NotFound();
        }

    }
}
