using Accountant_s_Assistant.Database.Tables;
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;

namespace Accountant_s_Assistant.App
{
    class PDFCreator
    {
        private PDFCreator()
        {
            //empty
        }

        private static string createCroatianDate()
        {
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();

            string croatianMonthName = parseMonthNumber(month);

            string croatianDate = day + ". " + croatianMonthName + " " + year + ".";
            return croatianDate;
        }

        private static decimal castDecimalFromString(string number)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ",";
            nfi.NumberGroupSeparator = ".";
            var value = Convert.ToDecimal(number, nfi);
            return value;
        }

        private static string createDecimalString(double number)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ",";
            nfi.NumberGroupSeparator = ".";
            var value = number.ToString("N2", nfi);
            return value;
        }

        private static string getNKDbyCode(string code)
        {
            List<KeyValuePair<string, string>> nkd = initNKD();
            string returnValue = "";
            returnValue = nkd.Find(x => x.Key == code).Value;
            return returnValue;
        }

        private static List<KeyValuePair<string, string>> initGfiInformation(string path)
        {
            ApplicationManager.killExcelProcesses();

            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(path);
            Worksheet sheet = null;

            if (checkIfCorrectFileIsLoaded(wb))
            {
                sheet = (Worksheet)wb.Worksheets["RefStr"];

                List<KeyValuePair<string, string>> gfiInformation = new List<KeyValuePair<string, string>>();

                gfiInformation.Add(new KeyValuePair<string, string>("company", sheet.Cells["29", "C"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("postal", sheet.Cells["31", "C"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("city", sheet.Cells["31", "F"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("address", sheet.Cells["33", "C"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("idNumber", sheet.Cells["27", "C"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("currentDate", createCroatianDate()));
                gfiInformation.Add(new KeyValuePair<string, string>("mb", sheet.Cells["27", "H"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("mbs", sheet.Cells["27", "M"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("localCenter", sheet.Cells["39", "D"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("director", sheet.Cells["75", "A"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("county", sheet.Cells["39", "K"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("director", sheet.Cells["75", "A"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("companyOwnership", sheet.Cells["52", "D"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("companyType", sheet.Cells["50", "D"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("domesticCapital", sheet.Cells["54", "C"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("foreignCapital", sheet.Cells["54", "F"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("companyActivites", sheet.Cells["42", "C"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("companyAutonomy", sheet.Cells["44", "D"].Value));
                gfiInformation.Add(new KeyValuePair<string, string>("numberOfEmployees1", sheet.Cells["56", "C"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("numberOfEmployees2", sheet.Cells["56", "F"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("companyOperatingTime1", sheet.Cells["60", "C"].Value.ToString()));
                gfiInformation.Add(new KeyValuePair<string, string>("companyOperatingTime2", sheet.Cells["60", "F"].Value.ToString()));
                //gfiInformation.Add(new KeyValuePair<string, string>("", ));

                sheet = (Worksheet)wb.Worksheets["RDG"];

                gfiInformation.Add(new KeyValuePair<string, string>("salesIncome", createDecimalString(sheet.Cells["10", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("salesFromOwnProducts", createDecimalString(sheet.Cells["11", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("otherIncome", createDecimalString(sheet.Cells["13", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("exchangeRateIncome", createDecimalString(sheet.Cells["45", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("interestIncome", createDecimalString(sheet.Cells["44", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("stockIncome", createDecimalString(sheet.Cells["39", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("loanIncome", createDecimalString(sheet.Cells["46", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("otherFinancialIncome", createDecimalString(sheet.Cells["47", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("totalExpensesThisYear", createDecimalString(sheet.Cells["61", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("totalExpensesLastYear", createDecimalString(sheet.Cells["61", "I"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("gain", createDecimalString(sheet.Cells["67", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("loss", createDecimalString(sheet.Cells["68", "J"].Value)));
                gfiInformation.Add(new KeyValuePair<string, string>("gain", createDecimalString(sheet.Cells["67", "J"].Value)));

                sheet = (Worksheet)wb.Worksheets["Bilanca"];

                gfiInformation.Add(new KeyValuePair<string, string>("totalAssets", createDecimalString(sheet.Cells["73", "J"].Value)));

                excel.Quit();

                ApplicationManager.killExcelProcesses();
                return gfiInformation;
            }
            else
            {
                excel.Quit();
                ApplicationManager.killExcelProcesses();
                return null;
            }
        }

        private static List<KeyValuePair<string, string>> initNKD()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "../../../Resources/NKD.csv");
            var lines = File.ReadAllLines(path);

            List<KeyValuePair<string, string>> nkd = new List<KeyValuePair<string, string>>();
            foreach (var line in lines)
            {
                var lineSplit = line.Split(";");
                if (lineSplit.Length > 1)
                {
                    nkd.Add(new KeyValuePair<string, string>(lineSplit[0], lineSplit[1]));
                }
            }
            return nkd;
        }

        private static string parseMonthNumber(string number)
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

        private static string splitToLines(string stringToSplit, int maximumLineLength)
        {
            if (stringToSplit.Length > maximumLineLength)
                stringToSplit = stringToSplit.Insert(128, Environment.NewLine);

            return stringToSplit;
        }

        public static void generateContractOnDefinitiveTime(List<KeyValuePair<string, string>> list, Employer employer, Employee employee)
        {
            /*ApplicationManager.killExcelProcesses();

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

            ApplicationManager.killExcelProcesses();*/
        }

        private static void createFile(string template, string tempfile)
        {
            try
            {
                File.WriteAllBytes(tempfile, File.ReadAllBytes(template));
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        private static bool checkIfCorrectFileIsLoaded(Workbook wb)
        {
            bool check = false;
            foreach (Worksheet sheet in wb.Sheets)
            {
                if (sheet.Name == "RefStr")
                {
                    check = true;
                }
            }
            return check;
        }

        private static void removeFile(string path)
        {
            File.Delete(path);
        }

        private static string findValueByKey(List<KeyValuePair<string, string>> list, string key)
        {
            string value = list.Find(x => x.Key == key).Value;
            return value;
        }

        public static int generateGfiReport1(string gfiPodPath)
        {
            ApplicationManager.killExcelProcesses();
            List<KeyValuePair<string, string>> gfiInformation = initGfiInformation(gfiPodPath);

            string tempfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "tempfile.xlsx");
            string template = Path.Combine(Directory.GetCurrentDirectory(), "../../../Resources/template_odluka.xlsx");

            createFile(template, tempfile);

            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(tempfile);
            Worksheet sheet = (Worksheet)wb.ActiveSheet;

            string reportInformation = findValueByKey(gfiInformation, "company") + " iz mjesta " + findValueByKey(gfiInformation, "city") + ", ul. " + findValueByKey(gfiInformation, "address") + ", OIB: " + findValueByKey(gfiInformation, "idNumber") + ",";
            sheet.Cells["3", "A"].Value = reportInformation;

            reportInformation = "donijela je " + findValueByKey(gfiInformation, "currentDate") + " ovu";
            sheet.Cells["4", "A"].Value = reportInformation;

            sheet.Cells["32", "F"].Value = findValueByKey(gfiInformation, "director");

            reportInformation = "oporezivanja u svoti od " + findValueByKey(gfiInformation, "gain") + " kn (odnosno gubitaka u svoti od " + findValueByKey(gfiInformation, "loss") + " kn).";
            sheet.Cells["22", "A"].Value = reportInformation;

            string curranteDate = createCroatianDate();

            reportInformation = "Bilanca na dan " + curranteDate + " iskazuje zbroj aktive odnosno pasive u svoti od " + findValueByKey(gfiInformation, "totalAssets") + " kn.";
            sheet.Cells["24", "A"].Value = reportInformation;

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "[ASISTENT] " + findValueByKey(gfiInformation, "company") + " ODLUKA O UTVR. FIN. IZVJEŠĆA" + ".pdf");
            wb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, path);

            //closing app
            wb.Saved = true;
            wb.Save();
            wb.Close();
            excel.Quit();

            removeFile(tempfile);

            ApplicationManager.killExcelProcesses();

            return ErrorCodes.NoError;

        }

        public static int generateGfiReport2(string gfiPodPath)
        {
            ApplicationManager.killExcelProcesses();
            List<KeyValuePair<string, string>> gfiInformation = initGfiInformation(gfiPodPath);

            string tempfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "tempfile.xlsx");
            string template = Path.Combine(Directory.GetCurrentDirectory(), "../../../Resources/template_biljeske.xlsx");

            createFile(template, tempfile);

            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(tempfile);
            Worksheet sheet = (Worksheet)wb.ActiveSheet;

            string companyActivites = getNKDbyCode(findValueByKey(gfiInformation, "companyActivites"));

            string reportInformation = findValueByKey(gfiInformation, "company") + "\n" + findValueByKey(gfiInformation, "address") + "\n" + findValueByKey(gfiInformation, "postal") + " " + findValueByKey(gfiInformation, "city") + "\nOIB: " + findValueByKey(gfiInformation, "idNumber") + "\nMB: " + findValueByKey(gfiInformation, "mb") + "\nMBS: " + findValueByKey(gfiInformation, "mbs");
            sheet.Cells["1", "A"].Value = reportInformation;

            reportInformation = "Sjedište trgovačkog društva " + findValueByKey(gfiInformation, "company") + " (u daljnjem tekstu: “Društvo”) nalazi";
            sheet.Cells["50", "A"].Value = reportInformation;

            reportInformation = "se na adresi " + findValueByKey(gfiInformation, "address") + " " + findValueByKey(gfiInformation, "city") + ", u općini " + findValueByKey(gfiInformation, "localCenter") + ", županija";
            sheet.Cells["51", "A"].Value = reportInformation;

            reportInformation = "" + findValueByKey(gfiInformation, "county") + ".";
            sheet.Cells["52", "A"].Value = reportInformation;

            reportInformation = "Ovlaštena osoba za zastupanje je " + findValueByKey(gfiInformation, "director").ToUpper() + ".";
            sheet.Cells["53", "A"].Value = reportInformation;

            reportInformation = "Matični broj, dodijeljen od DZS-a, je " + findValueByKey(gfiInformation, "mb") + ", a matični broj subjekta (MBS), dodijeljen od";
            sheet.Cells["54", "A"].Value = reportInformation;

            reportInformation = "nadležnog trgovačkog suda, je " + findValueByKey(gfiInformation, "mbs") + ".";            
            sheet.Cells["55", "A"].Value = reportInformation;

            reportInformation = "Društvo je " + findValueByKey(gfiInformation, "companyOwnership") + ", a pripada kategoriji " + findValueByKey(gfiInformation, "companyType") + ".";
            sheet.Cells["56", "A"].Value = reportInformation;

            reportInformation = "Kapital Društva je " + findValueByKey(gfiInformation, "domesticCapital") + "% domaći te " + findValueByKey(gfiInformation, "foreignCapital") + "% strani.";
            sheet.Cells["57", "A"].Value = reportInformation;

            sheet.Cells["62", "A"].Value = companyActivites.ToUpper();

            reportInformation = "Status autonomnosti Društva: " + findValueByKey(gfiInformation, "companyAutonomy") + ".";
            sheet.Cells["64", "A"].Value = reportInformation;

            reportInformation = "Prosječni broj zaposlenih krajem razdoblja u prethodnoj godini bio je " + findValueByKey(gfiInformation, "numberOfEmployees1") + ", a na kraju tekuće";
            sheet.Cells["65", "A"].Value = reportInformation;

            reportInformation = "2021. godine je " + findValueByKey(gfiInformation, "numberOfEmployees2") + " zaposlenih.";
            sheet.Cells["66", "A"].Value = reportInformation;

            //findValueByKey(gfiInformation, "")
            reportInformation = "U 2020. godini Društvo je poslovalo " + findValueByKey(gfiInformation, "companyOperatingTime1") + " mjeseci, a u 2021. godini " + findValueByKey(gfiInformation, "companyOperatingTime2") + " mjeseci.";
            sheet.Cells["67", "A"].Value = reportInformation;

            reportInformation = "A.I. Prihodi od prodaje " + findValueByKey(gfiInformation, "salesIncome") + " kune";
            sheet.Cells["115", "A"].Value = reportInformation;

            reportInformation = "A.II. Prihodi na temelju upotrebe vlastitih proizvoda, robe i usluga " + findValueByKey(gfiInformation, "salesFromOwnProducts") + " kuna.";
            sheet.Cells["116", "A"].Value = reportInformation;

            reportInformation = "A.III. Ostali prihodi " + findValueByKey(gfiInformation, "otherIncome") + " kuna.";
            sheet.Cells["117", "A"].Value = reportInformation;

            reportInformation = "B.I. Prihodi s osnove tečajnih razlika " + findValueByKey(gfiInformation, "exchangeRateIncome") + " kuna.";
            excel.Cells["125", "A"].Value = reportInformation;

            reportInformation = "B.II. Prihodi od kamata " + findValueByKey(gfiInformation, "interestIncome") + " kuna.";
            excel.Cells["126", "A"].Value = reportInformation;

            reportInformation = "B.III. Prihodi od ulaganja u dionice " + findValueByKey(gfiInformation, "stockIncome") + " kuna.";
            excel.Cells["127", "A"].Value = reportInformation;

            reportInformation = "B.IV. Prihodi od zajmova " + findValueByKey(gfiInformation, "loanIncome") + " kuna.";
            excel.Cells["128", "A"].Value = reportInformation;

            reportInformation = "B.V. Ostali financijski prihodi " + findValueByKey(gfiInformation, "otherFinancialIncome") + " kuna.";
            excel.Cells["129", "A"].Value = reportInformation;

            reportInformation = "Ukupni rashodi Društva " + findValueByKey(gfiInformation, "company") + " u 2021. godini iznose " + findValueByKey(gfiInformation, "totalExpensesThisYear") + " kn, dok su";
            excel.Cells["132", "A"].Value = reportInformation;

            reportInformation = "prethodne godine iznosili " + findValueByKey(gfiInformation, "totalExpensesLastYear") + ", što znači da je zabilježena promjena od";
            excel.Cells["133", "A"].Value = reportInformation;

            decimal totalExpensesLastYear = castDecimalFromString(findValueByKey(gfiInformation, "totalExpensesLastYear"));
            decimal totalExpensesThisYear = castDecimalFromString(findValueByKey(gfiInformation, "totalExpensesThisYear"));
            decimal diff = totalExpensesThisYear - totalExpensesLastYear;
            decimal diffPercentage = (((totalExpensesThisYear - totalExpensesLastYear) / totalExpensesLastYear) * 100);
            diffPercentage = Decimal.Round(diffPercentage, 2);
            if (Decimal.Compare(totalExpensesThisYear, totalExpensesLastYear) == -1)
            {
                reportInformation = "" + diffPercentage.ToString() + "%, odnosno zabilježena je promjena u apsolutnom iznosu od " + createDecimalString(Convert.ToDouble(diff)) + " kn.";
            }
            else
            {
                reportInformation = "+" + diffPercentage.ToString() + "%, odnosno zabilježena je promjena u apsolutnom iznosu od +" + createDecimalString(Convert.ToDouble(diff)) + " kn.";
            }
            excel.Cells["134", "A"].Value = reportInformation;

            excel.Cells["140", "F"].Value = findValueByKey(gfiInformation, "director").ToUpper();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "[ASISTENT] " + findValueByKey(gfiInformation, "company") + " BILJEŠKE UZ FIN. IZVJEŠĆE.pdf");
            wb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, path);

            //closing app
            wb.Saved = true;
            wb.Save();
            wb.Close();
            excel.Quit();

            File.Delete(tempfile);
            ApplicationManager.killExcelProcesses();
            return ErrorCodes.NoError;
        }

        public static int generateGfiReport3(string gfiPodPath)
        {
            return ErrorCodes.NoError;
        }
    }
}
