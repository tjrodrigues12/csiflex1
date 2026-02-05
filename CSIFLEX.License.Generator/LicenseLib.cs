using CSIFLEX.License.Data;
using CSIFLEX.License.Library;

namespace CSIFLEX.License.Generator
{
    public class LicenseLib : LicenseLibrary
    {
        public string GenerateHash(LicenseBase license)
        {
            return this.GenerateLicenseHash(license);
        }
    }
}
