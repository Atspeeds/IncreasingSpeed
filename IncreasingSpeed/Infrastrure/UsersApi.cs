using IncreasingSpeed.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncreasingSpeed.Infrastrure
{
    public class UsersApi
    {

        RestClient client = new RestClient("https://jsonplaceholder.typicode.com");

        public  async Task<List<User>> GetUsers()
        {
            RestRequest restRequest = new RestRequest("/users", Method.Get);
            var resualt = await client.GetAsync<List<User>>(restRequest);

            return await Task.FromResult(resualt);
        }

      

    }
}
