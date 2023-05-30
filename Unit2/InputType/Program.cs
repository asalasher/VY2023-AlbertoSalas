﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputType
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool booleanValue = false;
            int integerValue = 0;
            decimal decimalValue = 0;
            char charValue = 'a';
            string stringValue = "";
            DateTime dateTimeValue = new DateTime(2023, 5, 24);

            bool isValidInput;

            #region boolean value
            isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Introduce a boolean value");

                if (bool.TryParse(Console.ReadLine(), out bool validatedInput))
                {
                    isValidInput = true;
                    booleanValue = validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a boolean value");
                }
            }
            #endregion


            #region integer value
            isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Introduce an integer value");

                if (int.TryParse(Console.ReadLine(), out int validatedInput))
                {
                    isValidInput = true;
                    integerValue = validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer value");
                }
            }
            #endregion

            #region decimal value
            isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Introduce a decimal value");

                if (decimal.TryParse(Console.ReadLine(), out decimal validatedInput))
                {
                    isValidInput = true;
                    decimalValue = validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a decimal value");
                }
            }
            #endregion

            #region char value
            isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Introduce a character");

                if (char.TryParse(Console.ReadLine(), out char validatedInput))
                {
                    isValidInput = true;
                    charValue = validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a character");
                }
            }
            #endregion

            #region string value

            Console.WriteLine("Introduce a string line:");
            stringValue = Console.ReadLine();

            #endregion

            #region date value
            isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Introduce a date (MM/dd/yyyy):");

                if (DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    isValidInput = true;
                    dateTimeValue = date;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a date with this format: (MM/dd/yyyy)");
                }
            }
            #endregion

            #region print modified values

            Console.WriteLine("The opposite of your boolean is: {0}", !booleanValue);

            try
            {
                Console.WriteLine("The division is {0}/{1} = {2}", (decimal)integerValue, decimalValue, (decimal)integerValue / decimalValue);
            }
            catch
            {
                Console.WriteLine("Error when dividing. Check your decimal value is not 0");
            }

            Console.WriteLine("The new string is: {0}", charValue + "(" + stringValue + ")" + charValue);

            DateTime newDate = new DateTime(
                dateTimeValue.Year,
                dateTimeValue.Month,
                DateTime.DaysInMonth(dateTimeValue.Year, dateTimeValue.Month),
                23, 59, 59);

            Console.WriteLine("The modified date is: {0}", newDate.ToString());

            #endregion

            Console.WriteLine("Press a key to exit");
            Console.ReadLine();
        }
    }
}
