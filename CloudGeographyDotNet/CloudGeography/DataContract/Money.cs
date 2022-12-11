using System.ComponentModel;
using System.Text;

namespace AngryMonkey.Cloud.Geography;

public partial class Money
{
    // Private Variables

    private const int _nanoFactor = 1_000_000_000;

    // Public Properties

    public string Currency { get; private set; } = string.Empty;
    public bool IsEmpty { get; private set; } = false;
    public long Units { get; private set; } = 0;
    public int Nanos { get; private set; } = 0;
    public string UnitsString => string.Format("{0:n0}", Units);

    // Constructors

    public Money(string currencyCode)
    {
        Currency = currencyCode;
        IsEmpty = true;
    }

    public Money(string currencyCode, decimal value)
    {
        Currency = currencyCode;
        Units = decimal.ToInt64(value);
        Nanos = decimal.ToInt32((value - Units) * _nanoFactor);
    }

    public Money(string currencyCode, long units, int nanos = _nanoFactor)
    {
        Currency = currencyCode;

        while (nanos > _nanoFactor)
        {
            units++;
            nanos -= _nanoFactor;
        }

        while (nanos <= -_nanoFactor)
        {
            units--;
            nanos += _nanoFactor;
        }

        // Adjust Units Nanos different signs
        if (units > 0 && nanos < 0)
        {
            units--;
            nanos += _nanoFactor;
        }
        else if (units < 0 && nanos > 0)
        {
            units++;
            nanos -= _nanoFactor;
        }

        Units = units;
        Nanos = nanos;
    }

    // Public Methods

    public decimal Value => Units + ((decimal)Nanos / _nanoFactor);
    public int DecimalNumberAsInteger => Nanos == 0 ? 0 : int.Parse(Nanos.ToString().TrimEnd('0'));

    public string NanosString(int digits)
    {
        string nanosString = Nanos.ToString();

        if (nanosString.Length == digits)
            return nanosString;

        if (nanosString.Length > digits)
            return nanosString.Remove(digits);

        while (nanosString.Length < digits)
            nanosString += "0";

        return nanosString;
    }

    public string Sentence
    {
        get
        {
            StringBuilder builder = new();

            builder.Append(ToSentence(Units));

            if (Nanos != 0)
                builder.Append($" {SmallNumberToWord(DecimalNumberAsInteger)}");

            builder[0] = builder[0].ToString().ToUpper()[0];

            return builder.ToString().Trim();
        }
    }

    public Money Clone() => new(Currency, Units, Nanos);

    // Private Methods

    private static string ToSentence(long units)
    {
        if (units == 0)
            return "zero";

        if (units < 0)
            return $"minus {ToSentence(Math.Abs(units))}";

        StringBuilder builder = new();

        if ((units / 1000000) > 0)
        {
            builder.Append($"{ToSentence(units / 1000000)} million ");
            units %= 1000000;
        }

        if ((units / 1000) > 0)
        {
            builder.Append($"{ToSentence(units / 1000)} thousand ");
            units %= 1000;
        }

        if ((units / 100) > 0)
        {
            builder.Append($"{ToSentence(units / 100)} hundred ");
            units %= 100;
        }

        if (units > 0)
        {
            if (builder.Length > 0)
                builder.Append("and ");

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (units < 20)
                builder.Append(unitsMap[units]);
            else
            {
                builder.Append(tensMap[units / 10]);

                if ((units % 10) > 0)
                    builder.Append($"- {unitsMap[units % 10]}");
            }
        }

        return builder.ToString().Trim();
    }

    private static string SmallNumberToWord(int decimalNumber)
    {
        if (decimalNumber == 0)
            return string.Empty;

        StringBuilder builder = new();

        var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        if (decimalNumber < 20)
            builder.Append(unitsMap[decimalNumber]);
        else
        {
            builder.Append(tensMap[decimalNumber / 10]);

            if ((decimalNumber % 10) > 0)
                builder.Append($"- {unitsMap[decimalNumber % 10]}");
        }

        return builder.ToString().Trim();
    }

    #region Calculations

    public Money Add(Money money)
    {
        if (!Currency.Equals(money.Currency, StringComparison.OrdinalIgnoreCase))
            throw new Exception("Cannot add money with different currency.");

        if (IsEmpty) return money;
        if (money.IsEmpty) return this;

        long units = Units + money.Units;
        int nanos = Nanos + money.Nanos;

        return new Money(Currency, units, nanos);
    }

    public Money Subtract(Money money)
    {
        if (!Currency.Equals(money.Currency, StringComparison.OrdinalIgnoreCase))
            throw new Exception("Cannot subtract money with different currency.");

        if (IsEmpty) return money;
        if (money.IsEmpty) return this;

        long units = Units - money.Units;
        int nanos = Nanos - money.Nanos;

        return new Money(Currency, units, nanos);
    }

    public Money Add(decimal value)
    {
        Money money = Clone();

        Money newMoney = new(money.Currency, value);

        return money.Add(newMoney);
    }

    public Money Subtract(decimal value)
    {
        Money money = Clone();

        Money newMoney = new(money.Currency, value);

        return money.Subtract(newMoney);
    }

    public Money Multiple(decimal value)
    {
        if (IsEmpty) return this;

        Money money = Clone();

        Money newMoney = new(money.Currency, Value * value);

        return money.Add(newMoney);
    }

    public Money Divide(decimal value)
    {
        if (IsEmpty) return this;

        Money money = Clone();

        Money newMoney = new(money.Currency, Value / value);

        return money.Add(newMoney);
    }

    #endregion

    #region Operators

    public static implicit operator decimal(Money money) => money.Value;

    public static Money operator +(Money m1, Money m2) => m1.Add(m2);
    public static Money operator -(Money m1, Money m2) => m1.Subtract(m2);
    public static Money operator +(Money money, decimal value) => money.Add(value);
    public static Money operator -(Money money, decimal value) => money.Subtract(value);
    public static Money operator *(Money money, decimal value) => money.Multiple(value);
    public static Money operator /(Money money, decimal value) => money.Divide(value);
    public static bool operator ==(Money m1, Money m2) => m1.Currency.Equals(m2.Currency, StringComparison.Ordinal) && m1.Units == m2.Units && m1.Nanos == m2.Nanos;
    public static bool operator !=(Money m1, Money m2) => !m1.Currency.Equals(m2.Currency, StringComparison.Ordinal) && m1.Units == m2.Units && m1.Nanos == m2.Nanos;
    public static bool operator <(Money m1, Money m2)
    {
        if (!m1.Currency.Equals(m2.Currency, StringComparison.OrdinalIgnoreCase))
            throw new Exception("Cannot compare Money with different currencies");

        if (m1.Units < m2.Units)
            return true;

        return m1.Units == m2.Units && m1.Nanos < m2.Nanos;
    }
    public static bool operator >(Money m1, Money m2)
    {
        if (!m1.Currency.Equals(m2.Currency, StringComparison.OrdinalIgnoreCase))
            throw new Exception("Cannot compare Money with different currencies");

        if (m1.Units > m2.Units)
            return true;

        return m1.Units == m2.Units && m1.Nanos > m2.Nanos;
    }
    public static bool operator <=(Money m1, Money m2) => m1 == m2 || m1 < m2;
    public static bool operator >=(Money m1, Money m2) => m1 == m2 || m1 > m2;

    #endregion
}