using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Lists
{
    public class Db
    {
        public List<Entities.User> Users { get; set; }

        private static Db instance;
        private Db()
        {
            Users = new List<Entities.User>();
        }

        public static Db Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Db();
                }
                return instance;
            }
        }

    }
}
