using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Models.Purchases.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Models.Users.Entities;
using Newtonsoft.Json;

namespace Fitshirt.Infrastructure.Context;

public class FitshirtDbContextData
{
    public static async Task LoadDataAsync(FitshirtDbContext context)
    {
        try
        {
            if (!context.Roles.Any())
            {
                var rolesData = File.ReadAllText("../Fitshirt.Infrastructure/Data/roles.json");

                var roles = JsonConvert.DeserializeObject<List<Role>>(rolesData);

                context.Roles.AddRange(roles!);
                await context.SaveChangesAsync();
            }

            if (!context.Categories.Any())
            {
                var categoriesData = File.ReadAllText("../Fitshirt.Infrastructure/Data/categories.json");

                var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesData);

                context.Categories.AddRange(categories!);
                await context.SaveChangesAsync();
            }
            
            if (!context.Colors.Any())
            {
                var colorsData = File.ReadAllText("../Fitshirt.Infrastructure/Data/colors.json");

                var colors = JsonConvert.DeserializeObject<List<Color>>(colorsData);

                context.Colors.AddRange(colors!);
                await context.SaveChangesAsync();
            }
            
            if (!context.Services.Any())
            {
                var servicesData = File.ReadAllText("../Fitshirt.Infrastructure/Data/services.json");

                var services = JsonConvert.DeserializeObject<List<Service>>(servicesData);

                context.Services.AddRange(services!);
                await context.SaveChangesAsync();
            }
            
            if (!context.Sizes.Any())
            {
                var sizesData = await File.ReadAllTextAsync("../Fitshirt.Infrastructure/Data/sizes.json");

                var sizes = JsonConvert.DeserializeObject<List<Size>>(sizesData);

                context.Sizes.AddRange(sizes!);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var user = new User
                {
                    Name = "Diego",
                    Lastname = "Defilippi",
                    Password = "Password123",
                    Email = "ddefsan@test.com",
                    Cellphone = "999999999",
                    BirthDate = new DateOnly(2002, 10, 2),
                    RoleId = 1,
                    ServiceId = 1,
                    Designs = new List<Design>(),
                    Items = new List<Item>(),
                    Posts = new List<Post>()
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception error)
        {
            throw error;
        }
    }
}