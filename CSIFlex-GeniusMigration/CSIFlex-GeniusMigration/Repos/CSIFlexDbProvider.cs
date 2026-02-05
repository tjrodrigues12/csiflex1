using CSIFlex_GeniusMigration.Entities;
using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_GeniusMigration
{
	public class CSIFlexDbProvider
	{

		public CSIFlexDbProvider()
		{
		}

		public async Task<bool> TestConnection(Settings settings)
		{
			try
			{
				var connectionString = UserDbConnectionString(settings);
				var connection = new MySqlConnection(connectionString);
				await connection.OpenAsync();
				connection.Close();
				connection.Dispose();
				return true;
			}
			catch (Exception e)
			{
				var s = e.Message;
				return false;
			}

		}

		private string UserDbConnectionString(Settings settings)
		{
			return ConnectionStringBuilder(settings, "csi_auth");
		}

		private string CSIDbConnectionString(Settings settings)
		{
			return ConnectionStringBuilder(settings, "csi_database");
		}

		private string ConnectionStringBuilder(Settings settings, string table)
		{
			return $"Server={ settings.DatabaseServer};Port={settings.CSFlexDbPort};Database={table};Uid={ SecureStoreApi.TryDecrypt(settings.CSIFlexUserName)};Pwd={ SecureStoreApi.TryDecrypt(settings.CSIFlexPassword)};";
		}

		public async Task<IEnumerable<CSIFlexMachine>> GetAllMachines(Settings settings)
		{
			var sql = "SELECT groups AS \"Group\", machines AS \"Machine\"FROM tbl_groups";
			using (var connection = new MySqlConnection(CSIDbConnectionString(settings)))
			{
				return await connection.QueryAsync<CSIFlexMachine>(sql);
			}
		}

		public async Task<IEnumerable<CSIFlexUser>> GetAllUsers(Settings settings)
		{
			//username_, Name_, firstname_, password_, salt_, email_, usertype, machines, refId, title, dept, phoneext

			var sql = "SELECT username_ as \"UserName\", firstname_ as \"FirstName\", Name_ as \"Name\",password_ as \"Password\", usertype as \"UserType\",salt_ as \"Salt\", email_ as \"Email\",machines as \"Machines\",refId as \"RefId\",title as \"Title\" ,dept as \"Department\" ,phoneext as \"PhoneExtension\"  FROM users";
			using (var connection = new MySqlConnection(UserDbConnectionString(settings)))
			{
				return await connection.QueryAsync<CSIFlexUser>(sql);
			}
		}

		public async Task<IEnumerable<MachineGroup>> GetAllGroups(Settings settings)
		{
			var machines = await GetAllMachines(settings);
			return machines
				.Where(x => x.Machine.HasValue())
				.GroupBy(x => x.Group, x => x.Machine)
				.ToDictionary(x => x.Key, x => x.ToArray())
				.Select(x => new MachineGroup()
				{
					Group = x.Key,
					Machines = x.Value
				});
		}

		public async Task<int> UpdateEmployees(Settings settings, IEnumerable<DeletableCSIFlexUser> users)
		{
			var usersToRemove = users
				.Where(x => x.IsToBeDeleted)
				.Select(x => x.User);
			var usersToInsert = users
				.Where(x => !x.IsToBeDeleted)
				.Select(x => x.User);

			var usersWithTypes = usersToInsert
				.Where(x => x.UserType != UserRole.None.ToString());  

			var replaceWithTypesQuery = "REPLACE INTO users (username_,Name_,firstname_,password_,salt_, email_,usertype,machines,refId,title,dept, phoneext) Values (@UserName, @Name, @FirstName, @Password, @Salt, @Email, @UserType, @Machines, @RefId, @Title, @Department,@PhoneExtension);";
 			string deleteUsersQuery = "DELETE FROM users WHERE username_ = @UserName";

			int rows = 0;
			using (var connection = new MySqlConnection(UserDbConnectionString(settings)))
			{
				if (usersWithTypes.Count() > 0)
				{
					var affectedRows = await connection.ExecuteAsync(replaceWithTypesQuery, usersWithTypes);
					rows += affectedRows;
				}
				 
				if(usersToRemove.Count()> 0)
				{
					var affectedRows = await connection.ExecuteAsync(deleteUsersQuery, usersToRemove);
				}
			}
			return rows;
		}	 

	}
}
