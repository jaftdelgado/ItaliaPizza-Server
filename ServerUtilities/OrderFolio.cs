using System;
using System.Text;

namespace ServerUtilities
{
    public static class OrderFolio
    {
        public static string GenerateUniqueFolio(DateTime date, Func<string, bool> folioExists)
        {
            string datePart = date.ToString("yyMMdd");
            string folio;
            int attempt = 0;

            do
            {
                string randomLetters = GenerateRandomLetters(3);
                folio = $"ORD-{datePart}-{randomLetters}";
                attempt++;

                if (attempt > 100)
                    throw new Exception("No se pudo generar un folio único después de varios intentos.");

            } while (folioExists(folio));

            return folio;
        }

        private static string GenerateRandomLetters(int length)
        {
            var random = new Random();
            var builder = new StringBuilder(length);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }
    }
}
