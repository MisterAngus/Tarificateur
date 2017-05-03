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
    public class GarantiesController : ApiController
    {
        private APP_DBEntities db = new APP_DBEntities();

        /// <summary>
        /// Get Garanties
        /// </summary>
        public Models.Garantie GetGaranties(DateTime DayOfBirth, int IdProf, int IdStatut, double NbPass)
        {
            return GetGaranties(DayOfBirth, IdProf, IdStatut, NbPass, null);
        }

        /// <summary>
        /// Get Garanties
        /// </summary>
        public Models.Garantie GetGaranties(DateTime DayOfBirth, int IdProf, int IdStatut, double NbPass, double? TxCourtier)
        {
            Models.Garantie result = new Models.Garantie();

            return result;
        }


    }
}