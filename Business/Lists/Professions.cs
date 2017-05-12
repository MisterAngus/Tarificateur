using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Business.Lists
{
    public class Professions
    {
        public static List<Entities.Profession> Get
        {
            get
            {
                APP_DBEntities db = new APP_DBEntities();
                return db.Professions.Where(e => e.Fin_Prof == null || e.Fin_Prof > DateTime.Today).Select(e => new Entities.Profession() { IdProf = e.IdProf, Name = e.Profession }).ToList();
            }
        }
    }
}
