using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi;
using Xunit;
using System.Net.Http.Json;
using WebApi.Controllers;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Identity;
using Storage.Models.Identity;
using System.Linq;
using Core.Interfaces.Authentication;
using Core.Services.Authentication;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace ApiIntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient testClient;
        protected readonly ApiDbContext context;
        protected User user;

        protected IntegrationTests()
        {            
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => builder.ConfigureServices(services => 
                {
                    services.RemoveAll(typeof(DbContextOptions<ApiDbContext>));
                    services.AddDbContext<ApiDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                }));

            var scope = appFactory.Services.CreateScope();
            context = scope.ServiceProvider.GetService<ApiDbContext>();

            testClient = appFactory.CreateClient();
            
        }

        protected async Task AuthenticateClientAsync()
        {
            testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }
        
        protected async Task<string> GetJwtAsync()
        {
            var email = "test@integration.com";
            HttpResponseMessage registerResponse = await testClient.PostAsJsonAsync("api/Authenticate/Register", new RegisterRequest
            {
                Username = "TestUsername",
                Email = email,
                Password = "testPass1!",
                ConfirmPassword = "testPass1!"
            });


            user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            user.EmailConfirmed = true;
            context.SaveChanges();


            HttpResponseMessage loginResponse = await testClient.PostAsJsonAsync("api/Authenticate/Login", new LoginRequest
            {                
                Email = "test@integration.com",
                Password = "testPass1!",                
            });   
            
            JObject json = JObject.Parse(loginResponse.Content.ReadAsStringAsync().Result);                        
            return json["content"].ToString();
        }

        protected void ClearInMemoryDatabase()
        {
            context.Database.EnsureDeleted();            
        }
    }
}
