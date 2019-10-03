using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IHttpClient _client;
        private readonly string _baseUri;
        public CatalogService(IConfiguration config, IHttpClient client)
        {
             _baseUri = $"{config["CatalogUrl"]}/api/Catalog/"; // need take out the config setting when we dockerize
           // _baseUri = "http://localhost:54501/api/Catalog/";
            _client = client;
        }
        public async Task<IEnumerable<SelectListItem>> GetEventCategoriesAsync()
        {

            var categoryUri = ApiPaths.Catalog.GetAllEventCategories(_baseUri);
            var dataString = await _client.GetStringAsync(categoryUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };
            var categories = JArray.Parse(dataString);
            foreach (var category in categories)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = category.Value<string>("id"),
                        Text = category.Value<string>("name")
                    }
                    );
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventLocationsAsync()
        {
            var locationUri = ApiPaths.Catalog.GetAllEventLocations(_baseUri);
            var dataString = await _client.GetStringAsync(locationUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };
            var locations = JArray.Parse(dataString);
            foreach (var location in locations)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = location.Value<string>("id"),
                        Text = location.Value<string>("city")
                    }
                    );
            }
            return items;
        }

        public async Task<Catalog> GetEventsCatalogAsync(int page, int size, int? type, int? category, int? location)
        {
            var catalogEventsUri = ApiPaths.Catalog.GetAllEvents(_baseUri, page, size, type, category, location);
            var dataString = await _client.GetStringAsync(catalogEventsUri);
            return JsonConvert.DeserializeObject<Catalog>(dataString);
        }

        public async Task<IEnumerable<SelectListItem>> GetEventTypesAsync()
        {
            var typeUri = ApiPaths.Catalog.GetAllEventTypes(_baseUri);
            var dataString = await _client.GetStringAsync(typeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected=true
                }
            };
            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")
                    }
                    );
            }
            return items;
        }
    }
}
