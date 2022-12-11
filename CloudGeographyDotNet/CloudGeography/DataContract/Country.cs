using System.Runtime.Serialization;

namespace AngryMonkey.Cloud.Geography;

public class Country
{
	public string Code { get; set; }
	public double? Area { get; set; }
	public int CallingCode { get; set; }
	public string Capital { get; set; }
	public string ThreeLettersCode { get; set; }
	public string Demonym { get; set; }
	public double? Latitude { get; set; }
	public string LocalName { get; set; }
	public double? Longitude { get; set; }
	public string Name { get; set; }
	public string ShortName { get; set; }
	public List<CountryCurrency> Currencies { get; set; }
	public List<CountryLanguage> Languages { get; set; }
    public List<CountryTimeZone> TimeZones { get; set; }


    internal bool CodeCheck(string code)
	{
		code = code.Trim().ToUpper();

		return Code == code || ThreeLettersCode == code;
	}
}
