using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Security
{
    using System.Globalization;
    using UseAlgorithm = SHA384Managed;

    public static class PasswordEncription
    {
        const int SaltValueSize = 10;
        
        #region from msdn
        public static string GenerateSaltValue()
        {
            UnicodeEncoding utf16 = new UnicodeEncoding();

            if (utf16 != null)
            {
                // Create a random number object seeded from the value
                // of the last random seed value. This is done
                // interlocked because it is a static value and we want
                // it to roll forward safely.

                Random random = new Random(unchecked((int)DateTime.Now.Ticks));

                if (random != null)
                {
                    // Create an array of random values.

                    byte[] saltValue = new byte[SaltValueSize];

                    random.NextBytes(saltValue);

                    // Convert the salt value to a string. Note that the resulting string
                    // will still be an array of binary values and not a printable string. 
                    // Also it does not convert each byte to a double byte.

                    string saltValueString = utf16.GetString(saltValue);

                    // Return the salt value as a string.

                    return saltValueString;
                }
            }

            return null;
        }

        public static string HashPassword(string clearData, string saltValue)
        {
            HashAlgorithm hash = new UseAlgorithm();
            UnicodeEncoding encoding = new UnicodeEncoding();

            if (clearData != null && hash != null && encoding != null && saltValue != null)
            {

                // Convert the salt string and the password string to a single
                // array of bytes. Note that the password string is Unicode and
                // therefore may or may not have a zero in every other byte.

                byte[] binarySaltValue = Encoding.Unicode.GetBytes(saltValue);
                byte[] valueToHash = new byte[SaltValueSize + encoding.GetByteCount(clearData)];
                byte[] binaryPassword = encoding.GetBytes(clearData);

                // Copy the salt value and the password to the hash buffer.

                binarySaltValue.CopyTo(valueToHash, 0);
                binaryPassword.CopyTo(valueToHash, SaltValueSize);

                byte[] hashValue = hash.ComputeHash(valueToHash);

                return BitConverter.ToString(hashValue);
            }

            return null;
        }

        #endregion

    }
}
