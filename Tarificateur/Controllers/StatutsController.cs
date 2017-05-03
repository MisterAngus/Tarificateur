using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace Tarificateur.Controllers
{
    public class StatutsController : ApiController
    {
        private APP_DBEntities db = new APP_DBEntities();


        /// <summary>
        /// Get Status List
        /// </summary>
        public List<Models.Statut> GetStatuts()
        {
            return db.Statuts_Tns.Select(e => new Models.Statut() { IdStatut = e.IdStatutTns, Name = e.StatutTns }).ToList();
        }

        /// <summary>
        /// Get Status List given by IdProf
        /// </summary>
        public List<Models.Statut> GetStatuts(int IdProf)
        {
            return db.Professions_Lgn_Tarifs.Where(e => e.IdProf == IdProf).Select(e => new Models.Statut() { IdStatut = e.IdStatutTns, Name = e.Statuts_Tns.StatutTns }).ToList();
        }
    }
}