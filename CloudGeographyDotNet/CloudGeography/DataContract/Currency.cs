namespace AngryMonkey.Cloud.Geography;

public class Currency
{
	public string Code { get; set; }
	public bool IsOffical { get; set; }
	public string Name { get; set; }
	public string NativeName { get; set; }
	public string NativeSymbol { get; set; }
	public string PluralShortName { get; set; }
	public string ShortName { get; set; }
	public string Symbol { get; set; }
	public int Decimal { get; set; } = 2;

	internal bool CodeCheck(string code)
	{
		code = code.Trim().ToUpper();

		return Code == code;
	}
}
