### For Money:
---
#### add money


```cs
//Positive Addition
Money moneyA = new("USD", 1.2m);
Money moneyB = new("USD", 1.4m);
//2.6m
Money sum = moneyA.Add(moneyB); 

//Negatice Addition
Money moneyA = new("USD", -1.2m);
Money moneyB = new("USD", -1.4m);
//-2.6m
Money sum = moneyA.Add(moneyB);
```


#### subtract money


```cs
//Positive Subtraction
Money moneyA = new("USD", 1.2m);
Money moneyB = new("USD", 1.4m);
//-.2m
Money sum = moneyA.Subtract(moneyB); 

//Negatice Subtraction
Money moneyA = new("USD", -1.2m);
Money moneyB = new("USD", -1.4m);
//.2m
Money sum = moneyA.Subtract(moneyB);
```

#### Get decimal part of the money as an integer

```cs
//Positive Number
Money money = new("USD", 50.3m);
int decimalPart = money.DecimalNumberAsInteger;
// decimalPart = 3

//Negative Number
Money money = new("USD", -50.3m);
int decimalPart = money.DecimalNumberAsInteger;
// decimalPart = -3
```