using System.Collections.Generic;

namespace CSIFLEX.Server.Library.DataModel
{
    public class HtmlAvailabilityReportData
    {
        public HtmlAvailabilityReportData()
        {
            ColorsMap = new Dictionary<string, string>();
            DayData = new List<PieData>();
            Reasons = new List<UnavailableReason>();
            PastChartData = new List<HistoryColumnData>();
            Shift1PieData = new List<PieData>();
            Shift1TimelineData = new List<ShiftTimelineData>();
            Shift2PieData = new List<PieData>();
            Shift2TimelineData = new List<ShiftTimelineData>();
            Shift3PieData = new List<PieData>();
            Shift3TimelineData = new List<ShiftTimelineData>();
        }

        public string MachineName { get; set; }
        public string ReportType { get; set; }
        public string ReportDate { get; set; }
        public string ReportTitle { get; set; }
        public string ReportSubTitle { get; set; }
        public Dictionary<string, string> ColorsMap { get; set; }
        public ICollection<PieData> DayData { get; set; }
        public ICollection<UnavailableReason> Reasons { get; set; }
        public ICollection<HistoryColumnData> PastChartData { get; set; }
        public ICollection<PieData> Shift1PieData { get; set; }
        public ICollection<ShiftTimelineData> Shift1TimelineData { get; set; }
        public ICollection<PieData> Shift2PieData { get; set; }
        public ICollection<ShiftTimelineData> Shift2TimelineData { get; set; }
        public ICollection<PieData> Shift3PieData { get; set; }
        public ICollection<ShiftTimelineData> Shift3TimelineData { get; set; }
    }
    public class UnavailableReason
    {
        public string Event { get; set; }
        public string Time { get; set; }
        public string Percent { get; set; }
    }
    public class HistoryColumnData
    {
        public string Date { get; set; }
        public int CycleOnTime { get; set; }
        public int CycleOffTime { get; set; }
        public int SetupTime { get; set; }
        public int OthersTime { get; set; }
    }
    public class PieData
    {
        public string Event { get; set; }
        public int CycleTime { get; set; }
        public string Time { get; set; }
        public string Percent { get; set; }
    }
    public class ShiftTimelineData
    {
        public string Group { get; set; }
        public string Event { get; set; }
        public string Color { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
    }
}
