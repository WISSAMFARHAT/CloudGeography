using AngryMonkey.Cloud.Geography;

namespace AngryMonkey.Cloud;

public partial class CloudGeographyClient
{
    public class SubdivisionsMethods
    {
        private CloudGeographyClient Client { get; set; }

        internal SubdivisionsMethods(CloudGeographyClient client) => Client = client;

        public List<Subdivision> Get(string countryCode)
        {
            try
            {
                return SubdivisionsList[countryCode];
            }
            catch
            {
                return new List<Subdivision>();
            }
        }

        public List<Subdivision> Get(string countryCode, params string[] subdivisionCodes) => Get(countryCode).Where(key => subdivisionCodes.Any(s => key.CodeCheck(s))).ToList();

        public Subdivision? Get(string countryCode, string subdivisionCode) => Get(countryCode).FirstOrDefault(key => key.CodeCheck(subdivisionCode));
    }
}