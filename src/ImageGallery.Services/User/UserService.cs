using System.Data.Entity;
using System.Linq;
using ImageGallery.Data.Common;

namespace ImageGallery.Services.User
{
    public class UserService : IUserService
    {        
        private IRepository<Data.Models.User, string> users;

        public UserService(DbContext context, IRepository<Data.Models.User> users)
        {
            this.users = users;            
        }

        public void Add(Data.Models.User user)
        {
//            user.UserName = user.Email;
//            user.CreatedOn = DateTime.Now;            
//            this.users.Update(user);            
        }

        public void Delete(string id)
        {
//            this.users.Delete(id);
//
//            var messagesDb = this.messages.All().Where(x => x.FromId == id || x.ToId == id).ToList();
//            foreach (var message in messagesDb)
//            {
//                this.messages.Delete(message);
//            }
        }

        public IQueryable<Data.Models.User> GetAll()
        {
            return this.users.All();
        }

        public Data.Models.User GetById(string id)
        {
            return this.users.GetById(id);
        }

        public void Update(Data.Models.User user)
        {
            this.users.Update(user);
        }
    }
}