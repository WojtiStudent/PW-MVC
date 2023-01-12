using Spychalski.Perfumes.Models;

namespace Spychalski.Perfumes
{
    public static class DataMock
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<IConfiguration>()))
            {
                if (context.Brands.Any())
                {
                    return;
                }

                context.Brands.AddRange(
                    new Brand
                    {
                        Name = "Chanel",
                        Country = "France",
                        Headquarters = "Paris"
                    },
                    new Brand
                    {
                        Name = "Dior",
                        Country = "France",
                        Headquarters = "London"
                    },
                    new Brand
                    {
                        Name = "Gucci",
                        Country = "Italy",
                        Headquarters = "Florence"
                    },
                    new Brand
                    {
                        Name = "Versace",
                        Country = "Italy",
                        Headquarters = "Milan"
                    }
                );
                context.SaveChanges();

                var brands = context.Brands.ToList();
                int chanelId = brands.First(x => x.Name == "Chanel").BrandId;
                int diorId = brands.First(x => x.Name == "Dior").BrandId;
                int gucciId = brands.First(x => x.Name == "Gucci").BrandId;
                int versaceId = brands.First(x => x.Name == "Versace").BrandId;


                context.Perfumes.AddRange(
                    new Perfume
                    {
                        Name = "No. 5",
                        BrandId = chanelId,
                        ScentDescription = "Aldehydes, neroli, ylang-ylang, bergamot, lemon",
                        Status = StatusType.InStock,
                        Amount = 10,
                    },

                    new Perfume
                    {
                        Name = "No. 19",
                        BrandId = chanelId,
                        ScentDescription = "Rose, jasmine, ylang-ylang, iris, vetiver, sandalwood, oakmoss, musk",
                        Status = StatusType.InStock,
                        Amount = 3,
                    },

                    new Perfume
                    {
                        Name = "Homme",
                        BrandId = diorId,
                        ScentDescription = "Cedar, patchouli, vetiver",
                        Status = StatusType.Ordered,
                        Amount = 0,
                    },

                    new Perfume
                    {
                        Name = "Guilty",
                        BrandId = gucciId,
                        ScentDescription = "Lemon, lavender, orange blossom, peach, lilac, geranium, amber, patchouli, vanilla, musk",
                        Status = StatusType.InStock,
                        Amount = 5,
                    },

                    new Perfume
                    {
                        Name = "Eros",
                        BrandId = versaceId,
                        ScentDescription = "Bergamot, lemon, pomegranate, mint, geranium, tonka bean, amber, musk",
                        Status = StatusType.InStock,
                        Amount = 2,
                    },

                    new Perfume
                    {
                        Name = "Bright Crystal",
                        BrandId = versaceId,
                        ScentDescription = "Yuzu, pomegranate, peony, magnolia, lotus, plant amber, musk",
                        Status = StatusType.OutOfStock,
                        Amount = 0,
                    },

                    new Perfume
                    {
                        Name = "Yellow Diamond",
                        BrandId = versaceId,
                        ScentDescription = "Lemon, bergamot, neroli, orange blossom, freesia, mimosa, amber, musk",
                        Status = StatusType.InStock,
                        Amount = 5,
                    },

                    new Perfume
                    {
                        Name = "Pour Homme",
                        BrandId = versaceId,
                        ScentDescription = "Bergamot, lemon, neroli, orange blossom, cedar, tonka bean, musk",
                        Status = StatusType.InStock,
                        Amount = 1,
                    },

                    new Perfume
                    {
                        Name = "Sauvage Elixir",
                        BrandId = diorId,
                        ScentDescription = "Grapefruit, cinnamon, nutmeg, cardamom, lavender, coumarin, vanilla," +
                        " amber, licorice, Haitian vetiver, patchouli",
                        Status = StatusType.OutOfStock,
                        Amount = 0,
                    },


                    new Perfume
                    {
                        Name = "Sauvage",
                        BrandId = diorId,
                        ScentDescription = "Bergamot, grapefruit, pepper, lavender, geranium, coumarin, patchouli, vetiver, ambergris",
                        Status = StatusType.InStock,
                        Amount = 2,
                    },

                    new Perfume
                    {
                        Name = "Bamboo",
                        BrandId = gucciId,
                        ScentDescription = "Bergamot, ylang-ylang, orange blossom, lily, amber, sandalwood, Tahiti vanilla",
                        Status = StatusType.InStock,
                        Amount = 3,
                    }
                   ); ;

                context.SaveChanges();
            }
        }
    }
}
