using Mango.Web.Models;
using Mongo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto responseModel { get; set; }

        // Create a generic model to sent the request
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
