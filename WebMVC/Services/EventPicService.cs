using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure;

namespace WebMVC.Services
{
    public class EventPicService
    {
        private readonly string _baseUri;
        public EventPicService(IConfiguration config)
        {
            _baseUri = $"{config["CatalogUrl"]}/api/pic/"; // need take out the config setting when we dockerize
                                                               // _baseUri = "http://localhost:54501/api/pic/";
           
        }

        public string GetDefaultImageUrl()
        {
            var ImageUrl = ApiPaths.Pic.GetDefaultImage(_baseUri);

            return ImageUrl;

        }
    }
}
