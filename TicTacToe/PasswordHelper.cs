using System;
using System.Security.Cryptography;

namespace TicTacToe
{
    /// <summary>
    /// Provides PBKDF2-based password hashing and constant-time verification.
    /// </summary>
    internal static class PasswordHelper
    {
        private const int SaltSize = 16;       // 128-bit salt
        private const int HashSize = 32;       // 256-bit hash
        private const int Iterations = 100000; // OWASP recommended minimum

        /// <summary>
        /// Hashes a plain-text password with a random salt.
        /// Returns a single Base64-encoded string containing [salt + hash].
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            byte[] hash = Pbkdf2(password, salt);

            byte[] combined = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, combined, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, combined, SaltSize, HashSize);

            return Convert.ToBase64String(combined);
        }

        /// <summary>
        /// Verifies a plain-text password against a stored hash produced by HashPassword.
        /// Uses a constant-time comparison to prevent timing attacks.
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                byte[] combined = Convert.FromBase64String(storedHash);
                if (combined.Length != SaltSize + HashSize) return false;

                byte[] salt = new byte[SaltSize];
                Buffer.BlockCopy(combined, 0, salt, 0, SaltSize);

                byte[] storedBytes = new byte[HashSize];
                Buffer.BlockCopy(combined, SaltSize, storedBytes, 0, HashSize);

                byte[] computed = Pbkdf2(password, salt);
                return SlowEquals(storedBytes, computed);
            }
            catch
            {
                return false;
            }
        }

        private static byte[] Pbkdf2(string password, byte[] salt)
        {
            using (var prf = new Rfc2898DeriveBytes(password, salt, Iterations))
                return prf.GetBytes(HashSize);
        }

        // Constant-time byte comparison — prevents timing side-channel attacks
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            int diff = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
