using CSIFLEX.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class MachinePartPerformance : ViewModelBase
    {
        private IEnumerable<MachineDataEntry> data;
        private string cycleonstring;
        private string cycleoffstring;
        private string othertimestring;
        private string setuptimestring;
        private string totalTimestring;


        public string MachineName { get; set; }

        public string PartNumber { get; set; }

        public DateTime ProductionStartTime { get; set; }

        public DateTime ProductionEndTime => ProductionStartTime.AddSeconds(TotalTimeInSeconds);

        [CsvHelper.Configuration.Attributes.Ignore]
        public IEnumerable<MachineDataEntry> Data
        {
            get => data;
            set
            {
                data = value;
                SetTime(data);
            }
        }

        [CsvHelper.Configuration.Attributes.Ignore]
        public long TotalTimeInSeconds => TotalCycleOnInSeconds + TotalCycleOffInSeconds + TotalSetupInSeconds + TotalOtherInSeconds;
        [CsvHelper.Configuration.Attributes.Name("Total time")]
        public string TotalTime
        {
            get => totalTimestring;
            set
            {
                totalTimestring = value;
                RaisePropertyChanged(nameof(TotalTime));
            }
        }

        [CsvHelper.Configuration.Attributes.Name("Total cycle on time")]
        public string TotalCycleOntime
        {
            get => cycleonstring;
            set
            {
                cycleonstring = value;
                RaisePropertyChanged(nameof(TotalCycleOntime));
            }
        }

        [CsvHelper.Configuration.Attributes.Name("Total cycle off time")]
        public string TotalCycleOfftime
        {
            get => cycleoffstring;
            set
            {
                cycleoffstring = value;
                RaisePropertyChanged(nameof(TotalCycleOfftime));
            }
        }

        [CsvHelper.Configuration.Attributes.Name("Total setup time")]
        public string TotalSetupTime
        {
            get => setuptimestring;
            set
            {
                setuptimestring = value;
                RaisePropertyChanged(TotalSetupTime);
            }
        }

        [CsvHelper.Configuration.Attributes.Name("Other time")]
        public string TotalOther
        {
            get => othertimestring;
            set
            {
                othertimestring = value;
                RaisePropertyChanged(TotalOther);
            }
        }

        [CsvHelper.Configuration.Attributes.Ignore]
        public long TotalCycleOnInSeconds { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public long TotalCycleOffInSeconds { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public long TotalOtherInSeconds { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public long TotalSetupInSeconds { get; set; }

        private void SetTime(IEnumerable<MachineDataEntry> data)
        {
            TotalCycleOnInSeconds = SumTime(x => x.status.Equals("CYCLE OFF"), data);
            TotalCycleOffInSeconds = SumTime(x => x.status.Equals("CYCLE ON"), data);
            TotalSetupInSeconds = SumTime(x => x.status.Equals("SETUP"), data);
            TotalOtherInSeconds = SumTime(x => !x.status.Equals("SETUP") && !x.status.Equals("CYCLE ON") && !x.status.Equals("CYCLE OFF"), data);
            if (data.Any())
            {

                var res = data.ToArray();
                var first = res[0];
                var dtStart = DateTime.Now;
                DateTime.TryParse(first.Date_, out dtStart);
                ProductionStartTime = dtStart;
            }
            //SetTimeFormat(timeFormat);
        }

        private int SumTime(Func<MachineDataEntry, bool> predicate, IEnumerable<MachineDataEntry> part)
        {
            return part
                .Where(predicate)
                .Select(x => x.cycletime)
                .Sum();
        }

        public void SetTimeFormat(string timeFormat)
        {
            switch (timeFormat)
            {
                case Constants.Seconds:
                    TotalTime = TotalTimeInSeconds.ToString();
                    TotalCycleOntime = TotalCycleOnInSeconds.ToString();
                    TotalCycleOfftime = TotalCycleOffInSeconds.ToString();
                    TotalOther = TotalOtherInSeconds.ToString();
                    TotalSetupTime = TotalSetupInSeconds.ToString();
                    break;
                case Constants.MinSeconds:
                    TotalCycleOntime = TotalCycleOnInSeconds.FromSecondsToMMSS();
                    TotalCycleOfftime = TotalCycleOffInSeconds.FromSecondsToMMSS();
                    TotalOther = TotalOtherInSeconds.FromSecondsToMMSS();
                    TotalSetupTime = TotalSetupInSeconds.FromSecondsToMMSS();
                    TotalTime = TotalTimeInSeconds.FromSecondsToMMSS();
                    break;
                case Constants.HoursMinSeconds:

                    TotalCycleOntime = TotalCycleOnInSeconds.FromSecondsToHHMMSS();
                    TotalCycleOfftime = TotalCycleOffInSeconds.FromSecondsToHHMMSS();
                    TotalOther = TotalOtherInSeconds.FromSecondsToHHMMSS();
                    TotalSetupTime = TotalSetupInSeconds.FromSecondsToHHMMSS();
                    TotalTime = TotalTimeInSeconds.FromSecondsToHHMMSS();
                    break;
            }
        }
    }
}
