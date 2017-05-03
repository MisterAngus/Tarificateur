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
    public class ProfessionsController : ApiController
    {
        private APP_DBEntities db = new APP_DBEntities();

        /// <summary>
        /// Get Professions List
        /// </summary>
        public List<Models.Profession> GetProfessions()
        {
            return db.Professions.Where(e => e.Fin_Prof == null || e.Fin_Prof > DateTime.Today).Select(e => new Models.Profession() { IdProf = e.IdProf, Name = e.Profession }).ToList();
        }


    }
}