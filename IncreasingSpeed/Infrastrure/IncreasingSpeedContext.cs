using IncreasingSpeed.Models;
using System.Collections.Generic;

namespace IncreasingSpeed.Infrastrure
{
    public class IncreasingSpeedContext
    {
        private UsersApi UsersApi=new UsersApi();

        public List<User> users = new List<User>();

        public IncreasingSpeedContext()
        {
            this.users = UsersApi.GetUsers().Result;
        }
    }
}
