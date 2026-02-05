using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration.Entities
{
	public class GeniusUser
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string OperationCode { get; set; }
		public string WageCode { get; set; }
		public bool DirectLabour { get; set; }
		public string DepartmentCode { get; set; }
		public bool Active { get; set; }
		public string MachineCode { get; set; }
		public string AlternateCode { get; set; }
		public string Ssn { get; set; }
		public string LanguageCode { get; set; }
		public string Email { get; set; }
		public bool Encryption { get; set; }
		public DateTime HiringDate { get; set; }
		public string TerminationDate { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
		public string Phone3 { get; set; }
		public string PayGroup { get; set; }
		public string AccessGroup { get; set; }
		public string FirstName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Position { get; set; }
		public string Note { get; set; }
		public string EmployeeType { get; set; }
		public int LanguageId { get; set; }
		public int PayFrequency { get; set; }
		public string Id { get; set; }
		public string Link { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUser { get; set; }
		public string FullSearch { get; set; }
		public int SupervisionRight { get; set; }
		public string SupervisorId { get; set; }
		public string Password { get; set; }
		public bool CanEnterExpenses { get; set; }
		public string RelatedVendorId { get; set; }
		public string[] WorkSchedules { get; set; }
		public string Machine { get; set; }

		[JsonIgnore]
		public string FullName => $"{FirstName} {Name}";

		public string UserNameForCSIFlex => $"{FirstName[0]}{Name}";
	}
}
