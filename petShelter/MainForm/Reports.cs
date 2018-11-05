using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace MainForm
{
    class Reports
    {
        public string AskFileName(SaveFileDialog SFD)
        {
            //Save Dialog Init

            SFD.Filter = "Text files(*.xlsx)|*.xlsx|Text files(*.xls)|*.xls|All files(*.*)|*.*"; ;
            SFD.CreatePrompt = true;
            SFD.FileName = DateTime.Today.ToString("dd/MM/yyyy") + ".xlsx";
            SFD.RestoreDirectory = true;
            SFD.Title = "Export Excel File To";
            if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return SFD.FileName;
            }
            return null;
        }
        public void CreateReportFromVisibleItems(DataGridView DG, string Title, SaveFileDialog SFD)
        {

            string path = AskFileName(SFD);

            int currentRow = 1; //текущая строка в файле Excel
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
            //Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(filename);
            Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
            //имя листа
            excelWorkSheet.Name = "Отчёт";
            //добавление заголовка
            excelWorkSheet.Cells[currentRow, 1] = Title;
            currentRow++;
            excelWorkSheet.Cells[currentRow, 1] = DateTime.Today.ToString("dd/MM/yyyy");
            currentRow += 2;
            //Поиск выборка только видимых элементов для отчета
            List<DataGridViewColumn> listVisible = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in DG.Columns)
            {
                if (col.Visible)
                    listVisible.Add(col);
            }
            int rowCount = DG.RowCount;
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

            excelWorkBook.SaveCopyAs(path);
            excelWorkBook.Saved = true;
            excelWorkBook.Close(true);
            excelApp.Quit();
            MessageBox.Show("Отчет сохранен в файл: " + path, "Отчет сохранен!", MessageBoxButtons.OK);
        }
    }
    

}
