using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoodStapleEx.Data;

namespace TheFoodStapleEx.Models
{
    public class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            Console.WriteLine("In DB");
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }
            if (!context.ITypes.Any())
            {
                context.ITypes.AddRange(ITypes.Select(c => c.Value));
            }

            if (!context.Items.Any())
            {
                context.AddRange
                (
                    new Item
                    {
                        ItemName = "Mango",
                        ItemPrice = 7.95F,
                        ShortDescription = "The most widely favorite fruit of summer",
                        LongDescription = "Mango is the king of fruit and most eatable fruit of summer.",
                        Category = Categories["Organic"],
                        ImageUrl = "mango.png",
                        InStock = true,
                        ImageThumbnailUrl = "http://imgh.us/beerS_1.jpeg",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Apple",
                        ItemPrice = 7.95F,
                        ShortDescription = "Apple is very useful for health reasons",
                        LongDescription = "Apple, Once a day keep Doctors away.",
                        Category = Categories["Organic"],
                        ImageUrl = "apple.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/apple.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Guvava",
                        ItemPrice = 7.95F,
                        ShortDescription = "Guvava",
                        LongDescription = "Guvava",
                        Category = Categories["Organic"],
                        ImageUrl = "guvava.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/guvava.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Strawberry",
                        ItemPrice = 7.95F,
                        ShortDescription = "Strawberry",
                        LongDescription = "Strawberry",
                        Category = Categories["Organic"],
                        ImageUrl = "strawberry.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/strawberry.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Blackberry",
                        ItemPrice = 7.95F,
                        ShortDescription = "Blackberry",
                        LongDescription = "Blackberry",
                        Category = Categories["Organic"],
                        ImageUrl = "blackberry.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/blackberry.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Blueberry",
                        ItemPrice = 7.95F,
                        ShortDescription = "Blueberry",
                        LongDescription = "Blueberry",
                        Category = Categories["Organic"],
                        ImageUrl = "blueberry.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/Fruits/blueberry.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Kiwi",
                        ItemPrice = 7.95F,
                        ShortDescription = "Kiwi",
                        LongDescription = "Kiwi",
                        Category = Categories["Organic"],
                        ImageUrl = "kiwi.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/Fruits/kisi.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Plum",
                        ItemPrice = 7.95F,
                        ShortDescription = "Plum",
                        LongDescription = "Plum",
                        Category = Categories["Organic"],
                        ImageUrl = "plum.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/Fruits/plum.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Pineapple",
                        ItemPrice = 7.95F,
                        ShortDescription = "Pineapple",
                        LongDescription = "Pineapple",
                        Category = Categories["Organic"],
                        ImageUrl = "pineapple.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/Fruits/pineapple.png",
                        IType = ITypes["Fruits"]
                    },
                    new Item
                    {
                        ItemName = "Pomegranate",
                        ItemPrice = 7.95F,
                        ShortDescription = "Pomegranate",
                        LongDescription = "Pomegranate",
                        Category = Categories["Organic"],
                        ImageUrl = "pomegranate.png",
                        InStock = true,
                        ImageThumbnailUrl = "~/images/Fruits/pomegranate.png",
                        IType = ITypes["Fruits"]
                    }
                );
            }
            context.SaveChanges();
        }



        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Organic", Description="All Organic Foods" },
                        new Category { CategoryName = "Inorganic", Description="All Inorganic Foods" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
        private static Dictionary<string, IType> itypes;
        public static Dictionary<string, IType> ITypes
        {
            get
            {
                if (itypes == null)
                {
                    var genresList = new IType[]
                    {
                        new IType{ ITypeName = "Fruit", Description="All Fruits" },
                        new IType { ITypeName = "Vegetables", Description="All Vegetables" }
                    };

                    itypes = new Dictionary<string, IType>();

                    foreach (IType genretype in genresList)
                    {
                        itypes.Add(genretype.ITypeName, genretype);
                    }
                }
                return itypes;
            }
        }
    }
}
  