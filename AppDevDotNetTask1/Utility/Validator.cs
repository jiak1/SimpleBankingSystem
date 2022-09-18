using System;
using System.Text.RegularExpressions;

namespace AppDevDotNetTask1
{
    class Validator
    {
        /// <summary>
        /// Validates that a given number meets all of the requirements specified in the parameters.
        /// If it isn't valid, output where the validation failed to the user.
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="fieldName">A neat field name to print if validation fails</param>
        /// <param name="maxCharacters">The maximum length/amount of characters this number can be</param>
        /// <param name="minCharacters">The minimum length/amount of character this number can be</param>
        /// <returns>Whether this value passed all validation checks</returns>
        public static bool Validate(int value, string fieldName, int maxCharacters = 30, int minCharacters = 1)
        {
            if (value == -1)
            {
                Console.WriteLine($"{fieldName} must be numeric.");
                return false;
            }

            if (value <= 0)
            {
                Console.WriteLine($"{fieldName} must be greater then zero.");
                return false;
            }

            if (value.ToString().Length < minCharacters)
            {
                Console.WriteLine($"{fieldName} must be at least {minCharacters} characters long.");
                return false;
            }

            if (value.ToString().Length > maxCharacters)
            {
                Console.WriteLine($"{fieldName} cannot be longer then {maxCharacters} characters.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that a given text value meets all of the requirements specified in the parameters.
        /// If it isn't valid, output where the validation failed to the user.
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="fieldName">A neat field name to print if validation fails</param>
        /// <param name="maxCharacters">The maximum length/amount of characters this number can be</param>
        /// <param name="minCharacters">The minimum length/amount of character this number can be</param>
        /// <returns>Whether this value passed all validation checks</returns>
        public static bool Validate(string value, string fieldName, int maxCharacters = 30, int minCharacters = 1)
        {
            if (value.Length == 0)
            {
                Console.WriteLine($"{fieldName} must not be empty.");
                return false;
            }

            if (value.Length < minCharacters)
            {
                Console.WriteLine($"{fieldName} must be at least {minCharacters} characters long.");
                return false;
            }

            if (value.Length > maxCharacters)
            {
                Console.WriteLine($"{fieldName} cannot be longer then {maxCharacters} characters.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that a given email meets all of the requirements specified in the parameters.
        /// If it isn't valid, output where the validation failed to the user.
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="fieldName">A neat field name to print if validation fails</param>
        /// <returns>Whether this value passed all validation checks</returns>
        public static bool ValidateEmail(string value, string fieldName)
        {
            // Check it passes all basic string validation checks before checking domain specific rules
            if (Validate(value, fieldName, 50, 5))
            {
                // Confirm the text contains at least two values seperate by an '@' symbol
                if (Regex.IsMatch(value, ".+@.+") == false)
                {
                    Console.WriteLine($"{fieldName} must be formatted correctly & contain a domain.");
                    return false;
                }

                // Confirm the domain belongs to gmail, outlook or uts
                if (Regex.IsMatch(value, ".+@((gmail|outlook)\\.com|uts\\.edu\\.au)$") == false)
                {
                    Console.WriteLine($"{fieldName} does not contain an allowed domain.");
                    return false;
                }
            }

            return true;
        }
    }
}