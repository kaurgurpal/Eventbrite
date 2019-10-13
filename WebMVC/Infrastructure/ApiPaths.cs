using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class ApiPaths
    {
        public static class Catalog
        {
            //API Path for GetAllEventTypes
            public static string GetAllEventTypes(string baseUri)
            {
                return $"{baseUri}EventTypes";
            }

            //ApiPath for GetAllEventCategories

            public static string GetAllEventCategories(string baseUri)
            {
                return $"{baseUri}EventCategories";
            }

            //ApiPath for GetAllEvents
            public static string GetAllEvents(string baseUri, int page, int take, int? type, int? category, int? location)
            {
                string filterQs = string.Empty;
                if (type.HasValue || category.HasValue || location.HasValue)
                {
                    var typeQs = (type.HasValue) ? type.Value.ToString() : " ";
                    var categoryQs = (category.HasValue) ? category.Value.ToString() : " ";
                    var locationQs = (location.HasValue) ? location.Value.ToString() : " ";
                    filterQs = $"/type/{typeQs}/category/{categoryQs}/location/{locationQs}";
                }
                return $"{baseUri}Events{filterQs}?pageIndex={page}&pageSize={take}";               
            }

            //ApiPath for GetAllEventLocations
            public static string GetAllEventLocations(string baseUri)
            {
                return $"{baseUri}EventLocations";
            }

                       
            //ApiPath for CreateEvent(POST request)

            public static string CreateEvent(string baseUri)
            {
                return $"{baseUri}CreateEvent";
            }

            //ApiPath for UpdateEvent(PUT request) // Need to Ask gurpal to update the name of the method
            public static string UpdateEvent(string baseUri)
            {
                return $"{baseUri}UpdateEvent";
            }

            //APIPath for DeleteEvent(DELETE request) //Need to Ask Gurpal to add ROUTE and update the name of the method
            public static string DeleteEvent(string baseUri)
            {
                return $"{baseUri}DeleteEvent";
            }

        }

        public class Pic
        {
            public static string GetDefaultImage(string baseuri)
            {
                return $"{baseuri}100";
            }
        }
    }

}
