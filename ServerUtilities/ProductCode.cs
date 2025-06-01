using System;
using System.Text;

namespace ServerUtilities
{
    public static class ProductCodeGenerator
    {
        public static string GenerateUniqueProductCode(int categoryId, Func<string, bool> productCodeExists)
        {
            string prefix = GetCategoryPrefix(categoryId);
            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentException("Categoría inválida.");

            string code;
            int attempt = 0;

            do
            {
                string randomSuffix = GenerateRandomSuffix(4);
                code = $"{prefix}-{randomSuffix}";
                attempt++;

                if (attempt > 100)
                    throw new Exception("No se pudo generar un código único para el producto.");

            } while (productCodeExists(code));

            return code;
        }

        private static string GetCategoryPrefix(int categoryId)
        {
            switch (categoryId)
            {
                case 1: return "APP";
                case 2: return "SAL";
                case 3: return "PIQ";
                case 4: return "SWT";
                case 5: return "DSC";
                case 6: return "BAR";
                default: return null;
            }
        }

        private static string GenerateRandomSuffix(int length)
        {
            var random = new Random();
            var builder = new StringBuilder(length);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }
    }
}
