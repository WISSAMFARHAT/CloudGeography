using AngryMonkey.Cloud.Geography;
using System.Collections.ObjectModel;
using System;
using System.Text.RegularExpressions;

namespace AngryMonkey.Cloud;

public partial class CloudGeographyClient
{
    public partial class CountriesMethods
    {
        private CloudGeographyClient Client { get; set; }

        internal CountriesMethods(CloudGeographyClient client) => Client = client;

        public List<Country> Get(params string[] countryCodes) => countryCodes.Any() ? CountriesList.Where(key => countryCodes.Any(c => key.CodeCheck(c))).ToList() : CountriesList;

        public Country? Get(string countryCode) => CountriesList.FirstOrDefault(key => key.CodeCheck(countryCode));

        public List<Country> GetByCallingCode(int callingCode)
        {
            string callingCodeString = callingCode.ToString().TrimStart('0');

            callingCode = int.Parse(callingCodeString);

            return CountriesList.Where(key => key.CallingCode == callingCode).ToList();
        }

        private List<string> CanadianCallingCodes { get; } = new()
        {
            "403", "587", "780", "825", // Alberta
			"236", "250", "604", "672", "778", // British Columbia
			"204", "431", // Manitoba
			"506", // New Brunswick
			"709", // New Brunswick
			"709", // Newfoundland and Labrador
			"902", "782", // Nova Scotia and Prince Edward Island
			"226", "249", "289", "343", "365", "416", "437", "519", "548", "613", "647", "705", "807", "905", // Ontario
			"418", "438", "450", "514", "579", "581", "819", "873", // Quebec
			"306", "639", // Saskatchewan
			"867" // Yukon, Northwest Territories and Nunavut
		};

        public Country? GuessCountryByPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return null;

            if (phoneNumber.StartsWith("00"))
                phoneNumber = $"+{phoneNumber[2..]}";

            if (phoneNumber.StartsWith("0"))
                phoneNumber = phoneNumber[1..];

            if (phoneNumber.StartsWith('+'))
                phoneNumber = phoneNumber[1..];

            if (phoneNumber.StartsWith("1"))
            {
                phoneNumber = phoneNumber[1..];

                if (phoneNumber.Length < 3)
                    return Get("US");

                string subdivisionCode = phoneNumber[..3];

                if (CanadianCallingCodes.Contains(subdivisionCode))
                    return Get("CA");

                return Get("US");
            }

            string prefix = "";

            for (int i = 0; i < 3; i++)
                try
                {
                    prefix = $"{prefix}{phoneNumber[i]}";

                    List<Country> countries = GetByCallingCode(int.Parse(prefix));

                    if (countries.Any())
                        return countries.First();
                }
                catch { }

            return null;
        }
    }
}