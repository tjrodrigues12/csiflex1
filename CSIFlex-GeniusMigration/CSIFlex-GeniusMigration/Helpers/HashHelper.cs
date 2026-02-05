using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration.Helpers
{
	public class HashHelper
	{
		public const int SALT_SIZE = 32; // size in bytes
		public const int HASH_SIZE = 32; // size in bytes
		public const int ITERATIONS = 10000; // number of pbkdf2 iterations

		public static PBKDF2Hash CreatePBKDF2Hash(string input, int iterations = ITERATIONS, int saltSize = SALT_SIZE)
		{
			byte[] salt = GenerateSalt(saltSize);
			using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, iterations))
			{

				return new PBKDF2Hash
				{
					Hash = pbkdf2.GetBytes(HASH_SIZE),
					Salt = salt,
					Iterations = iterations,
				};
			}
		}

		public static byte[] GenerateSalt(int saltSize = SALT_SIZE)
		{
			using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
			{
				byte[] salt = new byte[saltSize];
				provider.GetBytes(salt);
				return salt;
			}
		}


		public static byte[] ComputePBKDF2Hash(string input, byte[] salt, int iterations = ITERATIONS, int hashSize = HASH_SIZE)
		{
			// Generate the hash
			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, iterations);
			return pbkdf2.GetBytes(hashSize);
		}

		public class PBKDF2Hash
		{
			public byte[] Salt { get; set; }

			public byte[] Hash { get; set; }

			public int Iterations { get; set; }
		}
	}
}
