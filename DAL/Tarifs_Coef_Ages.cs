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
    
    public partial class Tarifs_Coef_Ages
    {
        public int IdCoef { get; set; }
        public int Produit { get; set; }
        public string Niveau { get; set; }
        public int Type_prev { get; set; }
        public int Age { get; set; }
        public float Coef_Tarif { get; set; }
        public System.DateTime Coef_Debut { get; set; }
        public Nullable<System.DateTime> Coef_Fin { get; set; }
        public string IdTarifLigne { get; set; }
    }
}