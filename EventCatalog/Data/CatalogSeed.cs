using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public static class CatalogSeed
    {
        public static void Seed(CatalogContext context)
        {
            context.Database.Migrate();
            if (!context.CatalogCategories.Any())
            {
                context.CatalogCategories.AddRange(GetPreConfiguredCatalogCategories());
                context.SaveChanges();
            }
            if (!context.CatalogTypes.Any())
            {
                context.CatalogTypes.AddRange(GetPreConfiguredCatalogTypes());
                context.SaveChanges();
            }
            if (!context.CatalogEvents.Any())
            {
                context.CatalogEvents.AddRange(GetPreConfiguredCatalogEvents());
                context.SaveChanges();
            }
            if (!context.)
            {

            }
        }

        private static IEnumerable<CatalogEvent> GetPreConfiguredCatalogEvents()
        {
            return new List<CatalogEvent>
            {
                new CatalogEvent{ Title="TEDxSeattle 2019",                }

            };
        }

        private static IEnumerable<CatalogType> GetPreConfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                new CatalogType { Type = "Appearance or Singing" },
                new CatalogType { Type = "Attraction" },
                new CatalogType { Type = "Camp, trip, or Retreat" },
                new CatalogType { Type = "Class, Training, or Workshop" },
                new CatalogType { Type = "Concert or Performance" },
                new CatalogType { Type = "Conference" },
                new CatalogType { Type = "Convention" },
                new CatalogType { Type = "Dinner or Gala" },
                new CatalogType { Type = "Festival or Fair" },
                new CatalogType { Type = "Game or Competition" },
                new CatalogType { Type = "Meeting or Networking Event" },
                new CatalogType { Type = "Other" },
                new CatalogType { Type = "Party or Social Gathering" },
                new CatalogType { Type = "Race or Endurance Event" },
                new CatalogType { Type = "Rally" },
                new CatalogType { Type = "Screening" },
                new CatalogType { Type = "Seminar or Talk" },
                new CatalogType { Type = "Tour" },
                new CatalogType { Type = "Tournament" }
            };
        }

        private static IEnumerable<CatalogCategory> GetPreConfiguredCatalogCategories()
        {
            return new List<CatalogCategory>
            {
                new CatalogCategory{ Category="Business or Profession"},
                new CatalogCategory{ Category="Charity & causes"},
                new CatalogCategory{ Category="Community & Culture"},
                new CatalogCategory{ Category="Family & Education"},
                new CatalogCategory{ Category="Fashion & Beauty"},
                new CatalogCategory{ Category="Film, Media & Entertainment"},
                new CatalogCategory{ Category="Food & Drink"},
                new CatalogCategory{ Category="Government & Politics"},
                new CatalogCategory{ Category="Health & Wellness"},
                new CatalogCategory{ Category="Hobbies & Special Interest"},
                new CatalogCategory{ Category="Home & Lifestyle"},
                new CatalogCategory{ Category="Music"},
                new CatalogCategory{ Category="Other"},
                new CatalogCategory{ Category="Performing & Visual Arts"},
                new CatalogCategory{ Category="Religion & Spirituality"},
                new CatalogCategory{ Category="School Activities"},
                new CatalogCategory{ Category="Science & Technology"},
                new CatalogCategory{ Category="Seasonal & Holiday"},
                new CatalogCategory{ Category="Sports & Fitness"},
                new CatalogCategory{ Category="Travel & Outdooor"}
            };
        }
    }
}
