using Client.NetCore.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public string Image
        {
            get
            {
                return ApiManager.GetImage(Id);
            }
        }

        public string FullName
        {
            get
            {
                return Name + " " + LastName;
            }
        }

    
    }
}
