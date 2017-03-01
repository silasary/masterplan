using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools.Import
{
    public static class AppImport
    {
        internal static Dictionary<string, IHeroProvider> Providers;

        static AppImport()
        {
            Providers = new Dictionary<string, IHeroProvider>();
            var iplay4e = new IPlay4E();
            Providers.Add(iplay4e.ProviderName, iplay4e);
        }

        public static Hero ImportHero(string xml)
        {
            return CharacterBuilderImporter.ImportHero(xml);
        }

        public static Creature ImportCreature(string xml)
        {
            return CreatureImporter.ImportCreature(xml);
        }

        public static IHeroProvider GetProvider(Hero hero)
        {
            if (string.IsNullOrEmpty(hero.Key))
                return null;
            if (string.IsNullOrEmpty(hero.KeyProvider))
                hero.KeyProvider = "iPlay4E";
            if (Providers.ContainsKey(hero.KeyProvider))
            {
                return Providers[hero.KeyProvider];
            }
            return null;
        }

        public static IHeroProvider GetProvider(Party party)
        {
            if (string.IsNullOrEmpty(party.Key))
                return null;
            if (Providers.ContainsKey(party.KeyProvider))
            {
                return Providers[party.KeyProvider];
            }
            return null;
        }

        public static bool ImportExternalHero(Hero hero)
        {
            IHeroProvider provider = GetProvider(hero);
            if (provider == null)
                return false;
            return provider.ImportHero(hero);
        }

        [Obsolete]
        public static List<Hero> ImportParty(string key)
        {
            return new IPlay4E().ImportParty(key);
        }

        public static List<Hero> ImportParty(Party party)
        {
            return GetProvider(party)?.ImportParty(party.Key) ?? new List<Hero>();
        }

        internal static string GetUrlString(Hero hero)
        {
            return GetProvider(hero)?.GetUrlString(hero);
        }
    }
}
