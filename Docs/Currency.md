### For Currency:
---


#### Get all available currencies


```cs
List<Currency> Currencies = client.Currencies.Get();
```


#### Get a currecny from its code


 ```cs
Currency Currency = client.Currencies.Get("USD");
```


#### Get multiple currencies from currency codes


```cs
string[] currencyCodes = new(){"AFN", "USD"};

List<Currency> Currencies = client.Currencies.Get(new[] {currencyCodes});
```


#### Get  multiple currencies from country code


 ```cs
 // by 2 letter country code
List<CountryCurrency> Currencies = client.Currencies.GetByCountry("US");

 // by 3 letter country code
List<CountryCurrency> Currencies = client.Currencies.GetByCountry("USA");

//Note: A country can have multiple currencies
```

