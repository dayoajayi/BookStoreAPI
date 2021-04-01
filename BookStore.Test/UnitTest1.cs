using Bookstore.Data.Interfaces;
using Bookstore.Data.Models;
using BookStoreAPI.Controllers;
using Moq;
using Xunit;

namespace BookStore.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var mockBookRepo = new Mock<IBookRepository>();
            var bookId = 2;
            var title = "Dayo's Book";
            mockBookRepo.Setup(x => x.GetBook(It.IsAny<int>()))
                .Returns(new Book() { Id = 2, Title = "Dayo's Book" });
            var controller = new BooksController(mockBookRepo.Object);

            //Act
            var response = controller.Get(bookId);

            // Assert 
            Assert.Equal(bookId, response.Value.Id);
            Assert.Equal(title, response.Value.Title);

        }
    }
}
