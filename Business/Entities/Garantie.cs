using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Runtime.Serialization;
using System.Web.Http.Description;

namespace Business.Entities
{
    public class Garantie
    {
        APP_DBEntities db = new APP_DBEntities();

        private class TypePrev
        {
            public int noBase { get; set; }
            public string nTypePrev { get; set; }

            public bool canDiscount { get; set; } = false;
        }

        private List<TypePrev> TypePrevs = new List<TypePrev>()
        {
            new TypePrev() {noBase = 31, nTypePrev = "633|619"},
            new TypePrev() {noBase = 31, nTypePrev = "610"},
            new TypePrev() {noBase = 31, nTypePrev = "634", canDiscount = true},
            new TypePrev() {noBase = 31, nTypePrev = "622", canDiscount = true},
            new TypePrev() {noBase = 31, nTypePrev = "621"},
            new TypePrev() {noBase = 32, nTypePrev = "637|619"},
            new TypePrev() {noBase = 32, nTypePrev = "610"},
            new TypePrev() {noBase = 32, nTypePrev = "611"},
            new TypePrev() {noBase = 32, nTypePrev = "612"},
            new TypePrev() {noBase = 32, nTypePrev = "635", canDiscount = true},
            new TypePrev() {noBase = 32, nTypePrev = "622"},
            new TypePrev() {noBase = 32, nTypePrev = "621"},
            //new TypePrev() {noBase = 32, nTypePrev = "631"},
            //new TypePrev() {noBase = 32, nTypePrev = "632"},
            //new TypePrev() {noBase = 32, nTypePrev = "638"}
        };

        private const int PASS = 39228;
        private const double txAssureur = 0.04;
        private const double txCipres = 0.12;
        private const double txGestion = 0.07;
        private double TxCourtier = 0;
        private bool Salarie = false;

        public class Detail
        {
            /// <summary>
            /// Type de garantie
            /// </summary>
            public string Name { get; set; }
            [IgnoreDataMember]
            public double[] Tx = new double[3];
            /// <summary>
            /// Cotisations en %<br/>Taux[0] -> jusqu'à 1 PASS<br/>Taux[1] -> de 1,1 à 4 PASS<br/>Taux[2] -> de 4,1 à 8 PASS
            /// </summary>
            public double[] Taux = new double[3];
        }

        public List<Detail> Details;

        public enum PACK
        {
            /// <summary>
            /// Pack Pro Access
            /// </summary>
            PPA,
            /// <summary>
            /// Pack Pro Innovation
            /// </summary>
            PPI,
            /// <summary>
            /// Pack Pro Excellence
            /// </summary>
            PPE,
            /// <summary>
            /// Impossible de définir le type de pack
            /// </summary>
            UNKNOW
        }

        /// <summary>
        /// Montant du traitment de base en €
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Date de naissance reçue en paramètre
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// La variable Age se calcule par différence entre l'année d'établissement du devis et l'année de naissance (age millésime)
        /// </summary>
        [DataMember]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int result = today.Year - BirthDay.Year;
                if (BirthDay > today.AddYears(-result)) result--;

                if (result < 18 || result > 60)
                {
                    throw new Exception("The age must be between 18 and 60 years");
                }

