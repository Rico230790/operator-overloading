using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace пример_3
{
    
   enum Currency
    {
        RUR,
        UAH,
        USD,
    }
    class Money
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; private set; }

        public Money(decimal amount, Currency currency)
        {
            Amount = Math.Round(amount, 2);
            Currency = currency;
        }
        public static Money operator +(Money m1, Money m2)
        {
            return m1.Currency == m2.Currency ? new Money(m1.Amount + m2.Amount, m1.Currency) : new Money(m1.Amount + Converter.Convert(m2, m1.Currency).Amount, m1.Currency);
        }
        public override string ToString()
        {
            return string.Format("{0,4:f} {1}", Amount, Currency);
        }
        public static bool operator ==(Money a, Money b)
        {
            if (a.Currency == b.Currency)
            {
                return a.Amount == b.Amount;
            }
            return a.Amount == Converter.Convert(b, a.Currency).Amount;
        }
        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }
    }
    static class Converter
    {
        public static Money Convert(Money money, Currency currency)
        {
            return new Money(money.Amount * GetExchangeRate(money.Currency, currency), currency);
        }
        private static decimal GetExchangeRate(Currency currencyFrom, Currency currencyTo)
        {
            return GetExchangeRateRelativeToRUR(currencyFrom) / GetExchangeRateRelativeToRUR(currencyTo);
        }
        private static decimal GetExchangeRateRelativeToRUR(Currency type)
        {
            //мфк
            switch (type)
            {
                case Currency.RUR:
                    return 1;
                case Currency.UAH:
                    return 2.7m;
                case Currency.USD:
                    return 66.1m;
                default:
                    throw new Exception("No exchage rate for " + type);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Money uah = new Money(1, Currency.UAH);
            Money uah2 = new Money(24.48m, Currency.UAH);
            Money rur = new Money(1, Currency.RUR);
            Money rur1 = new Money(1, Currency.RUR);
            Money rur2 = new Money(2, Currency.RUR);
            Money usd = new Money(1, Currency.USD);

            Console.WriteLine(rur.Amount + " " + rur.Currency + "\n");
            Console.WriteLine(Converter.Convert(rur, Currency.UAH));
            Console.WriteLine(Converter.Convert(rur, Currency.USD));

            Console.WriteLine("\n" + uah.Amount + " " + uah.Currency + "\n");
            Console.WriteLine(Converter.Convert(uah, Currency.RUR));
            Console.WriteLine(Converter.Convert(uah, Currency.USD));

            Console.WriteLine("\n" + usd.Amount + " " + usd.Currency + "\n");
            Console.WriteLine(Converter.Convert(usd, Currency.UAH));
            Console.WriteLine(Converter.Convert(usd, Currency.RUR));

            Console.WriteLine("\n" + "Суммирование денег" + "\n");
            Console.WriteLine(uah + " + " + rur + " = " + (uah + rur));
            Console.WriteLine(rur + " + " + usd + " + " + uah + " = " + (rur + usd + uah));
            Console.WriteLine("\n" + "Сравнивание денег" + "\n");
            Console.WriteLine(rur + (rur == rur1 ? " = " : " != ") + rur1);
            Console.WriteLine(rur + (rur == rur2 ? " = " : " != ") + rur2);
            Console.WriteLine(uah2 + (uah2 == usd ? " = " : " != ") + usd);
            Console.WriteLine(rur + (rur == usd ? " = " : " != ") + usd);
            Console.ReadKey();
        }
    }
}

