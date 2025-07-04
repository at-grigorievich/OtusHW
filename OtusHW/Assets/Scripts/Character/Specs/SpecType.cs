using System.Collections.Generic;

namespace Core
{
    public enum SpecType: short
    {
        None = 0,
        MoveSpeed = 1,
        Stamina = 2,
        Dexterity = 3,
        Intelligence = 4,
        Damage = 5,
        Regeneration = 6
    }

    public static class SpecLocalization
    {
        private static Dictionary<SpecType, string> _specTagDic = new Dictionary<SpecType, string>()
        {
            { SpecType.None, "empty" },
            { SpecType.MoveSpeed, "Move Speed" },
            { SpecType.Stamina, "Stamina" },
            { SpecType.Dexterity, "Dexterity" },
            { SpecType.Intelligence, "Intelligence" },
            { SpecType.Damage, "Damage" },
            { SpecType.Regeneration, "Regeneration" },
        };

        public static string Localize(SpecType type)
        {
            string result = _specTagDic[SpecType.None];

            _specTagDic.TryGetValue(type, out result);

            return result;
        }
    }
}