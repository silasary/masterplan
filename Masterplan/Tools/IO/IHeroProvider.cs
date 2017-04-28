using Masterplan.Data;
using System.Collections.Generic;

namespace Masterplan.Tools.IO
{
    /// <summary>
    /// Interface that defines Hero Providers, such as iPlay4E.
    /// This should allow for Addin developers to hook Masterplan into ObsidianPortal, City Of Brass, or a custom cloud provider.
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
        /// Imports or updates a full party.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<Hero> ImportParty(string key, IEnumerable<Hero> ExistingHeroes);

        /// <summary>
        /// Returns a URL to that hero's URL.
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        string GetUrlString(Hero hero);
    }
}