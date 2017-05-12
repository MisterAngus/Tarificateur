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
using Business.Lists;

namespace Tarificateur.Controllers
{
    public class ProfessionsController : ApiController
    {

        /// <summary>
        /// Retourne la liste professions
        /// </summary>
        public List<Business.Entities.Profession> GetProfessions()
        {
            var professions = Professions.Get;
            return professions;
        }


    }
}