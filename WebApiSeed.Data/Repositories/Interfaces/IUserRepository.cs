namespace WebApiSeed.Data.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Domain;

    public interface IUserRepository : IDisposable
    {
        User FindUserById(int id);

        User FindUserByUserName(string userName);

        User FindUserByToken(string token);

        IList<User> GetAllUsers();

        void SaveOrUpdateUser(User user);

        void DeleteUser(int id);
    }
}