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
    
    public partial class Fournisseurs_Garanties_Prév
    {
        public int No_Garantie { get; set; }
        public string Code_Garantie { get; set; }
        public Nullable<int> N__Produit { get; set; }
        public string Code_Fournisseur { get; set; }
        public string Type_Garantie { get; set; }
        public string Description { get; set; }
        public string niveau_garantie { get; set; }
        public Nullable<short> Code_Collège { get; set; }
        public Nullable<short> N__Type_Prev { get; set; }
        public Nullable<System.DateTime> Dt_Effet { get; set; }
        public Nullable<System.DateTime> Dt_Fin { get; set; }
        public Nullable<int> Typ_Fin { get; set; }
        public Nullable<int> Rang { get; set; }
        public Nullable<int> No_produits_typ_prev { get; set; }
        public string Select_critere { get; set; }
        public string CodeRisque { get; set; }
        public string NiveauRisque { get; set; }
        public string CodeEvolan { get; set; }
    }
}
