using System.Security.Cryptography;
using System.Text;

namespace InternshipAutomation.Persistance.Hasing;

public class Hash
{
    public static string ToHash(string password)
    {
        using (SHA1Managed sha1Managed = new SHA1Managed())
        {
            var hash = sha1Managed.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder(hash.Length * 2);
            
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}