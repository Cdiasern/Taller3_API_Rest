using Taller3_API_Rest.DAL.Entities;

namespace Taller3_API_Rest.Services
{
    public static class HolidayCalculator
    {
        public static DateTime CalculateEasterDate(int year)
        {
            int a = year % 19;
            int b = year % 4;
            int c = year % 7;
            int d = (19 * a + 24) % 30;
            int e = (2 * b + 4 * c + 6 * d + 5) % 7;
            int daysFromMarch15 = 22 + d + e;

            return new DateTime(year, 3, 15).AddDays(daysFromMarch15);
        }

        public static DateTime? CalculateHolidayDate(Holiday holiday, int year)
        {
            switch (holiday.Type)
            {
                case 1:
                    return new DateTime(year, holiday.Month, holiday.Day);
                case 2:
                    DateTime date = new DateTime(year, holiday.Month, holiday.Day);
                    return date.DayOfWeek == DayOfWeek.Sunday ? date.AddDays(1) : date;
                case 3:
                    return CalculateEasterDate(year).AddDays(holiday.DaysFromEaster ?? 0);
                case 4:
                    DateTime easterDate = CalculateEasterDate(year).AddDays(holiday.DaysFromEaster ?? 0);
                    return easterDate.DayOfWeek == DayOfWeek.Sunday ? easterDate.AddDays(1) : easterDate;
                default:
                    return null;
            }
        }
    }
}
