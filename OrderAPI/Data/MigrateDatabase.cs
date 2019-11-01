using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Data
{
    public static class MigrateDatabase
    {
        public static void EnsureCreated(OrdersContext context)
        {
            System.Console.WriteLine("Creating Database..!!");
            context.Database.Migrate();

            System.Console.WriteLine("Database and Tables creation Complete...!!! ");
        }
    }
}
