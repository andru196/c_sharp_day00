using System;

if (args.Length != 3 || 
	!double.TryParse(args[0], out var sum)
	|| !double.TryParse(args[1], out var rate)
	|| !int.TryParse(args[2], out var term))
{
	Console.WriteLine("Usage:\nsum\t\tdouble\tСумма кредита, р\n" +
		"rate\t\tdouble\tГодовая процентная ставка, %\n" +
		"term\t\tint\tКоличество месяцев кредита\n");
}
else
{
	double AnnuityPay (double sum, double n, double i)
		=> sum * i * System.Math.Pow(1 + i, n)
		/ (System.Math.Pow(1 + i, n) - 1);
	double ProcentPay (double all, double proc, double days)
		=> all * proc *days / (100 * new DateTime(DateTime.Now.Year, 12, 31)
		.DayOfYear);
	var tmp = DateTime.Now.AddMonths(1);
	var startDate = new DateTime(tmp.Year, tmp.Month, 1);
	double i = rate / 12 / 100;
	var annuity = AnnuityPay(sum, term, i);
	var pereplata = annuity * term - sum;
	Console.WriteLine($"Ежемесячный платёж {annuity.ToString("N")}");
	var indebtedness = sum;
	var date = new DateTime(2021, 05, 1);
	var j = 1;
	var proc = .0;
	while (indebtedness > annuity - proc)
	{
		proc = ProcentPay(indebtedness, rate, (date.AddMonths(1) - date).Days);
		date = date.AddMonths(1);
		indebtedness -= annuity - proc;
		Console.WriteLine($"{j}\t{date.ToShortDateString()}\t{annuity.ToString("N")}\t\t{(annuity - proc).ToString("N")}\t\t{proc.ToString("N")}\t\t{indebtedness.ToString("N")}");
		j++;
	}
}

