using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace userService.Security
{
    public class HashingManager
    {
        private const int DefaultIterations = 10000;

        private class HashVersion
        {
            public short Version { get; set; }
            public int SaltSize { get; set; }
            public int HashSize { get; set; }
            public KeyDerivationPrf KeyDerivation { get; set; }
        }

        private readonly Dictionary<short, HashVersion> _versions = new Dictionary<short, HashVersion>
        {
            {
                1, new HashVersion
                {
                    Version = 1,
                    KeyDerivation = KeyDerivationPrf.HMACSHA512,
                    HashSize = 256 / 8,
                    SaltSize = 128 / 8
                }
            }
        };

        private HashVersion DefaultVersion => _versions[1];

        public bool IsLatestHashVersion(byte[] data)
        {
            var version = BitConverter.ToInt16(data, 0);
            return version == DefaultVersion.Version;
        }

        public bool IsLatestHashVersion(string data)
        {
            var dataBytes = Convert.FromBase64String(data);
            return IsLatestHashVersion(dataBytes);
        }

        public byte[] GetRandomBytes(int length)
        {
            var data = new byte[length];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(data);
            }
            return data;
        }

        public byte[] Hash(string clearText, int iterations = DefaultIterations)
        {
            var currentVersion = DefaultVersion;

            var saltBytes = GetRandomBytes(currentVersion.SaltSize);
            var versionBytes = BitConverter.GetBytes(currentVersion.Version);
            var iterationBytes = BitConverter.GetBytes(iterations);
            var hashBytes = KeyDerivation.Pbkdf2(clearText, saltBytes, currentVersion.KeyDerivation, iterations, currentVersion.HashSize);

            var indexVersion = 0;
            var indexIteration = indexVersion + 2;
            var indexSalt = indexIteration + 4;
            var indexHash = indexSalt + currentVersion.SaltSize;

            var resultBytes = new byte[2 + 4 + currentVersion.SaltSize + currentVersion.HashSize];
            Array.Copy(versionBytes, 0, resultBytes, indexVersion, 2);
            Array.Copy(iterationBytes, 0, resultBytes, indexIteration, 4);
            Array.Copy(saltBytes, 0, resultBytes, indexSalt, currentVersion.SaltSize);
            Array.Copy(hashBytes, 0, resultBytes, indexHash, currentVersion.HashSize);
            return resultBytes;
        }

        public string HashToString(string clearText, int iterations = DefaultIterations)
        {
            var data = Hash(clearText, iterations);
            return Convert.ToBase64String(data);
        }

        public bool Verify(string clearText, byte[] data)
        {
            //Get the current version and number of iterations
            var currentVersion = _versions[BitConverter.ToInt16(data, 0)];
            var iteration = BitConverter.ToInt32(data, 2);

            //Create the byte arrays for the salt and hash
            var saltBytes = new byte[currentVersion.SaltSize];
            var hashBytes = new byte[currentVersion.HashSize];

            //Calculate the indexes of the salt and the hash
            var indexSalt = 2 + 4; // Int16 (Version) and Int32 (Iteration)
            var indexHash = indexSalt + currentVersion.SaltSize;

            //Fill the byte arrays with salt and hash
            Array.Copy(data, indexSalt, saltBytes, 0, currentVersion.SaltSize);
            Array.Copy(data, indexHash, hashBytes, 0, currentVersion.HashSize);

            //Hash the current clearText with the parameters given via the data
            var verificationHashBytes = KeyDerivation.Pbkdf2(clearText, saltBytes, currentVersion.KeyDerivation, iteration, currentVersion.HashSize);

            //Check if generated hashes are equal
            return hashBytes.SequenceEqual(verificationHashBytes);
        }

        public bool Verify(string clearText, string data)
        {
            var dataBytes = Convert.FromBase64String(data);
            return Verify(clearText, dataBytes);
        }

        public bool VerifyPassword(string normalPassword, string hashedPassword)
        {
            return Verify(normalPassword, hashedPassword);
        }
    }
}
