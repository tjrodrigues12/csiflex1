namespace CSIFLEX.Server.Library.DataModel
{
    public class ReportingData
    {
        public string Task_Name { get; set; }

        public string TimeOfReport { get; set; }

        public string MailTo { get; set; }

        public string isDone { get; set; }

        public string isShortFileName { get; set; }

        public ReportingData(string task_name, 
                             string timeOfReport, 
                             string mailTo,
                             string isDone,
                             string isShortFileName)
        {
            this.Task_Name = task_name;
            this.TimeOfReport = timeOfReport;
            this.MailTo = mailTo;
            this.isDone = isDone;
            this.isShortFileName = isShortFileName;
        }
    }
}
