using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DotForums.Forum
{
    public class Security
    {
        private static readonly int _saltLen = 32;
        private static readonly int _keyLen = 32;

        private static byte[] GenerateHash(byte[] salt, string password, int count)
        {
            password = string.IsNullOrEmpty(password) ? string.Empty : password;
            return KeyDerivation.Pbkdf2
            (
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: count,
                numBytesRequested: _keyLen
            );
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[_saltLen];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }
            return salt;
        }

        /* https://lockmedown.com/hash-right-implementing-pbkdf2-net/ */
        private static bool ConstantTimeComparison(byte[] passwordGuess, byte[] actualPassword)
        {
            uint difference = (uint)passwordGuess.Length ^ (uint)actualPassword.Length;
            for (var i = 0; i < passwordGuess.Length && i < actualPassword.Length; i++)
                difference |= (uint)(passwordGuess[i] ^ actualPassword[i]);

            return difference == 0;
        }

        public static string GeneratePassword(string password, int count)
        {
            byte[] salt = GenerateSalt();
            byte[] hashed = GenerateHash(salt, password, count);
            byte[] iteration = BitConverter.GetBytes(count);
            byte[] navigation = new byte[_saltLen + _keyLen + iteration.Length];

            Buffer.BlockCopy(salt, 0, navigation, 0, _saltLen);
            Buffer.BlockCopy(hashed, 0, navigation, _saltLen, _keyLen);
            Buffer.BlockCopy(iteration, 0, navigation, _saltLen + _keyLen, iteration.Length);

            return Convert.ToBase64String(navigation);
        }

        public static bool ValidatePassword(string input, string passwordHash)
        {
            byte[] salt = new byte[_saltLen];
            byte[] password = new byte[_keyLen];
            byte[] navigation = Convert.FromBase64String(passwordHash);
            byte[] count = new byte[navigation.Length - _saltLen - _keyLen];

            Buffer.BlockCopy(navigation, 0, salt, 0, _saltLen);
            Buffer.BlockCopy(navigation, _saltLen, password, 0, _keyLen);
            Buffer.BlockCopy(navigation, _saltLen + _keyLen, count, 0, count.Length);

            byte[] hashed = GenerateHash(salt, input, BitConverter.ToInt32(count, 0));

            return ConstantTimeComparison(hashed, password);
        }
    }
}
