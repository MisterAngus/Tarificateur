using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Web.Http.Description;

namespace Business.Entities
{
    public class Statut
    {
        public int IdStatut { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Nombre de pass maximum admis
        /// </summary>
        public double? MaxPass
        {
            get
            {
                double? result = null;

                switch (IdStatut)
                {
                    case 1:
                    case 2:
                    default:
                        result = 8;
                        break;

                    case 3:
                    case 4:
                        result = 1.5;
                        break;
                }

                return result;
            }
        }
    }
}