namespace WebApiSeed.Data.Domain
{
    using System;
    using System.Collections.Generic;

    public class User : BaseEntity
    {
        public User()
        {
        }

        public string AccessToken { get; set; }
        public string UserName { get; set; }

        public Gender? Gender { get; set; }
    }
}