﻿using System;
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

namespace Tarificateur.Controllers
{
    public class StatutsController : ApiController
    {

        /// <summary>
        /// Retourne la listes des statuts
        /// </summary>
        public List<Business.Entities.Statut> GetStatuts()
        {
            return Business.Lists.Statuts.Get();
        }

        /// <summary>
        /// Retourne la listes des statuts pour un IdProf
        /// </summary>
        public List<Business.Entities.Statut> GetStatuts(int IdProf)
        {
            return Business.Lists.Statuts.Get(IdProf);
        }
    }
}