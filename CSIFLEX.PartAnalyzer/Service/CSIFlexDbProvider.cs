using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using CSIFLEX.PartAnalyzer.Entities;

namespace CSIFLEX.PartAnalyzer.Service
{
    public class CSIFlexDbProvider
    {
        private bool initialized;
        private SettingsProvider settingsProvider;

        public CSIFlexDbProvider(SettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        private List<RenamedMachines> MachineTableNames { get; set; }

        public async Task<bool> TestConnection(Settings settings)
        {
            try
            {
                var connectionString = CSIDbConnectionString(settings);
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

        public async Task Init(Settings settings)
        {
            if (!initialized)
            {
                using (var connection = new MySqlConnection(CSIDbConnectionString(settings)))
                {
                    var query = "SELECT table_name AS \"TableName\", original_name AS \"OriginalName\" FROM tbl_renamemachines";
                    var rows = await connection.QueryAsync<RenamedMachines>(query);
                    var machineTablesMap = rows
                        .Where(x => x.OriginalName.HasValue() || x.TableName.HasValue())
                        .ToList();
                    foreach (var item in machineTablesMap)
                    {
                        item.TableName = "tbl_" + item.TableName.ToLowerInvariant();
                    }
                    MachineTableNames = machineTablesMap;
                }
                initialized = true;
            }
        }

        private string CSIDbConnectionString(Settings settings)
        {
            return ConnectionStringBuilder(settings, "csi_database");
        }

        private string ConnectionStringBuilder(Settings settings, string db)
        {
            return $"Server={ settings.DatabaseServer};Port={settings.CSFlexDbPort};Database={db};Uid={ settings.CSIFlexUserName};Pwd={ settings.CSIFlexPassword};";
        }

        public async Task<IEnumerable<MachinePartProduction>> GetMachines(Settings settings, string productionPartNumber)
        {
            await Init(settings);
            using (var connection = new MySqlConnection(CSIDbConnectionString(settings)))
            {
                var sb = new StringBuilder();
                for (int i = 0; i < MachineTableNames.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.AppendLine(" UNION ");
                    }

                    sb.AppendLine($"SELECT *, \"{MachineTableNames[i].OriginalName}\" AS \"MachineName\" FROM {MachineTableNames[i].TableName.ToLower()}");
                    sb.AppendLine($"WHERE status LIKE \'%{productionPartNumber}%\'");
                }
                sb.AppendLine("ORDER BY Date_");
                var sql = sb.ToString();
                var results = await connection.QueryAsync<MachineDataEntry>(sql);
                return results
                    .Select(x =>
                    {
                        return new MachinePartProduction()
                        {
                            Machine = x.MachineName,
                            PartNumber = GetPartNumberFromMachineDataEntry(x)
                        };
                    })
                    .Distinct(new MachinePartProductionComparer());
            }
        }

        public async Task<MachinePartPerformance> GetLatestMachinePartPerformance(string partNumber, string machineName)
        {
            var settings = settingsProvider.GetSettings;
            await Init(settings);
            using (var connection = new MySqlConnection(CSIDbConnectionString(settings)))
            {
                var sb = new StringBuilder();
                var item = MachineTableNames
                    .Where(x => x.OriginalName == machineName)
                    .FirstOrDefault();

                var query = $@"SELECT T1.* ,""{machineName}"" AS ""MachineName""
                                    FROM {item.TableName} as T1
                                    WHERE T1.Date_ >= (SELECT MAX(pi.Date_)
                                    FROM {item.TableName} pi
                                    WHERE status like ""_PARTNO:{partNumber}%""
                                    ORDER BY Date_ DESC LIMIT 1
                                    )
                                    AND Partnumber like ""%{partNumber}%"";";

                var result = await connection.QueryAsync<MachineDataEntry>(query);
                return new MachinePartPerformance()
                {
                    MachineName = machineName,
                    PartNumber = partNumber,
                    Data = result
                };
            }
        }

        public async Task<IEnumerable<MachinePartPerformance>> GetLatestMachinePartPerformance(string partNumber)
        {
            var settings = settingsProvider.GetSettings;
            await Init(settings);
            using (var connection = new MySqlConnection(CSIDbConnectionString(settings)))
            {
                var sb = new StringBuilder();

                for (int i = 0; i < MachineTableNames.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.AppendLine(" UNION ");
                    }

                    sb.AppendLine($@"SELECT *,""{MachineTableNames[i].OriginalName}"" AS ""MachineName""  FROM {MachineTableNames[i].TableName} mainTable
                    WHERE mainTable.Date_ > (SELECT st1.Date_
	                    FROM (SELECT t1.Date_
                                FROM { MachineTableNames[i].TableName} t1
                                WHERE t1.Date_ < (SELECT Date_
				                    FROM { MachineTableNames[i].TableName}
				                    WHERE status LIKE ""%_PARTNO:{partNumber}%""
			                    	ORDER BY Date_ DESC
			                    	LIMIT 1)
                                ORDER BY Date_ DESC
                                LIMIT 1) AS st1
                            ORDER BY Date_ DESC
                            LIMIT 1) AND
                            mainTable.Date_ <(SELECT t3.Date_
                            FROM { MachineTableNames[i].TableName} t3
                            WHERE t3.Date_ > (SELECT st1.Date_
                    			FROM (SELECT t1.Date_
					                    FROM { MachineTableNames[i].TableName} t1
                                        WHERE t1.Date_ < (SELECT Date_ 
                    						FROM { MachineTableNames[i].TableName}
                    						WHERE status LIKE ""%_PARTNO:{partNumber}%""
					                    	ORDER BY Date_ DESC
                    						LIMIT 1)
                                        ORDER BY Date_ DESC
                                        LIMIT 1) AS st1
                                    ORDER BY Date_ DESC
                                    LIMIT 1)
                                    AND status LIKE ""_PARTNO%""
                                    AND STATUS NOT LIKE ""%{partNumber}%""  
                            ORDER BY Date_ ASC
                            LIMIT 1) ");
                }
                var query = sb.ToString();
                var results = (await connection.QueryAsync<MachineDataEntry>(query)).ToArray();


                return new List<MachinePartPerformance>()
                {
                    new MachinePartPerformance()
                    {
                        Data = results,
                        MachineName = results != null && results.Length > 0
                            ? results[0].MachineName
                            : string.Empty,
                        PartNumber = results != null && results.Length > 0
                            ? GetPartNumberFromMachineDataEntry(results[0])
                            :default
                    }
                }
                .Where(x => x.MachineName.HasValue() && x.PartNumber.HasValue());
            }
        }

        private Func<SearchOptions, int, DateTime, DateTime> BuildTimeSubQuery = (options, multiplier, initialDate) =>
        {
            if (options.IsIterateSearchOverDate && options.TimeWindowSearchIterations > 0)
            {
                var ts = new TimeSpan((int)options.HourWindowValue, (int)options.MinuteWindowValue, 0);
                return initialDate + (multiplier * ts);
            }
            return initialDate;
        };

        public async Task<IEnumerable<MachinePartPerformance>> GetMachinePartPerformance(  DateTime from, DateTime to, string machineName, string partNumber)
        {
            var settings = settingsProvider.GetSettings;
            await Init(settings);
            using (var connection = new MySqlConnection(CSIDbConnectionString(settings)))
            {
                var sb = new StringBuilder();
                var item = MachineTableNames
                    .Where(x => x.OriginalName == machineName)
                    .FirstOrDefault(); 

                var likeSubquery = new StringBuilder();
                var query = $@"SELECT * ,""{machineName}"" AS ""MachineName""
                            FROM {item.TableName}
                            WHERE Date_ BETWEEN ""{from.ToString("yyyy-MM-dd HH:mm:ss")}"" AND ""{to.ToString("yyyy-MM-dd HH:mm:ss")}"" AND Partnumber like ""%{partNumber}%""";

                var results = (await connection.QueryAsync<MachineDataEntry>(query)).ToArray();
                var indicies = new List<int>();
                for (int i = 0; i < results.Length; i++)
                {
                    if (results[i].status.Contains("_PARTNO:"))
                    {
                        indicies.Add(i);
                    }
                }
                var retVal = new List<MachinePartPerformance>();
                for (int i = 0; i < indicies.Count - 1; i++)
                {
                    var data = new MachineDataEntry[indicies[i + 1] - indicies[i + 1]];
                    Array.Copy(results, indicies[0], data, 0, data.Length);
                    retVal.Add(new MachinePartPerformance()
                    {
                        MachineName = machineName,
                        PartNumber = partNumber,
                        Data = data
                    });
                }
                return retVal;
            }
        }

        private string BuildLikePartNumberSubquery(SearchOptions options, string initialPartNumber)
        {
            var sb = new StringBuilder();
            sb.Append($@" Partnumber like ""%{initialPartNumber}%"" ");
            if (options.IsIteratingOverPartName && options.IterativeSearchPartNameValue > 0)
            {
                var startString = initialPartNumber.Substring(0, initialPartNumber.Length);
                for (int i = 1; startString.Length > 4; i++)
                {
                    startString = initialPartNumber.Substring(0, initialPartNumber.Length - i);
                    var s = $@" OR Partnumber like ""%{startString}%"" ";
                    sb.Append(s);
                }
            }

            if (options.IsSplittingHyphens)
            {
                var splitPartNumber = initialPartNumber.Split('-');
                if (splitPartNumber.Length > 1)
                {
                    for (int i = splitPartNumber.Length - 1; i >= 0; i--)
                    {
                        var concatedWithoutEnd = string.Empty;
                        if (i == 0)
                        {
                            concatedWithoutEnd = $@""" OR Partnumber like ""%{splitPartNumber[0]}%"" ";
                        }
                        else
                        {
                            concatedWithoutEnd = $@""" OR Partnumber like ""%{string.Join('-', splitPartNumber.SubArray(0, i))}%"" ";
                        }
                        sb.Append(concatedWithoutEnd);
                    }
                }
            }

            return sb.ToString();
        }

        public async Task<IEnumerable<MachinePartPerformance>> GetMachinePartPerformance(SearchOptions options, DateTime from, DateTime to, string partNumber)
        {
            var settings = settingsProvider.GetSettings;
            await Init(settings);
            using (var connection = new MySqlConnection(CSIDbConnectionString(settings)))
            {
                var sb = new StringBuilder();
                var fromDate = BuildTimeSubQuery(options, -1, from);
                var toDate = BuildTimeSubQuery(options, 1, to);
                var partNumberSubQuery = BuildLikePartNumberSubquery(options, partNumber);
                for (int i = 0; i < MachineTableNames.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.AppendLine(" UNION ");
                    }
                    sb.AppendLine($"SELECT *, \"{MachineTableNames[i].OriginalName}\" AS \"MachineName\" FROM  {MachineTableNames[i].TableName.ToLower()}");
                    sb.AppendLine($@" WHERE Date_ > ""{fromDate.ToString("yyyy-MM-dd HH:mm:ss")}"" AND Date_ < ""{toDate.ToString("yyyy-MM-dd HH:mm:ss")}""");
                    sb.AppendLine($@"AND  ({partNumberSubQuery})");
                }
                sb.AppendLine("ORDER BY Date_ asc");

                var query = sb.ToString();
                var groups = (await connection.QueryAsync<MachineDataEntry>(query))
                    .GroupBy(x => x.MachineName)
                    .SelectMany(x => x)
                    .ChunkOn(x => x.status.StartsWith("_PARTNO:"))
                    .Select(x =>
                    {
                        if (x == null && x.Count() == 0)
                        {
                            return new MachinePartPerformance();
                        }
                        else
                        {
                            var part = new MachinePartPerformance()
                            {
                                MachineName = x[0].MachineName,
                                PartNumber = GetPartNumberFromMachineDataEntry(x[0]),
                                Data = x
                            };

                            part.SetTimeFormat(Constants.HoursMinSeconds);

                            return part;
                        }
                    });

                return groups;
            }
        }

        private string GetPartNumberFromMachineDataEntry(MachineDataEntry entry)
        {
            if (entry.Partnumber.HasValue())
            {
                var split = entry.Partnumber.Split(';');
                return split != null && split.Length > 0
                    ? split[0]
                    : string.Empty;
            }
            return string.Empty;
        }

    }
}
