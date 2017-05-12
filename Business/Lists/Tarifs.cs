using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Business.Lists
{
    public class Tarifs
    {
        public static List<Entities.TTechnique> GetTechniques(int nBase)
        {
            APP_DBEntities db = new APP_DBEntities();
            var result = (from vgp in db.Version_Garanties_Prev
                          join
                            vgt in db.Version_Gar_Tarif on vgp.No_Version_Gar equals vgt.No_Version_Gar
                          join
                            fgp in db.Fournisseurs_Garanties_Prév on vgp.No_garantie equals fgp.No_Garantie
                          where (fgp.N__Produit == 948 || fgp.N__Produit == 951)
                          && vgt.No_Base == nBase
                          select new Entities.TTechnique()
                          {
                              NTypePrev = fgp.N__Type_Prev,
                              TypePrev = vgp.Type_Garantie,
                              Tx = vgt.Tx
                          }
                          ).ToList();

            return result;
        }

        public static List<Entities.TTechnique> GetBases(string niveau, int nBase, string idTarifLigne)
        {
            APP_DBEntities db = new APP_DBEntities();
            return db.Tarifs_Coef_Bases.Where(e => e.Niveau == niveau && e.Base == nBase && e.IdTarifLigne == idTarifLigne).Select(e => new Entities.TTechnique() { NTypePrev = e.Type_prev, Tx = e.Coef_Tarif }).ToList();
        }

        public static List<Entities.TTechnique> GetBaseAges(string niveau, int age, string idTarifLigne)
        {
            APP_DBEntities db = new APP_DBEntities();
            int fage = 0;

            if (age > 35)
            {
                fage = 36;
            }

            if (age > 40)
            {
                fage = 41;
            }

            if (age > 45)
            {
                fage = 46;
            }

            if (age > 50)
            {
                fage = 51;
            }

            if (age > 55)
            {
                fage = age;
            }


            return db.Tarifs_Coef_Ages.Where(e => e.Niveau == niveau && e.Age == fage && e.IdTarifLigne == idTarifLigne).Select(e => new Entities.TTechnique() { NTypePrev = e.Type_prev, Tx = e.Coef_Tarif }).ToList();
        }
    }
}
