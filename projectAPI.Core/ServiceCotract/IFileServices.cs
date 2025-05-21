using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.ServiceCotract
{
    public interface IFileServices
    {
        string Upload( IFormFile formFile);
    }
}
