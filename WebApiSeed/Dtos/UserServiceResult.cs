using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSeed.Dtos
{
    public class UserServiceResult : IServiceResult
    {
        public UserServiceResultEnum Enum { get; set; }
    }
}