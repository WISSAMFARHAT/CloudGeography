namespace CloudGeography.Test
{
	[TestClass]
	public class MoneyTest
	{
		[TestMethod]
		public void Addition_Positive()
		{
			Money moneyA = new("USD", 1.2m);
			Money moneyB = new("USD", 1.4m);

			Money sum = moneyA.Add(moneyB);

			Assert.AreEqual(2.6m, sum.Value);
		}

		[TestMethod]
		public void Addition_Negative()
		{
			Money moneyA = new("USD", -1.2m);
			Money moneyB = new("USD", -1.4m);

			Money sum = moneyA.Add(moneyB);

			Assert.AreEqual(-2.6m, sum.Value);
		}

		[TestMethod]
		public void Addition_PositiveCarry()
		{
			Money moneyA = new("USD", 1.6m);
			Money moneyB = new("USD", 1.8m);

			Money sum = moneyA.Add(moneyB);

			Assert.AreEqual(3.4m, sum.Value);
		}

		[TestMethod]
		public void Addition_NegativeCarry()
		{
			Money moneyA = new("USD", -1.6m);
			Money moneyB = new("USD", -1.8m);

			Money sum = moneyA.Add(moneyB);

			Assert.AreEqual(-3.4m, sum.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void Addition_DifferentCurrencies()
		{
			Money moneyA = new("USD", -1.6m);
			Money moneyB = new("LBP", -1.8m);

			moneyA.Add(moneyB);
		}

		[TestMethod]
		public void Subtraction_Positive()
		{
			Money moneyA = new("USD", 1.2m);
			Money moneyB = new("USD", 1.4m);

			Money sum = moneyA.Subtract(moneyB);

			Assert.AreEqual(-.2m, sum.Value);
		}

		[TestMethod]
		public void Subtraction_Negative()
		{
			Money moneyA = new("USD", -1.2m);
			Money moneyB = new("USD", -1.4m);

			Money sum = moneyA.Subtract(moneyB);

			Assert.AreEqual(.2m, sum.Value);
		}

		[TestMethod]
		public void Subtraction_PositiveCarry()
		{
			Money moneyA = new("USD", 1.6m);
			Money moneyB = new("USD", 1.8m);

			Money sum = moneyA.Subtract(moneyB);

			Assert.AreEqual(-.2m, sum.Value);
		}

		[TestMethod]
		public void Subtraction_NegativeCarry()
		{
			Money moneyA = new("USD", -1.2m);
			Money moneyB = new("USD", 1.8m);

			Money sum = moneyA.Subtract(moneyB);

			Assert.AreEqual(-3m, sum.Value);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void Subtraction_DifferentCurrencies()
		{
			Money moneyA = new("USD", -1.6m);
			Money moneyB = new("LBP", -1.8m);

			moneyA.Subtract(moneyB);
		}

		[TestMethod]
		public void GeDecimalNumberAsInteger_Zero()
		{
			Money money = new("USD", 50.0m);

			Assert.AreEqual(0, money.DecimalNumberAsInteger);
		}

		[TestMethod]
		public void GeDecimalNumberAsInteger_Positive()
		{
			Money money = new("USD", 50.3m);

			Assert.AreEqual(3, money.DecimalNumberAsInteger);
		}

		[TestMethod]
		public void GeDecimalNumberAsInteger_Negative()
		{
			Money money = new("USD", -50.3m);

			Assert.AreEqual(-3, money.DecimalNumberAsInteger);
        }
    }
}