using AngryMonkey.Cloud.Geography;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryMonkey.Cloud;
public partial class CloudGeographyClient
{
    public class TimeZonesMethods
    {
        private CloudGeographyClient Client { get; set; }

        internal TimeZonesMethods(CloudGeographyClient client) => Client = client;

        public List<TimeZoneInfo> Get(params string[] timeZoneCodes) => timeZoneCodes.Any() ? TimeZoneInfo.GetSystemTimeZones().ToList().Where(TimeZone => timeZoneCodes.Any(key => key == TimeZone.Id)).ToList() : TimeZoneInfo.GetSystemTimeZones().ToList();

        public DateTime GetTime(string toTimeZoneId) => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId));

        public DateTime GetTime(string toTimeZoneId, DateTime dateTime) => TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId));

        public DateTime GetTime(string toTimeZoneId, DateTime dateTime, string fromTimeZoneId) => TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId));

        public List<CountryTimeZone>? GetByCountry(string countryCode) => Client.Countries.Get(countryCode)?.TimeZones;

    }
}
