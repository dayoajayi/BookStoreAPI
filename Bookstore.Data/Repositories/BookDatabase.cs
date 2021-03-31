using Bookstore.Data.Interfaces;
using Bookstore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Data.Repositories
{
    public class BookDatabase : IBookRepository
    {
        private BookStoreContext db;

        public BookDatabase(BookStoreContext _db)
        {
            db = _db;
        }

        public bool AddNewBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return true;
        }

        public List<Book> GetAllBooks()
        {
            return db.Books.ToList();
        }

        public Book GetBook(int id)
        {
            return db.Books.FirstOrDefault(x => x.Id == id);
        }

        public bool Remove(int id)
        {
            var book = GetBook(id);
            if (book == null)
            {
                return false;
            }
            db.Books.Remove(book);
            db.SaveChanges();
            return true;
        }

        public List<Book> UpdateBook(int id, Book book)
        {
            if (Remove(id))
            {
                AddNewBook(book);
                db.SaveChanges();
                return db.Books.ToList();
            }
            return db.Books.ToList();
        }
    }
}
