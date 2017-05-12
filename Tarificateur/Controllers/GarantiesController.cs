using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    public class GarantiesController : ApiController
    {
        /// <summary>
        /// Paramètre pour récupérer la garantie
        /// </summary>
        public class Parameter
        {
            ///<summary>
            ///Date de naissance. L'âge du candidat à l'assurance doit être compris entre 18 et 60 ans pour souscrire.
            ///</summary>
            [Required]
            public DateTime BirthDay { get; set; }
            /// <summary>
            /// Profession. Id de la profession (ex : 1020)
            /// </summary>
            [Required]
            public int IdProf { get; set; }
            /// <summary>
            /// Statut. Id du statut
            /// </summary>
            [Required]
            public int IdStatut { get; set; }
            /// <summary>
            /// Montant annuel à assurer (traitement de base). Le montant du traitement de base à assurer doit correspondre au revenu net déclaré à l'administration fiscale l'année N-1( rémunération, dividendes…) éventuellement majoré d'au plus 80%.
            /// </summary>
            [Required]
            public double NbPass { get; set; }
            /// <summary>
            /// Taux courtier. Entre 0 et 3% (ex : 0.11)
            /// </summary>
            [DefaultValue(0)]
            public double? TxCourtier { get; set; } = 0;
            /// <summary>
            /// Tout entrepreneur qui emploie au moins un salarié bénéficie de l'avantage Employeur : -10% appliqués sur la garantie incapacité et rachat de franchise.
            /// </summary>
            [DefaultValue(false)]
            public bool? Salarie { get; set; } = false;
        }

        /// <summary>
        /// Retourne la garantie
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Garantie</returns>
        [HttpPost]
        public Business.Entities.Garantie GetGaranties([FromBody] Parameter parameter)
        {
            Business.Entities.Garantie garantie = new Business.Entities.Garantie(parameter.BirthDay, parameter.IdProf, parameter.IdStatut, parameter.NbPass, parameter.TxCourtier, parameter.Salarie);
            garantie.Calculate();

            return garantie;
        }

        /// <summary>
        /// Retourne la garantie
        /// </summary>
        /// <param name="BirthDay">Date de naissance. L'âge du candidat à l'assurance doit être compris entre 18 et 60 ans pour souscrire.</param>
        /// <param name="IdProf">Profession. Id de la profession (ex : 1020)</param>
        /// <param name="IdStatut">Statut. Id du statut</param>
        /// <param name="NbPass">Montant annuel à assurer (traitement de base). Le montant du traitement de base à assurer doit correspondre au revenu net déclaré à l'administration fiscale l'année N-1( rémunération, dividendes…) éventuellement majoré d'au plus 80%.</param>
        /// <param name="TxCourtier">Taux courtier. Entre 0 et 3% (ex : 0.11)</param>
        /// <param name="Salarie">Tout entrepreneur qui emploie au moins un salarié bénéficie de l'avantage Employeur : -10% appliqués sur la garantie incapacité et rachat de franchise.</param>
        /// <returns></returns>
        [HttpGet]
        public Business.Entities.Garantie GetGaranties(DateTime BirthDay, int IdProf, int IdStatut, double NbPass, double? TxCourtier = 0, bool? Salarie = false)
        {
            Business.Entities.Garantie garantie = new Business.Entities.Garantie(BirthDay, IdProf, IdStatut, NbPass, TxCourtier, Salarie);
            garantie.Calculate();

            return garantie;
        }
    }
}