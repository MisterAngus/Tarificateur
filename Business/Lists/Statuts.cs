using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Business.Lists
{
    public class Statuts
    {
        public static List<Entities.Statut> Get()
        {
            APP_DBEntities db = new APP_DBEntities();
            return db.Statuts_Tns.Select(e => new Entities.Statut() { IdStatut = e.IdStatutTns, Name = e.StatutTns }).ToList();
        }

        public static List<Entities.Statut> Get(int idProf)
        {
            APP_DBEntities db = new APP_DBEntities();
            return db.Professions_Lgn_Tarifs.Where(e => e.IdProf == idProf).Select(e => new Entities.Statut() { IdStatut = e.IdStatutTns, Name = e.Statuts_Tns.StatutTns }).ToList();
        }
    }
}
