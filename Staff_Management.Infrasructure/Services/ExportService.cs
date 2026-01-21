using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Staff_Management.Application.Features;
using Staff_Management.Application.Services;


namespace Staff_Management.Infrasructure.Services
{
    public class ExportService : IExportService
    {
        public byte[] GenerateStaffsPDFReport(List<StaffInfo> data)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);

                    // Page header
                    page.Header().Column(col =>
                    {
                        col.Item().Text("STAFF REPORT")
                            .FontSize(24)
                            .Bold()
                            .FontColor(QuestPDF.Helpers.Colors.Blue.Medium)
                            .AlignCenter();

                        col.Item().Text($"Generated on {DateTime.Now:dd MMM yyyy}")
                            .FontSize(10)
                            .AlignCenter()
                            .FontColor(QuestPDF.Helpers.Colors.Grey.Darken2);

                        col.Item().PaddingVertical(5).LineHorizontal(1);
                    });

                    // Table content
                    page.Content().PaddingTop(10).Table(table =>
                    {
                        // Define columns
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                        });

                        // Header row
                        table.Header(header =>
                        {
                            header.Cell().Background(QuestPDF.Helpers.Colors.Teal.Lighten3).Padding(6)
                                .Text("Staff ID").Bold();
                            header.Cell().Background(QuestPDF.Helpers.Colors.Teal.Lighten3).Padding(6)
                                .Text("Full Name").Bold();
                            header.Cell().Background(QuestPDF.Helpers.Colors.Teal.Lighten3).Padding(6)
                                .Text("Birthday").Bold();
                            header.Cell().Background(QuestPDF.Helpers.Colors.Teal.Lighten3).Padding(6)
                                .Text("Gender").Bold();
                        });

                        // Data rows 
                        bool alternate = false;
                        foreach (var staff in data)
                        {
                            var bgColor = alternate ? QuestPDF.Helpers.Colors.Grey.Lighten4 : QuestPDF.Helpers.Colors.White;
                            alternate = !alternate;

                            table.Cell().Background(bgColor).Padding(6)
                                .Text(staff.StaffId ?? "");
                            table.Cell().Background(bgColor).Padding(6)
                                .Text(staff.FullName ?? "");
                            table.Cell().Background(bgColor).Padding(6)
                                .Text(staff.Birthday.ToString("dd MMM yyyy"));
                            table.Cell().Background(bgColor).Padding(6)
                                .Text(staff.Gender == 1 ? "Male" : "Female");
                        }
                    });

                    // footer
                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Page ");
                        txt.CurrentPageNumber();
                        txt.Span(" / ");
                        txt.TotalPages();
                    });
                });
            }).GeneratePdf();
        }

        public byte[] GenerateStaffsExcelReport(List<StaffInfo> data)
        {
            using var memoryStream = new MemoryStream();

            using (var document = SpreadsheetDocument.Create(
                memoryStream,
                SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                var sheets = document.WorkbookPart!.Workbook.AppendChild(new Sheets());
                sheets.Append(new Sheet
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Staffs"
                });

                // Header
                sheetData.Append(CreateRow( "Staff ID", "Full Name", "Birthday", "Gender" ));

                // Data rows
                foreach (var staff in data)
                {
                    sheetData.Append(CreateRow(
                        staff.StaffId ?? "",
                        staff.FullName ?? "",
                        staff.Birthday.ToString("yyyy-MM-dd"),
                        staff.Gender == 1 ? "Male" : "Female"
                    ));
                }

                workbookPart.Workbook.Save();
            }

            return memoryStream.ToArray();
        }

        private static Row CreateRow(params string[] values)
        {
            var row = new Row();

            foreach (var value in values)
            {
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(value)
                });
            }

            return row;
        }
    }
}