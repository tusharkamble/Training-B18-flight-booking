using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServices.Interfaces
{
    public interface IAuthRepository
    {
        User GetUserDetailByCredentials(string emailId = "", string password = "");
    }
}
