### For TimeZones:
---
#### Get all timezone


```cs
List<TimeZoneInfo> timeZones = client.TimeZones.Get();
```


#### Get timezone by timezone Ids


```cs
string[] timeZonesIds = new[] {"Hawaiian Standard Time", "Middle East Standard Time", "Greenland Standard Time"};

List<TimeZoneInfo> timeZones = client.TimeZones.Get(timeZonesIds);
```
#### Get timezones by country code


```cs
List<CountryTimeZone> timeZones = client.TimeZones.GetByCountry("LB");
```


#### Get current time of a timezone by timezone id


```cs
string toTimeZone = "Eastern Standard Time";

DateTime convertedTime = client.TimeZones.GetTime(toTimeZone);
```



#### Convert UTC time to a timezone time by timezone id


```cs
string toTimeZone = "Eastern Standard Time";
DateTime UTCTime = DateTime.Parse("2022-11-09 10:00:00 AM");

DateTime convertedTime = client.TimeZones.GetTime(toTimeZone , UTCTime);
```

#### Convert time from a timezone to another timezone time by timezone ids


```cs
string toTimeZone = "Afghanistan Standard Time";
string fromTimeZone = "Middle East Standard Time";
DateTime timeToConvert = DateTime.Parse("2022-11-08 12:00:00 PM")

DateTime convertedTime = client.TimeZones.GetTime(toTimeZone, timeToConvert, fromTimeZone);
```
