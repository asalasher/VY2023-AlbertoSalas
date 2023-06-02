using System;
using System.Globalization;

namespace DDDBankManager
{
    public class InputValidator : IInputValidator
    {

        public (int validatedInput, string error) ParseInteger(string userInput, int minimumValue)
        {
            if (int.TryParse(userInput, out int validatedInput) && validatedInput >= minimumValue)
            {
                return (validatedInput, null);
            }
            return (0, "Invalid input");
        }

        public (decimal validatedInput, string error) ParseDecimal(string userInput, int minimumValue)
        {
            if (decimal.TryParse(userInput, out decimal validatedInput) && validatedInput >= minimumValue)
            {
                return (validatedInput, null);
            }
            return (0, "Invalid input");
        }

        public (DateTime validatedInput, string error) ParseDate(string userInput)
        {
            if (DateTime.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validatedInput))
            {
                return (validatedInput, null);
            }
            return (new DateTime(), "Date input not valid");
        }

        public (string validatedInput, string error) ParseString(string userInput)
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                return (userInput, null);
            }
            return (null, "Invalid input, the string is null or empty");
        }

    }
}
