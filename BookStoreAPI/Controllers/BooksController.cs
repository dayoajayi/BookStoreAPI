using Bookstore.Data.Interfaces;
using Bookstore.Data.Models;
using Bookstore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
