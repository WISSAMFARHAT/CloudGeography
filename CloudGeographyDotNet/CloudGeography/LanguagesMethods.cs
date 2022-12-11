using AngryMonkey.Cloud.Geography;

namespace AngryMonkey.Cloud;

public partial class CloudGeographyClient
{
	public partial class LanguagesMethods
	{
		private CloudGeographyClient Client { get; set; }

		internal LanguagesMethods(CloudGeographyClient client) => Client = client;

		public List<Language> Get(params string[] languageCodes) => languageCodes.Any() ? LanguagesList.Where(key => languageCodes.Any(l => key.CodeCheck(l))).ToList() : LanguagesList;

		public Language? Get(string languageCode) => LanguagesList.FirstOrDefault(key => key.CodeCheck(languageCode));

		public List<CountryLanguage> GetByCountry(string countryCode) => Client.Countries.Get(countryCode).Languages;

	}
}