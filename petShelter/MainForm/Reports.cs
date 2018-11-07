using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace MainForm
{
    class Reports
    {
        int usedColumnCount; //Количество используемых колонок
        Excel.Application excelApp;
        Excel.Workbook excelWorkBook;
        Excel.Worksheet excelWorkSheet;
        string path;
        int currentRow;


        public string AskFileName()
        {
            SaveFileDialog SFD = new SaveFileDialog();

            SFD.Filter = "Excel files(*.xlsx)|*.xlsx"; ;
            SFD.CreatePrompt = true;
            SFD.FileName = DateTime.Today.ToString("dd/MM/yyyy") + ".xlsx";
            SFD.RestoreDirectory = true;
            SFD.Title = "Export To Excel File";
            if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return SFD.FileName;
            }
            return null;
        }
        public void CreateReportFromVisibleItems(DataGridView DG, string Title)
        {


            path = AskFileName();
            if (path == null)
                return;
            currentRow = 1; //текущая строка в файле Excel
            excelApp = new Excel.Application();
            excelWorkBook = new Excel.Workbook();
            excelWorkSheet = new Excel.Worksheet();
            excelWorkBook = excelApp.Workbooks.Add();

            excelWorkSheet = excelWorkBook.Sheets.Add();
            //имя листа
            excelWorkSheet.Name = "Отчёт";
            //добавление заголовка
            excelWorkSheet.Cells[currentRow, 1] = Title;
            currentRow++;
            excelWorkSheet.Cells[currentRow, 1] = "Дата: " + DateTime.Today.ToString("dd/MM/yyyy");
            currentRow += 2; //не меняй, а то форматирование уплывет
            //Поиск выборка только видимых элементов для отчета
            List<DataGridViewColumn> listVisible = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in DG.Columns)
            {
                if (col.Visible)
                    listVisible.Add(col);
            }

            usedColumnCount = listVisible.Count;
            int rowCount = DG.RowCount;
            excelWorkSheet.Rows[currentRow].Font.Bold = true;
            for (int i = 0; i < listVisible.Count; i++)
            {
                excelWorkSheet.Cells[currentRow, i + 1] = listVisible[i].HeaderText;
            }
            for (int y = 0; y < rowCount; y++)
            {
                currentRow++;
                for (int x = 0; x < listVisible.Count; x++)
                {
                    for (int j = 0; j < listVisible.Count; j++)
                    {
                        excelWorkSheet.Cells[currentRow, x + 1] = DG.Rows[y].Cells[listVisible[x].Name].Value.ToString();
                    }
                }
            }
            Formating();
            SaveToFile();
        }
        private void SaveToFile()
        {
            try
            {
                excelWorkBook.SaveCopyAs(path);
                excelWorkBook.Saved = true;
                MessageBox.Show("Отчет сохранен в файл: " + path, "Отчет сохранен!", MessageBoxButtons.OK);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                excelWorkBook.Saved = false;
                MessageBox.Show("Отчет не сохранен!\nПроверьте не используется ли файл другой программой или создайте другой файл.\nФайл: " + path, "Ошибка сохранения!", MessageBoxButtons.OK);
            }
            excelWorkBook.Close(true);
            excelApp.Quit();

        }
        private void Formating()
        {
            excelWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;
            MergeCells(1, 1, 1, usedColumnCount);
            MergeCells(2, 1, 2, usedColumnCount);
            SetRangeBorders(4, 1, currentRow, usedColumnCount);
            //SetRangeAlignment(5, 2, currentRow, usedColumnCount, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //SetRangeAlignment(1, 1, 2, 1, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft);

            // Autofit columns
            excelWorkSheet.Columns.AutoFit();
        }
        private void MergeCells(int x, int xx, int y, int yy)
        {
            excelWorkSheet.Range[excelWorkSheet.Cells[x, xx], excelWorkSheet.Cells[y, yy]].Merge();
        }
        private void SetRangeBorders(int x, int xx, int y, int yy)
        {
            excelWorkSheet.Range[excelWorkSheet.Cells[x, xx], excelWorkSheet.Cells[y, yy]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

        }
        private void SetRangeAlignment(int x, int xx, int y, int yy, Microsoft.Office.Interop.Excel.XlHAlign alignment)
        {
            /*excelWorkSheet.Range[excelWorkSheet.Cells[x, xx], excelWorkSheet.Cells[y, yy]].Style.HorizontalAlignment = alignment;

            Excel.Range range = excelWorkSheet.Range[excelWorkSheet.Cells[x, xx], excelWorkSheet.Cells[y, yy]];
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;*/

        }





    }
}
