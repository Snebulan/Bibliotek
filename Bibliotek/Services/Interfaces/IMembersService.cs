using Bibliotek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Services.Interfaces
{
    public interface IMembersService
    {

        //object Details(int? id);
        Member GetDetails(int? id);
    }
}
