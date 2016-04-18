using System.Linq;

namespace ImageGallery.Services.User
{
    public interface IUserService
    {
        void Add(Data.Models.User user);

        void Delete(string id);

        IQueryable<Data.Models.User> GetAll();
        
        Data.Models.User GetById(string id);
        
        void Update(Data.Models.User user);
    }
}