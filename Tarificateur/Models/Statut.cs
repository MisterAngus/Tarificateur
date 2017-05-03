using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarificateur.Models
{
    public class Statut
    {
        public int IdStatut { get; set; }
        public string Name { get; set; }
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