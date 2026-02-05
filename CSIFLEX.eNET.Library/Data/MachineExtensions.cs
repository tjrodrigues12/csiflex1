using System;
using System.IO;
namespace CSIFLEX.eNET.Library.Data
{
    public static class MachineExtensions
    {
        public static CsvMachineModel ToCsvMachineModel(this eNETMachine @this)
        {
            var pos = "";
            var file = "";
            var enetPos = @this.EnetPos.Split(','); 
            pos = $"{ (int.Parse(enetPos[0]) - 1).ToString("x") }{ int.Parse(enetPos[1]) - 1 }";
            file = Path.Combine(eNETServer.EnetTempFolder, $"MonitorData{pos}.SYS");

            var lines = File.ReadAllLines(file);
            var fields = lines[lines.Length - 1].Split(',');
            DateTime date = DateTime.ParseExact($"{ fields[0] } { fields[1] }", "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan diff = DateTime.Now - date;

            return new CsvMachineModel()
            {
                MachineName = @this.MachineName,
                MachineStatus = fields[2],
                Date = date,
                StartTime = date,
                ElapsedTime = diff.Seconds
            };
        }
    }
}
