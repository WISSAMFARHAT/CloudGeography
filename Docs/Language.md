### For Language:
---


#### Get all available langugaes


```cs
List<Language> languages = client.Languages.Get();
```


#### Get languages by language codes


```cs
string[] languageCodes = new(){"USA","CA"};

List<Language> languages = client.Languages.Get(languageCodes);
```


#### Get languages by country code


```cs
//by 2 letters code
List<CountryLanguage> languages = client.Languages.GetByCountry("US");

//by 3 letters code
List<CountryLanguage> languages = client.Languages.GetByCountry("USA");
```


#### Get language by language code


 ```cs
 //by 2 letter code
Language ?language = client.Languages.Get("EN");

//by 3 letter code
Language? language = client.Languages.Get("ENG");
```