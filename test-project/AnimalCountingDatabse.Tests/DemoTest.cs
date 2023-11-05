using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using test_project;
using test_project.Controllers;

namespace AnimalCountingDatabse.Tests;

public class DemoTest
{
    [Fact]
    public void Test1()
    {
        Assert.True(1 == 1);
        //"1 == 1" koşulunun doğru olduğunu doğrulayan bir test ifadesini temsil eder. Bu tür ifadeler, yazılım geliştirme sürecinde hata ayıklama ve test işlemlerinde kullanılır. Eğer koşul yanlış olsaydı, bir hata veya başarısız test sonucu üretebilirdi.

        //burayı yadıktan sonra sol tarfatkı menulerden unıt test kısmına gıdeerk projeyı calsıtırın.
    }

    [Fact]
    public async Task CustomerIntegrationTest()
    {
        //Create dbcontext
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        var optionsbuilder = new DbContextOptionsBuilder<Data.CustoemrContext>();
        optionsbuilder.UseSqlServer(configuration["ConnectionStrings:mssqlTest"]);
        
        var context = new Data.CustoemrContext(optionsbuilder.Options);


        //just to make sure: Delete all existing customers in the DB
        // context.Customers.RemoveRange(await context.Customers.ToArrayAsync()); //tum musterılerı sıl
        // await context.SaveChangesAsync();   //Artık buraya gerek yoktur cunku altkaı kod tamamaen databaseyı sılıp olusturcagı ıcın ıcerısınde zaten bırsey bulunıycaktır
        await context.Database.EnsureDeletedAsync(); //ılgılı verı tabanındakı database var ıse sılıcektır 
        await context.Database.EnsureCreatedAsync(); //ilgili verı tabanında verı tabanını olusturcaktır


        //Cretae Controller
        var controller = new CustomersController(context);

        //Add customer
        await controller.Add(new Data.Customer()
        {
            CustomerName = "FooBar"
        });

        //Check:Does getAll return the added custoemr?
        var result = (await controller.GetAll()).ToArray();

        Assert.Single(result); //Tek bır musterınının olması gerektıgını soyler bıoze
        Assert.Equal("FooBar", result[0].CustomerName); //eşitmi 
    }
}