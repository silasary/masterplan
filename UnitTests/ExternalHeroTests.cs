using Masterplan.Data;
using Masterplan.Tools.Import;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{

    class ExternalHeroTests
    {
        [Test]
        public void LoadSingleIP4ECharacter()
        {
            var hero = new Hero();
            Assert.IsFalse(AppImport.ImportExternalHero(hero), "Not proving a key should cause it to fail");

            hero.Key = "agdpcGxheTRlchQLEgtDaGFyYWN0ZXJWMhjcp4gVDA";
            Assert.IsTrue(AppImport.ImportExternalHero(hero), "Providing a key, but no provider should fall back to iPlay4E");

            Assert.AreEqual("iPlay4E", hero.KeyProvider, 
                "When migrating to the new Provider-based system, old style heros should be marked as iPlay4E");

            hero.KeyProvider = "NOT A REAL PROVIDER";
            Assert.IsFalse(AppImport.ImportExternalHero(hero), "When a provider is removed or incorrectly set, it should fail nicely.");
        }

        [Test]
        public void LoadIP4EParty()
        {
            var party = new Party()
            {
                Key = "ag1zfmlwbGF5NGUtaHJkchULEghDYW1wYWlnbhiAgIDwuceUCgw",
                KeyProvider = "iPlay4E"
            };
            var heroes = AppImport.ImportParty(party);
            Assert.IsNotEmpty(heroes);
        }
    }
}
