using System;

if (args.Length != 5 || 
    !double.TryParse(args[0], out var sum)
    || !double.TryParse(args[1], out var rate)
    || !int.TryParse(args[2], out var term)
    || !int.TryParse(args[3], out var selectedMonth)
    || !double.TryParse(args[4], out var payment))
{
    Console.WriteLine("Usage:\nsum\t\tdouble\tСумма кредита, р\n" +
        "rate\t\tdouble\tГодовая процентная ставка, %\n" +
        "term\t\tint\tКоличество месяцев кредита\n" +
        "selectedMonth\tint\tНомер месяца кредита, в котором вносится досрочный платёж\n" +
        "payment\t\tdouble\tСумма досрочного платежа, р");
}
else
{
    double AnnuityPay (double sum, double n, double i)
        => sum * i * System.Math.Pow(1 + i, n)
        / (System.Math.Pow(1 + i, n) - 1);
    double ProcentPay (double all, double proc, double days)
        => all * proc *days / (100 * new DateTime(DateTime.Now.Year, 12, 31)
        .DayOfYear);
    int DaysPay()
        => (DateTime.Now - DateTime.Now.AddMonths(-1)).Days;
    double MounthsPay(double sum, double needPay, double proc)
        => Math.Log( sum / (sum - proc * needPay), 1 + proc);

    var tmp = DateTime.Now.AddMonths(1);
    var startDate = new DateTime(tmp.Year, tmp.Month, 1);
    double i = rate / 12 / 100;
    var annuity = AnnuityPay(sum, term, i);
    Console.WriteLine($"{annuity.ToString("N")}");
}

