namespace WebApiSeed.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.SqlServer;
    using System.Linq;
    using WebApiSeed.Common.Helpers.Interfaces;
    using WebApiSeed.Data.Configuration.EF.Interfaces;
    using WebApiSeed.Data.Domain;
    using WebApiSeed.Data.Repositories.Interfaces;

    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _context;

        private readonly ILoggingHelper _loggingHelper;

        public UserRepository(IDbContext context, ILoggingHelper loggingHelper)
        {
            _context = context;
            _loggingHelper = loggingHelper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public User FindUserById(int id)
        {
            return _context.Entity<User>().FirstOrDefault(user => user.Id == id);
        }

        public User FindUserByUserName(string userName)
        {
            return
                _context.Entity<User>()
                    .FirstOrDefault(user => user.UserName != null && user.UserName.ToUpper() == userName.ToUpper());
        }

        public User FindUserByToken(string token)
        {
            return _context.Entity<User>().FirstOrDefault(user => user.AccessToken == token);
        }

        public IList<User> GetAllUsers()
        {
            return _context.Entity<User>().ToList();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Entity<User>().SingleOrDefault(i => i.Id == id);
            if (user == null)
                return;

            _context.Entity<User>().Remove(user);
            _context.SaveChanges();
        }

        public void SaveOrUpdateUser(User user)
        {
            if (user.Id != 0)
            {
                var existingUser = _context.Entity<User>().FirstOrDefault(u => u.Id == user.Id);

                if (existingUser == null)
                    return;

                //Update scalar values
                if (user.AccessToken != null)
                    existingUser.AccessToken = user.AccessToken;

                if (user.UserName != null)
                    existingUser.UserName = user.UserName;

                if (user.Gender != null)
                    existingUser.Gender = user.Gender;

                _context.SaveChanges();
            }
            else
            {
                _context.Entity<User>().Add(user);
                _context.SaveChanges();
            }
        }
    }
}