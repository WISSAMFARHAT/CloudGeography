### For Subdivisions:

#### Get all subdivisions by country code

```cs
List<Subdivision> subdivisions = client.Subdivisions.Get("US");
```

#### Get all subdivisions by country and subdivision code

```cs
string []  subdivisionCodes = new(){"AL", "AK", "AZ" };

List<Subdivision> subdivisions = client.Subdivisions.Get("US", subdivisionCodes);
```

#### Get a subdivisions by country and subdivision code

```cs
List<Subdivision> subdivisions = client.Subdivisions.Get("US", "AL");
```