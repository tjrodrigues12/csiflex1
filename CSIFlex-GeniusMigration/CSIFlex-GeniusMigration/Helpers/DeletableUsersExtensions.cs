using CSIFlex_GeniusMigration.Entities;
using CSIFlex_GeniusMigration.ViewModel;
using System;

namespace CSIFlex_GeniusMigration.Helpers
{
	public static class DeletableUsersExtensions
	{
		public static DeletableCSIFlexUser ToCSIFlexUser(this DataGridItem @this, string basePassword, bool isToBeDeleted)
		{
			var res = HashHelper.CreatePBKDF2Hash(basePassword);
			var password = Convert.ToBase64String(res.Hash);
			var salt = Convert.ToBase64String(res.Salt);
			return new DeletableCSIFlexUser()
			{
				IsToBeDeleted = isToBeDeleted,
				User = new CSIFlexUser()
				{
					UserName = @this.GeniusUser.UserNameForCSIFlex,
					FirstName = @this.GeniusUser.FirstName,
					Name = @this.GeniusUser.Name,
					Password = password,
					Salt = salt,
					Email = @this.GeniusUser.Email,
					UserType = @this.UserRole.ToString(),
					Machines = string.Join(",", @this.Machines),
					RefId = @this.GeniusUser.Id,
					Title = @this.GeniusUser.PayGroup,
					Department = @this.GeniusUser.DepartmentCode,
					PhoneExtension = string.Join(" ", @this.GeniusUser.Phone1, @this.GeniusUser.Phone2, @this.GeniusUser.Phone3)
				}
			};
		}
	}
}
