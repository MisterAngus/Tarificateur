//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Professions_Lgn_Tarifs
    {
        public int Id { get; set; }
        public int IdProf { get; set; }
        public int IdStatutTns { get; set; }
        public int Code_Collège { get; set; }
        public string IdTarifLigne { get; set; }
        public int ID_Regime_RO { get; set; }
    
        public virtual Professions Professions { get; set; }
        public virtual Statuts_Tns Statuts_Tns { get; set; }
        public virtual Collèges Collèges { get; set; }
    }
}
