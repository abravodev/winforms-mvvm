using System.Globalization;

namespace UserManager.DTOs
{
    public class LanguageDto
    {
        public CultureInfo Culture { get; set; }

        public bool Current { get; set; }
    }
}
