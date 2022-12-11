using System.Diagnostics.Metrics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace AngryMonkey.Cloud;

public partial class CloudGeographyClient
{
	public CountriesMethods Countries { get; set; }
	public LanguagesMethods Languages { get; set; }
	public CurrenciesMethods Currencies { get; set; }
	public SubdivisionsMethods Subdivisions { get; set; }
	public PhoneNumbersMethods PhoneNumbers { get; set; }
    public TimeZonesMethods TimeZones { get; set; }


    public CloudGeographyClient()
	{
		Countries = new CountriesMethods(this);
		Languages = new LanguagesMethods(this);
		Currencies = new CurrenciesMethods(this);
		Subdivisions = new SubdivisionsMethods(this);
		PhoneNumbers = new PhoneNumbersMethods(this);
		TimeZones = new TimeZonesMethods(this);

    }

	internal static T? DeserializeModel<T>(string fileName, string directory = "")
	{
		Assembly assembly = Assembly.GetExecutingAssembly();

		string fullFileName = $"AngryMonkey.Cloud.Data.{(!string.IsNullOrEmpty(directory) ? $"{directory}." : null)}{fileName.ToLower()}.json";

		using Stream stream = assembly.GetManifestResourceStream(fullFileName);
		StreamReader reader = new(stream);

		return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
	}
}