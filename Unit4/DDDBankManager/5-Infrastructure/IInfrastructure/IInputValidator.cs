using System;

namespace DDDBankManager
{
    public interface IInputValidator
    {
        (DateTime validatedInput, string error) ParseDate(string userInput);
        (decimal validatedInput, string error) ParseDecimal(string userInput, int minimumValue);
        (int validatedInput, string error) ParseInteger(string userInput, int minimumValue);
        (string validatedInput, string error) ParseString(string userInput);
    }
}