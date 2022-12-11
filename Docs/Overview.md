## Summary
A .Net client to retrieve Countries, Languages, and Currencies information.The .Net client is built on .Net Standard so it supports Windows, Windows Store, Windows Phone, Mono, and Xamarin, also it has implemented an integrated caching so developers don't have to. Backend API is deployed on Microsoft Azure latest technology services. .Net client and backend API are both built using best practices as lead developers would expect.



## Code Sample
```cs
 //gets all countries
 CloudGeographyClient client = new();
 List<Country> country = client.Countries.Get();
```

```cs
//gets all currencies
CloudGeographyClient client = new();
List<Currency> Currencies = client.Currencies.Get();
```

```cs
//gets all languages
CloudGeographyClient client = new();
List<Language> languages = client.Languages.Get();
```
