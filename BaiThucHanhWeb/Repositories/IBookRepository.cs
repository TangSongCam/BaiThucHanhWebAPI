using BaiThucHanhWeb.Model.DTO;
using BaiThucHanhWeb.Model.Domain;


namespace BaiThucHanhWeb.Repositories
{
    public interface IBookRepository
    {
        List<BookDTO> GetAllBooks();
        BookDTO GetBookById(int id);
        AdBookRequestDTO AddBook(AdBookRequestDTO addBookRequestDTO);
        AdBookRequestDTO? UpdateBookById(int id, AdBookRequestDTO bookDTO);
        Books? DeleteBookById(int id);
    }
}