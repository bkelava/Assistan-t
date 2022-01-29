using Accountant_s_Assistant.Database.Tables;
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Accountant_s_Assistant.App
{
    class PDFCreator
    {
        private string path;
        public PDFCreator(string path)
        {
            this.path = path;
        }

        private string SplitToLines(string stringToSplit, int maximumLineLength)
        {
            return Regex.Replace(stringToSplit, @"(.{1," + maximumLineLength + @"})(?:\s|$)", "$1\n");
        }

        private void killExcelProcesses()
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Equals("Microsoft Excel"))
                {
                    clsProcess.Kill();
                    break;
                }
            }
        }

        public string generateContractOnDefinitiveTime(List<KeyValuePair<string, string>> list, Employer employer, Employee employee)
        {
            killExcelProcesses();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "../../../Resources/template.xlsx");
            string pathToTempFile = Path.Combine(this.path, "tempfile.xlsx");
            File.WriteAllBytes(pathToTempFile, File.ReadAllBytes(path));
            
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(pathToTempFile);
            Worksheet excelSheet = (Worksheet)wb.ActiveSheet;

            //Read the first cell
            excelSheet.Cells["1", "C"].Value = employer.Name + " ," + employer.Address.Street + " " + employer.Address.PostalCode + " " + employer.Address.City + ", OIB: " + employer.VAT;
            excelSheet.Cells["2", "C"].Value = employer.Director;
            excelSheet.Cells["2", "H"].Value = employee.Name + ", OIB: " + employee.VAT;
            excelSheet.Cells["3", "E"].Value = list.Find(x => x.Key == "contractDate").Value;
            excelSheet.Cells["8", "F"].Value = list.Find(x => x.Key == "endOfEmployment").Value;
            excelSheet.Cells["9", "A"].Value = list.Find(x => x.Key == "jobDescription").Value;
            excelSheet.Cells["10", "F"].Value = list.Find(x => x.Key == "trialWorkDuration").Value;
            excelSheet.Cells["11", "C"].Value = list.Find(x => x.Key == "employeeWorkPlace").Value;
            excelSheet.Cells["14", "C"].Value = list.Find(x => x.Key == "startOfEmployment").Value;
            excelSheet.Cells["14", "E"].Value = list.Find(x => x.Key == "startOfEmploymentDescription").Value;
            excelSheet.Cells["17", "H"].Value = list.Find(x => x.Key == "sallary").Value;
            excelSheet.Cells["19", "I"].Value = list.Find(x => x.Key == "stimulation").Value;
            excelSheet.Cells["21", "H"].Value = list.Find(x => x.Key == "sallaryFitA").Value;
            excelSheet.Cells["22", "H"].Value = list.Find(x => x.Key == "sallaryFitB").Value;
            excelSheet.Cells["23", "H"].Value = list.Find(x => x.Key == "sallaryFitC").Value;
            excelSheet.Cells["24", "H"].Value = list.Find(x => x.Key == "sallaryFitD").Value;
            excelSheet.Cells["25", "H"].Value = list.Find(x => x.Key == "sallaryFitE").Value;
            excelSheet.Cells["26", "H"].Value = list.Find(x => x.Key == "sallaryFitF").Value;
            excelSheet.Cells["32", "C"].Value = list.Find(x => x.Key == "workTimeHalfOrFull").Value;
            excelSheet.Cells["32", "F"].Value = list.Find(x => x.Key == "workHoursPerWeek").Value;

            string workTime = list.Find(x => x.Key == "workTime").Value;
            string workTimeStartA = list.Find(x => x.Key == "workTimeStartA").Value;
            string workTimeEndB = list.Find(x => x.Key == "workTimeEndB").Value;
            if (workTime.Equals("klizno"))
            {
                string workTimeEndA = list.Find(x => x.Key == "workTimeEndA").Value;
                string workTimeStartB = list.Find(x => x.Key == "workTimeStartB").Value;
                excelSheet.Cells["33", "F"].Value = "od " + workTimeStartA + " do " + workTimeEndA + ", a zavšrava od " + workTimeStartA + " do " + workTimeEndB + "."; 
            }
            else if (workTime.Equals("dvokratno"))
            {
                string workTimeEndA = list.Find(x => x.Key == "workTimeEndA").Value;
                string workTimeStartB = list.Find(x => x.Key == "workTimeStartB").Value;
                excelSheet.Cells["33", "F"].Value = "od " + workTimeStartA + " i završava u" + workTimeEndA + ", te počinje u" + workTimeStartA + " i zavšrava do " + workTimeEndB + ".";
            }
            else
            {
                excelSheet.Cells["33", "F"].Value = "od " + workTimeStartA + " i završava u " + workTimeEndB + ".";

            }
            excelSheet.Cells["35", "D"].Value = list.Find(x => x.Key == "weeklyTimeOff").Value;
            excelSheet.Cells["36", "E"].Value = list.Find(x => x.Key == "vacation").Value;
            excelSheet.Cells["36", "F"].Value = list.Find(x => x.Key == "vacationDescription").Value;
            excelSheet.Cells["43", "C"].Value = list.Find(x => x.Key == "contractCancelation").Value;
            excelSheet.Cells["45", "F"].Value = list.Find(x => x.Key == "noticePeriodA").Value;
            excelSheet.Cells["46", "C"].Value = list.Find(x => x.Key == "noticePeriodB").Value;

            string rightsAndObligations = list.Find(x => x.Key == "stimulation").Value;
            rightsAndObligations = SplitToLines(rightsAndObligations, 128);
            excelSheet.Cells["49", "A"].Value = rightsAndObligations;

            excelSheet.Cells["53", "G"].Value = list.Find(x => x.Key == "competentCourt").Value;
            excelSheet.Cells["54", "D"].Value = list.Find(x => x.Key == "contractEntry").Value;

            excelSheet.Cells["58", "I"].Value = employer.Director;
            excelSheet.Cells["58", "B"].Value = employee.Name;

            path = Path.Combine(this.path, "[ASISTENT] " + employee.Name + " " + employer.Name + ".pdf");
            wb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, path);

            //closing app
            wb.Saved = true;
            wb.Save();
            wb.Close();
            excel.Quit();
            File.Delete(pathToTempFile);

            killExcelProcesses();
            return path;
        }
    }
}
