using System.Globalization;
using System.Text;

namespace GRRWS.Application.Common
{
    public static class StringHelper
    {
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            // Loại bỏ dấu, chuyển thành chữ in hoa, thay khoảng trắng bằng dấu gạch dưới
            return stringBuilder.ToString()
                .Normalize(NormalizationForm.FormC)
                .ToUpperInvariant()
                .Replace(" ", "_");
        }
    }
}
