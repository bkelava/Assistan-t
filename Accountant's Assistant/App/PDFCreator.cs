using Accountant_s_Assistant.Database.Tables;
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Accountant_s_Assistant.App
{
    class PDFCreator
    {
        private string path;
        public PDFCreator(string path)
        {
            this.path = path;
        }

        private string splitToLines(string stringToSplit, int maximumLineLength)
        {
            if (stringToSplit.Length > maximumLineLength)
                stringToSplit = stringToSplit.Insert(128, Environment.NewLine);

            return stringToSplit;
        }

        public string generateContractOnDefinitiveTime(List<KeyValuePair<string, string>> list, Employer employer, Employee employee)
        {
            ApplicationManager.killExcelProcesses();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "../../../Resources/template.xlsx");
            string pathToTempFile = Path.Combine(this.path, "tempfile.xlsx");
            try
            {
                File.WriteAllBytes(pathToTempFile, File.ReadAllBytes(path));
            }
            catch(Exception e)
            {
                EventLog.WriteEntry(e.ToString(), "ERROR");
            }
            
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

            string rightsAndObligations = list.Find(x => x.Key == "rightsAndObligations").Value;
            rightsAndObligations = splitToLines(rightsAndObligations, 128);
            excelSheet.Cells["49", "A"].Value = rightsAndObligations;
            excelSheet.Cells["50", "A"].Value = "";

            excelSheet.Cells["53", "G"].Value = list.Find(x => x.Key == "competentCourt").Value;
            excelSheet.Cells["54", "D"].Value = list.Find(x => x.Key == "contractEntry").Value;
            excelSheet.Cells["54", "F"].Value = list.Find(x => x.Key == "contractEntryComment").Value;

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

            ApplicationManager.killExcelProcesses();
            return path;
        }
        
        private string parseMonthNumber(string number)
        {
            if (number.Equals("1"))
            {
                return "siječnja";
            }
            else if (number.Equals("2"))
            {
                return "veljače";
            }
            else if (number.Equals("3"))
            {
                return "ožujka";
            }
            else if (number.Equals("4"))
            {
                return "travnja";
            }
            else if (number.Equals("5"))
            {
                return "svibnja";
            }
            else if (number.Equals("6"))
            {
                return "lipnja";
            }
            else if (number.Equals("7"))
            {
                return "srpnja";
            }
            else if (number.Equals("8"))
            {
                return "kolovoza";
            }
            else if (number.Equals("9"))
            {
                return "srpnja";
            }
            else if (number.Equals("10"))
            {
                return "listopada";
            }
            else if (number.Equals("11"))
            {
                return "studenog";
            }
            else
            {
                return "prosinca";
            }
        }
        private string createCroatianDate()
        {
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();

            string croatianMonthName = parseMonthNumber(month);

            string croatianDate = day + ". " + croatianMonthName + " " + year + ".";
            return croatianDate;
        }

        private string createDecimalString(double number)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ",";
            nfi.NumberGroupSeparator = ".";
            var value = number.ToString("N2", nfi);
            return value;
        }

        public int generateGfiReport1(string gfiPodPath)
        {
            ApplicationManager.killExcelProcesses();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "../../../Resources/template_odluka.xlsx");
            string pathToTempFile = Path.Combine(this.path, "tempfile.xlsx");
            try
            {
                File.WriteAllBytes(pathToTempFile, File.ReadAllBytes(path));
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }

            Application excelToWrite = new Application();
            Workbook wbToWrite = excelToWrite.Workbooks.Open(pathToTempFile);
            Worksheet excelSheetToWrite = (Worksheet)wbToWrite.ActiveSheet;

            Application excelToRead = new Application();
            Workbook wbToRead = excelToRead.Workbooks.Open(gfiPodPath);
            Worksheet excelSheetToRead = null;

            bool check = false;
            foreach(Worksheet sheet in wbToRead.Sheets)
            {
                if (sheet.Name == "RefStr")
                {
                    check = true;
                }
            }
            if (check)
            {
                excelSheetToRead = (Worksheet)wbToRead.Worksheets["RefStr"];
            }
            else
            {
                wbToWrite.Saved = true;
                wbToWrite.Save();
                excelToWrite.Quit();

                excelToRead.Quit();
                ApplicationManager.killExcelProcesses();
                Thread.Sleep(100); //wait 1 second to kill all processes
                File.Delete(pathToTempFile);
                return ErrorCodes.Error;
            }


            string company = excelSheetToRead.Cells["29", "C"].Value;
            string city = excelSheetToRead.Cells["31", "F"].Value;
            string address = excelSheetToRead.Cells["33", "C"].Value;
            string idNumber = excelSheetToRead.Cells["27", "C"].Value;
            string currentDate = createCroatianDate();

            string reportInformation = company + " iz mjesta " + city + ", ul. " + address + ", OIB: " + idNumber + ",";
            excelSheetToWrite.Cells["3", "A"].Value = reportInformation;

            reportInformation = "donijela je " + currentDate + " ovu";
            excelSheetToWrite.Cells["4", "A"].Value = reportInformation;

            excelSheetToWrite.Cells["32", "F"].Value = excelSheetToRead.Cells["75", "A"].Value;

            excelSheetToRead = (Worksheet)wbToRead.Worksheets["RDG"];

            double value1 = 0.00;
            double value2 = 0.00;
            double value3 = 0.00;

            value1 = excelSheetToRead.Cells["67", "J"].Value;
            value2 = excelSheetToRead.Cells["68", "J"].Value;

            string stringValue1 = createDecimalString(value1);
            string stringValue2 = createDecimalString(value2);

            reportInformation = "oporezivanja u svoti od " + stringValue1 + "kn (odnosno gubitaka u svoti od " + stringValue2 + " kn).";
            excelSheetToWrite.Cells["22", "A"].Value = reportInformation;

            excelSheetToRead = (Worksheet)wbToRead.Worksheets["Bilanca"];

            value3 = excelSheetToRead.Cells["73", "J"].Value;
            string stringValue3 = createDecimalString(value3);

            string curranteDate = createCroatianDate();

            reportInformation = "Bilanca na dan " + curranteDate + " iskazuje zbroj aktive odnosno pasive u svoti od " + stringValue3 + " kn.";
            excelSheetToWrite.Cells["24", "A"].Value = reportInformation;

            path = Path.Combine(this.path, "[ASISTENT] " + company + " ODLUKA O UTVR. FIN. IZVJEŠĆA" + ".pdf");
            wbToWrite.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, path);

            //closing app
            wbToWrite.Saved = true;
            wbToWrite.Save();
            //wbToWrite.Save//Save();
            wbToWrite.Close();
            excelToWrite.Quit();

            excelToRead.Quit();
            File.Delete(pathToTempFile);

            ApplicationManager.killExcelProcesses();

            return ErrorCodes.NoError;
        }
        public int generateGfiPodReport(string gfiPodPath)
        {
            int report1ErrorCode = generateGfiReport1(gfiPodPath);
            if (report1ErrorCode == ErrorCodes.NoError)
            {
                return ErrorCodes.NoError;
            }
            else
            {
                return ErrorCodes.Error;
            }
        }
    }
}
