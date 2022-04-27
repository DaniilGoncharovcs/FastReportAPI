namespace FastReportAPI.Services;
public class FastReportService : IFastReportService
{
    readonly IWebHostEnvironment _environment;

    public FastReportService(IWebHostEnvironment environment)
        => _environment = environment;

    public string FillReport(Dictionary<string, dynamic> json, string filename, ExportFormat format)
    {
        using (var report = new Report())
        {
            var filePath = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Templates\\{filename}");
            report.Load($"{filePath}.frx");

            foreach(var parametr in json)
                report.SetParameterValue(parametr.Key, parametr.Value);
            
            dynamic dyn = json["Data"];
            var conn = "Json={\"Data\":";
            report.Dictionary.Connections[0].ConnectionString = $"{conn}{Convert.ToString(dyn)}}}";
            
            report.Prepare();

            switch (format)
            {
                case ExportFormat.Pdf:
                    PDFSimpleExport pdf = new PDFSimpleExport();
                    filePath = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}{GetExprotFormat(format)}");
                    pdf.Export(report, filePath);
                    return filePath;
                    break;
                case ExportFormat.Html:
                    HTMLExport html = new HTMLExport();
                    filePath = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}{GetExprotFormat(format)}");
                    html.Export(report, filePath);
                    break;
                
            }
        }
        return "";
    }
    private string GetExprotFormat(ExportFormat format, string expectedFormat = null)
    {
        switch (format)
        {
            case ExportFormat.Pdf:
                return ".pdf";

            case ExportFormat.Html:
                return ".html";

            case ExportFormat.Image:
                switch (expectedFormat)
                {
                    case "Jpeg":
                        return ".jpeg";

                    case "Png":
                        return ".png";

                    case "Gif":
                        return ".gif";

                    case "Bmp":
                        return ".bmp";

                    case "Mtf":
                        return ".mtf";

                    case "Tiff":
                        return ".tiff";

                    default:
                        return "";
                }
            case ExportFormat.Biff8:
                return ".xls";

            case ExportFormat.Csv:
                return ".csv";

            case ExportFormat.Dbf:
                return ".dbf";

            case ExportFormat.Json:
                return ".json";

            case ExportFormat.LaTeX:
                return ".latex";

            case ExportFormat.Ods:
                return ".ods";

            case ExportFormat.Odt:
                return ".odt";

            case ExportFormat.Docx:
                return ".docx";

            case ExportFormat.Pptx:
                return ".pptx";

            case ExportFormat.Xlsx:
                return ".xlsx";

            case ExportFormat.Xps:
                return ".xps";

            case ExportFormat.Ppml:
                return ".ppml";

            case ExportFormat.PS:
                return ".ps";

            case ExportFormat.Richtext:
                return ".rtf";

            case ExportFormat.Svg:
                return ".svg";

            case ExportFormat.Text:
                return ".txt";

            case ExportFormat.Xaml:
                return ".xaml";

            case ExportFormat.Xml:
                return ".xml";

            case ExportFormat.Zpl:
                return ".zpl";

            default:
                throw new NotImplementedException(string.Format("Format {0} is not supported", format));
        }
    }
}