                return result;
            }
            set { }
        }
        /// <summary>
        /// Id de la profession reçu en paramètre
        /// </summary>
        public int IdProf { get; set; }
        /// <summary>
        /// Libellé de la profession
        /// </summary>
        public string Profession { get; set; }
        /// <summary>
        /// Barème d'incapacité permanente applicable à la profession 
        /// </summary>
        public string Bareme { get; set; }
        /// <summary>
        /// Id du statut reçu en paramètre
        /// </summary>
        public int IdStatut { get; set; }
        /// <summary>
        /// Libellé du statut
        /// </summary>
        public string Statut { get; set; }
        /// <summary>
        /// Collège d'appartenance
        /// </summary>
        public string College { get; set; }
        /// <summary>
        /// Montant annuel à assurer reçu en paramètre
        /// </summary>
        public double NbPass { get; set; }
        /// <summary>
        /// Montant de la cotisation en € / mois
        /// </summary>
        public double AnnualAmount
        {
            get
            {
                return NbPass * PASS;
            }
            set { }
        }
        public PACK Pack
        {
            get
            {
                PACK result = PACK.UNKNOW;

                if (NbPass <= 1)
                {
                    result = PACK.PPA;
                }
                else
                {
                    if (NbPass <= 3)
                    {
                        result = PACK.PPI;
                    }
                    else
                    {
                        result = PACK.PPE;
                    }
                }

                return result;
            }
            set { }
        }
        /// <summary>
        /// Nom du pack de la gamme
        /// </summary>
        public string PackName
        {
            get
            {
                switch (Pack)
                {
                    case PACK.PPA:
                        return "ACCESS PPA";
                    case PACK.PPI:
                        return "INNOVATION PPI";
                    case PACK.PPE:
                        return "EXCELLENCE PPE";
                }

                return string.Empty;
            }
            set { }
        }

        [IgnoreDataMember]
        public int? NoBase
        {
            get
            {
                int? result = null;
                if (NbPass <= 1)
                {
                    result = 31;
                }
                else
                {
                    if (NbPass < 4)
                    {
                        result = 32;
                    }
                    else
                    {
                        result = 33;
                    }
                }

                return result;
            }
        }

        [IgnoreDataMember]
        public string IdTarifLigne { get; set; }

        /// <summary>
        /// Sample
        /// </summary>
        public Garantie()
        {
            BirthDay = new DateTime(1990, 1, 1);
            IdProf = 1020;
            IdStatut = 1;
            NbPass = 1.1;
            TxCourtier = 0.11;
            Calculate();
        }
        public Garantie(DateTime birthDay, int idProf, int idStatut, double nbPass, double? txCourtier, bool? salarie)
        {
            BirthDay = birthDay;
            IdProf = idProf;
            IdStatut = idStatut;
            NbPass = nbPass;
            TxCourtier = txCourtier ?? 0;
            Salarie = salarie ?? false;
        }

        public void Calculate()
        {
            var profession = db.Professions.Where(e => e.IdProf == IdProf).FirstOrDefault();

            if (profession == null)
            {
                throw new Exception(string.Format("Profession {0} not found", IdProf));
            }

            Profession = profession.Profession;
            Bareme = profession.Bareme;

            var lgnTarif = profession.Professions_Lgn_Tarifs.Where(e => e.IdStatutTns == IdStatut).FirstOrDefault();

            College = lgnTarif.Collèges.Collège;
            IdTarifLigne = lgnTarif.IdTarifLigne;

            var statut = Lists.Statuts.Get(IdProf).Where(e => e.IdStatut == IdStatut).FirstOrDefault();

            if (statut == null)
            {
                throw new Exception(string.Format("Statut {0} not found", IdStatut));
            }

            if (NbPass > statut.MaxPass)
            {
                throw new Exception(string.Format("NbPass must be lower than {0}", statut.MaxPass));
            }

            Statut = statut.Name;

            if (NoBase.HasValue)
            {
                Details = new List<Detail>();
                var tarifBaseAges = Lists.Tarifs.GetBaseAges(PackName, Age, IdTarifLigne);
                double txTotal = 1 - (txAssureur + txCipres + txGestion + TxCourtier);

                foreach (var typePrev in TypePrevs)
                {
                    if (typePrev.noBase == NoBase.Value)
                    {
                        var values = typePrev.nTypePrev.Split('|');
                        string name = string.Empty;
                        double txTechnique = 0;
                        double txBase = 0;
                        double txBaseAge = 0;

                        var detail = new Detail();
                        foreach (string value in values)
                        {
                            var tarifBaseAge = tarifBaseAges.Where(e => e.NTypePrev.ToString() == value).FirstOrDefault();
                            if (tarifBaseAge != null)
                            {
                                txBaseAge = tarifBaseAge.Tx ?? 0;
                            }

                            for (int i = 31; i <= NoBase.Value; i++)
                            {
                                var tarifTechnique = Lists.Tarifs.GetTechniques(i).Where(e => e.NTypePrev.ToString() == value).FirstOrDefault();
                                if (tarifTechnique != null)
                                {
                                    if (i == NoBase.Value)
                                    {
                                        name = name == string.Empty ? tarifTechnique.TypePrev : name + "/" + tarifTechnique.TypePrev;
                                        detail.Name = name;
                                    }
                                    txTechnique = tarifTechnique.Tx ?? 0;
                                }

                                var tarifBase = Lists.Tarifs.GetBases(PackName, i, IdTarifLigne).Where(e => e.NTypePrev.ToString() == value).FirstOrDefault();
                                if (tarifBase != null)
                                {
                                    txBase = tarifBase.Tx ?? 0;
                                }

                                detail.Tx[i - 31] += (txTechnique * txBase * txBaseAge * (typePrev.canDiscount && Salarie ? 0.9 : 1) * (Age <= 30 ? 0.5 : 1)) / txTotal;
                            }
                        }
                        Details.Add(detail);
                    }
                }

                //Total
                var total = new Detail() { Name = "Total" };

                foreach (var detail in Details)
                {
                    for (int i = 31; i <= NoBase.Value; i++)
                    {
                        detail.Taux[i - 31] = Math.Round(detail.Tx[i - 31], 2);
                        total.Tx[i - 31] += detail.Tx[i - 31];
                        total.Taux[i - 31] = Math.Round(total.Tx[i - 31], 2);
                    }
                }

                //Total euros
                Amount = NbPass <= 1 ? total.Tx[0] * NbPass * PASS : (total.Tx[0] * PASS) + (total.Tx[1] * (NbPass - 1) * PASS);
                Amount = Math.Round(Amount / 100 / 12, 2);

                Details.Add(total);
            }



        }

    }
}