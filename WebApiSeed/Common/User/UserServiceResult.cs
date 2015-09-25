using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSeed.Common.User
{
    public class UserServiceResult : IServiceResult
    {
        public UserServiceResultEnum Enum { get; set; }
    }
}