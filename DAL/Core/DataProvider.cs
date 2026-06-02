using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL.Core
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance { 
            get {if(instance == null) instance = new DataProvider(); return instance; }
            private set {instance = value;}
        }

        public DataProvider() { }

        private string connSTR = ConfigurationManager.ConnectionStrings["SupermarketDB"].ConnectionString;
    }
}
