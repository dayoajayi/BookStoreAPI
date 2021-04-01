using Bookstore.Data.Models;
using System.Collections.Generic;

namespace Bookstore.Data.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBook(int id);
        List<Book> GetBooksByAuthor(string author);
        Book GetBookByAuthorAndYear(string author, int year);
        string GetAuthorById(int id);


        bool AddNewBook(Book book);
        bool Remove(int id);
        List<Book> UpdateBook(int id, Book book);

        Book AddCost(int id, Cost cost);
    }
}
