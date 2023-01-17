namespace CUVU_Technical_Task.Application.Extensions;
public static class ApplicationExtensions
{
    public static int ToDurationInDay(this DateOnly d1, DateOnly d2)
    {
        var diff = d2.DayNumber - d1.DayNumber;
        return diff +1;
    }

    public static double CheckParkingPrice(this DateOnly date)
    {
        int month = date.Month;
        return month switch
        {
            >= 3 and <= 5 => 20.0,// spring price
            >= 6 and <= 8 => 25.0,// summer price
            >= 9 and <= 11 => 30.0,// autumn price
            _ => 35.0,// winter price
        };
    }

    public static double CheckParkingPrice(DateOnly startDate, DateOnly endDate)
    {
        double totalPrice = 0;
        for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
        {
            int month = date.Month;
            totalPrice += month switch
            {
                >= 3 and <= 5 => 20.0,// spring price
                >= 6 and <= 8 => 25.0,// summer price
                >= 9 and <= 11 => 30.0,// autumn price
                _ => 35.0,// winter price
            };
        }
        return totalPrice;
    }

    public static List<DateOnly> ToDates(this DateOnly startDate, DateOnly endDate)
    {
        List<DateOnly> dates = new();
        for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
        {
            dates.Add(date);
        }
        return dates;
    }


}


