using Masterplan.Data;
using System.Collections.Generic;

namespace Masterplan.Tools.Import
{
    /// <summary>
    /// Interface that defines Hero Providers, such as iPlay4E.
    /// </summary>
    public interface IHeroProvider
    {
        /// <summary>
        /// Name of the Provider.  
        /// </summary>
        /// <remarks>
        /// Maps to Hero.KeyProvider.
        /// Used by AppImport to determine which provider to use.
        /// </remarks>
        string ProviderName { get; }

        /// <summary>
        /// Imports or updates a hero from the source.
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        bool ImportHero(Hero hero);

        /// <summary>
        /// Imports a full party.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<Hero> ImportParty(string key);

        /// <summary>
        /// Returns a URL to that hero's URL.
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        string GetUrlString(Hero hero);
    }
}