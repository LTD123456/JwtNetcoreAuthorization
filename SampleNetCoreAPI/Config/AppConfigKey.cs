using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNetCoreAPI.Config
{
    public class AppConfigKey
    {
        public static IConfiguration Intance;
        public static string SecretKey
        {
            get
            {
                var appsettings = Intance.GetSection("Appsettings");
                return appsettings["SecretKey"] ?? "asdhsahdjsahdjksahdjhdsadadasdjsadadjhdjkhsadjksad";
            }
        }
    }
}
