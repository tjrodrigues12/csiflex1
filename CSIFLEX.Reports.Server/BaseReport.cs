using Microsoft.Reporting.WinForms;
using System;
using System.Drawing.Printing;
using System.IO;

namespace CSIFLEX.Reports.Server
{
    public class BaseReport
    {

        private string tempFile = Path.Combine(Path.GetTempPath(), "CSIFLEX_TempReport.pdf");

        protected PageSettings MarginPageSettings()
        {
            PageSettings ps = new PageSettings();

            ps.Margins.Top = 1;
            ps.Margins.Right = 1;
            ps.Margins.Bottom = 1;
            ps.Margins.Left = 1;

            return ps;
        }

        protected string SaveReport(ReportViewer reportViewer, string reportName, string reportPeriod, bool shortName, string outputReportPath = "")
        {
            string fileFullPath = "";

            if (shortName)
            {
                fileFullPath = $"{reportName}_{reportPeriod}_{DateTime.Now.ToString("MMMddyy")}.pdf";
            } else
            {
                fileFullPath = $"{reportName}_{reportPeriod}_{DateTime.Now.ToString("MMMddyyyy hh-mm-ss")}.pdf";
            }
            fileFullPath = Path.Combine(outputReportPath, fileFullPath);

            var bites = reportViewer.LocalReport.Render("PDF");

            try
            {
                FileStream stream = new FileStream(fileFullPath, FileMode.Create);
                stream.Write(bites, 0, bites.Length);
                stream.Close();
                return fileFullPath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}

