using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;

using Utils;

using Masterplan.Data;
using Masterplan.Tools.IO;

namespace Masterplan.Tools
{
	class AppImport
	{
        internal static Dictionary<string, IHeroProvider> Providers;

        static AppImport()
        {
            Providers = new Dictionary<string, IHeroProvider>();
            var iplay4e = new iPlay4E();
            Providers.Add(iplay4e.ProviderName, iplay4e);
        }

        /// <summary>
        /// Directly import a Character Builder file
        /// </summary>
        /// <param name="xml">A string containing the contents of the .dnd4e file</param>
        public static Hero ImportHero(string xml)
        {
            return CharacterBuilderImporter.ImportHero(xml);
        }

        /// <summary>
        /// Directly import a monster from the Export function of the D&amp;D Insider Adventure Tools.
        /// </summary>
        /// <param name="xml">A string containing the contents of the file</param>
        public static Creature ImportCreature(string xml)
        {
            return AdventureToolsImporter.ImportCreature(xml);
        }

        /// <summary>
        /// Retrieve the External Hero Provider for a given hero.
        /// </summary>
        public static IHeroProvider GetProvider(Hero hero)
        {
            if (string.IsNullOrEmpty(hero.Key))
                return null;
            if (string.IsNullOrEmpty(hero.KeyProvider))
            {
                // It is assumed that any character that has a Key but no provider 
                // is from a previous version of Masterplan, and therefore from iPlay4E.
                hero.KeyProvider = "iPlay4E";
            }
            return GetProvider(hero.KeyProvider);
        }

        /// <summary>
        /// Retrieve the External Hero Provider for a given party.
        /// </summary>
        public static IHeroProvider GetProvider(Party party)
        {
            if (string.IsNullOrEmpty(party.Key))
                return null;
            return GetProvider(party.KeyProvider);
        }

        /// <summary>
        /// Directly request a given Hero Provider. 
        /// </summary>
        private static IHeroProvider GetProvider(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
                return null;
            if (Providers.ContainsKey(providerName))
            {
                return Providers[providerName];
            }
            return null;
        }

        /// <summary>
        /// Update an existing Hero with the latest data from an External Hero Provider.
        /// </summary>
        /// <param name="hero">The hero to Update</param>
        /// <returns>Whether the update was successful or not.</returns>
        public static bool ImportExternalHero(Hero hero)
        {
            IHeroProvider provider = GetProvider(hero);
            if (provider == null)
                return false;
            return provider.ImportHero(hero);
        }

        [Obsolete("Import using a Party instead.")]
        public static List<Hero> ImportParty(string key)
        {
            return GetProvider("iPlay4E").ImportParty(key, new Hero[0]);
        }

        public static List<Hero> ImportParty(Party party, IEnumerable<Hero> ExistingHeroes)
        {
            return GetProvider(party)?.ImportParty(party.Key, ExistingHeroes) ?? new List<Hero>();
        }

        internal static string GetUrlString(Hero hero)
        {
            return GetProvider(hero)?.GetUrlString(hero);
        }

    }
}
