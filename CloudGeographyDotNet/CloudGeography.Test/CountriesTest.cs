namespace CloudGeography.Test
{
    [TestClass]
    public class CountriesTest
    {
        [TestMethod]
        public void Get_Country_By_CountryCode()
        {
            CloudGeographyClient client = new();
            Country? country = client.Countries.Get("Us");

            Assert.AreEqual("United States of America", country?.Name);
        }

        [TestMethod]
        public void Get_Country_By_CountryCode_ThreeLetters()
        {
            CloudGeographyClient client = new();
            Country? country = client.Countries.Get("uSA");

            Assert.AreEqual("United States of America", country?.Name);
        }


        [TestMethod]
        public void Get_All_Countries()
        {
            CloudGeographyClient client = new();
            List<Country> country = client.Countries.Get();

            Assert.IsTrue(country.Any());
        }

        [TestMethod]
        public void Get_Countries_By_Country_Codes()
        {
            CloudGeographyClient client = new();
            List<Country> countries = client.Countries.Get(new[] { "US", "LB" });

            Assert.IsTrue(countries.Count == 2);

            Assert.AreEqual("United States of America", countries.First(key => key.Code.Equals("US", StringComparison.OrdinalIgnoreCase)).Name);
            Assert.AreEqual("Lebanon", countries.First(key => key.Code.Equals("LB", StringComparison.OrdinalIgnoreCase)).Name);
        }

        [TestMethod]
        public void Get_Countries_By_CountryCode_Cases()
        {
            CloudGeographyClient client = new();
            List<Country> countries = client.Countries.Get(new[] { "Us", "lB" });

            Assert.IsTrue(countries.Count == 2);

            Assert.AreEqual("United States of America", countries.First(key => key.Code.Equals("US", StringComparison.OrdinalIgnoreCase)).Name);
            Assert.AreEqual("Lebanon", countries.First(key => key.Code.Equals("LB", StringComparison.OrdinalIgnoreCase)).Name);
        }

        [TestMethod]
        public void Get_Countries_By_Calling_Code()
        {
            CloudGeographyClient client = new();
            List<Country> country = client.Countries.GetByCallingCode(1);
            Assert.IsTrue(country.Any());
        }

        [TestMethod]
        public void Guess_Countries_By_PhoneNumber()
        {
            CloudGeographyClient client = new();

            Country? usCountry = client.Countries.GuessCountryByPhoneNumber("+16265895784");

            Assert.AreEqual("US", usCountry?.Code);

            Country? usCountry2 = client.Countries.GuessCountryByPhoneNumber("+14");

            Assert.AreEqual("US", usCountry2?.Code);

            Country? caCountry = client.Countries.GuessCountryByPhoneNumber("+14185895784");

            Assert.AreEqual("CA", caCountry?.Code);

            Country? lbCountry = client.Countries.GuessCountryByPhoneNumber("+96176333687");

            Assert.AreEqual("LB", lbCountry?.Code);

            Country? noneCountry = client.Countries.GuessCountryByPhoneNumber("+0");

            Assert.IsNull(noneCountry);
        }
    }
}