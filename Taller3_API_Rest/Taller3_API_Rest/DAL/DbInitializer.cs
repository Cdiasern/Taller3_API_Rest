using Taller3_API_Rest.DAL.Entities;

namespace Taller3_API_Rest.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Holidays.Any()) return;

            var holidays = new List<Holiday>();

            for (int year = 2020; year <= 2025; year++)
            {
                // Festivos Fijos
                holidays.Add(new Holiday { Day = 1, Month = 1, Name = "Año Nuevo", Type = 1 });
                holidays.Add(new Holiday { Day = 1, Month = 5, Name = "Día del Trabajo", Type = 1 });
                holidays.Add(new Holiday { Day = 20, Month = 7, Name = "Día de la Independencia", Type = 1 });
                holidays.Add(new Holiday { Day = 7, Month = 8, Name = "Batalla de Boyacá", Type = 1 });
                holidays.Add(new Holiday { Day = 8, Month = 12, Name = "Día de la Inmaculada Concepción", Type = 1 });
                holidays.Add(new Holiday { Day = 25, Month = 12, Name = "Navidad", Type = 1 });

                DateTime easterSunday = CalculateEasterSunday(year);
                holidays.Add(new Holiday { Day = 6, Month = 1, Name = "Día de los Reyes Magos", Type = 2 });
                holidays.Add(new Holiday { Day = 19, Month = 3, Name = "Día de San José", Type = 2 });
                holidays.Add(new Holiday { Day = 29, Month = 6, Name = "San Pedro y San Pablo", Type = 2 });
                holidays.Add(new Holiday { Day = 12, Month = 10, Name = "Día de la Raza", Type = 2 });
                holidays.Add(new Holiday { Day = 1, Month = 11, Name = "Día de Todos los Santos", Type = 2 });
                holidays.Add(new Holiday { Day = 11, Month = 11, Name = "Independencia de Cartagena", Type = 2 });

                holidays.Add(new Holiday { Day = easterSunday.AddDays(-3).Day, Month = easterSunday.AddDays(-3).Month, Name = "Jueves Santo", Type = 3 });
                holidays.Add(new Holiday { Day = easterSunday.AddDays(-2).Day, Month = easterSunday.AddDays(-2).Month, Name = "Viernes Santo", Type = 3 });
                holidays.Add(new Holiday { Day = easterSunday.Day, Month = easterSunday.Month, Name = "Domingo de Pascua", Type = 3 });
                holidays.Add(new Holiday { Day = easterSunday.AddDays(40).Day, Month = easterSunday.AddDays(40).Month, Name = "Ascensión del Señor", Type = 4 });
                holidays.Add(new Holiday { Day = easterSunday.AddDays(61).Day, Month = easterSunday.AddDays(61).Month, Name = "Corpus Christi", Type = 4 });
                holidays.Add(new Holiday { Day = easterSunday.AddDays(68).Day, Month = easterSunday.AddDays(68).Month, Name = "Sagrado Corazón de Jesús", Type = 4 });
            }

            context.Holidays.AddRange(holidays);
            context.SaveChanges();
        }

        //Método para calcular los festivos de semana santa
        public static DateTime CalculateEasterSunday(int year)
        {
            int a = year % 19;
            int b = year % 4;
            int c = year % 7;
            int d = (19 * a + 24) % 30;
            int e = (2 * b + 4 * c + 6 * d + 5) % 7;
            int daysFromMarch15 = 22 + d + e;

            return new DateTime(year, 3, 15).AddDays(daysFromMarch15);
        }
    }
}