using System.Text;
using CUVU_Technical_Task.Domain.Entities;
using CUVU_Technical_Task.Infrastructure.Persistence;
using Newtonsoft.Json;

namespace WebUI.IntegrationTests.Common;
public class Utilities
{
    public static StringContent GetRequestContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }

    public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<T>(stringResponse);

        return result;
    }

    public static void InitializeDbForTests(ApplicationDbContext context)
    {
        context.CarParks.Add(new CarPark
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
        });



        context.SaveChanges();
    }
}
