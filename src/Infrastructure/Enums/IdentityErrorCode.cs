using System.Collections.Generic;

namespace Infrastructure.Enums
{
    public class IdentityErrorCode
    {
        public static readonly Dictionary<string, string> Errors = new Dictionary<string, string>()
        { {"DuplicateEmail", "Email" } };

        public static string GetField(string code)
        {
            Errors.TryGetValue(code, out var result);

            return result;
        }
    }
}
