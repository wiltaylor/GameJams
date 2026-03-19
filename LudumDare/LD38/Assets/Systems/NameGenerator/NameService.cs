using System;
using System.Linq;
using Assets.Systems.Unit;

namespace Assets.Systems.NameGenerator
{
    public class NameService
    {
        private static NameService _instance;
        private NameList[] _nameLists;

        public static NameService Instance
        {
            get { return _instance ?? (_instance = new NameService()); }
        }

        public void AssignNames(NameList[] names)
        {
            _nameLists = names;
        }

        private string GetNameFromList(string list)
        {
            try
            {
                var rnd = new System.Random();
                return _nameLists.First(n => n.Name == list).List.OrderBy(n => rnd.Next()).First().Trim();
            }
            catch
            {
                return "Error";
            }
        }

        public string NewCityName()
        {
            return GetNameFromList("Cities");
        }

        public string NewUnitName(UnitFaction faction, UnitType type)
        {
            if (faction == UnitFaction.Demon)
            {
                return GetNameFromList("Demon");
            }

            switch (type)
            {
                case UnitType.Scout:
                    return GetNameFromList("Pesant");
                case UnitType.Worker:
                    return GetNameFromList("Pesant");
                case UnitType.Clearic:
                    return GetNameFromList("KnightFirst");
                case UnitType.Knight:
                    return GetNameFromList("KnightFirst") + " " + GetNameFromList("KnightLast");
                case UnitType.Spearman:
                    return GetNameFromList("Pesant");
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
    }
}
