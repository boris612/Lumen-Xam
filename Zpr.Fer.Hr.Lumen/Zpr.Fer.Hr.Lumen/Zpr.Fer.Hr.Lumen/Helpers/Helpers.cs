using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zpr.Fer.Hr.Lumen.Helpers
{
    public class Language
    {
        public string DisplayName { get; }
        public string Code { get; }

        public Language(string displayName, string code)
        {
            DisplayName = displayName ?? string.Empty;
            Code = code ?? string.Empty;
        }
    }
    public static class Constants
    {
        public static readonly Language CroatianLanguge = new Language("Hrvatski", "hr-HR");
        public static readonly IEnumerable<Language> Languages = new List<Language>
        {
            CroatianLanguge
        };
    }
}
