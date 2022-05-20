using FastReport;

namespace FastReportAPI.Services;
public class FastReportService : IFastReportService
{
    readonly IWebHostEnvironment _environment;

    public FastReportService(IWebHostEnvironment environment)
        => _environment = environment;

    public string FillReport(Dictionary<string, dynamic> json, string filename, ExportFormat format, string imageFormat)
    {
        using (var report = new Report())
        {
            var filePath = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Templates\\{filename}");
            report.Load($"{filePath}.frx");

            foreach(var parametr in json)
                report.SetParameterValue(parametr.Key, parametr.Value);

            bool hasTableData = json.ContainsKey("Data");
            if(hasTableData)
            {
                dynamic dyn = json["Data"];
                var conn = "Json={\"Data\":";
                report.Dictionary.Connections[0].ConnectionString = $"{conn}{Convert.ToString(dyn)}}}";
            }
            
            report.Prepare();
            
            var exportfile = new ExportBase();
            
            switch (format)
            {
                case ExportFormat.Pdf:
                    exportfile = new PDFExport();
                    break;
                case ExportFormat.Html:
                    var html = new HTMLExport();
                    
                    break;
                case ExportFormat.Mht:
                    exportfile = new MHTExport();
                    break;
                case ExportFormat.Image:
                    exportfile = new ImageExport();
                    break;
                case ExportFormat.Biff8:
                    exportfile = new Excel2003Document();
                    break;
                case ExportFormat.Csv:
                    exportfile = new CSVExport();
                    break;
                case ExportFormat.Dbf:
                    exportfile = new DBFExport();
                    break;
                case ExportFormat.Json:
                    exportfile = new JsonExport();
                    break;
                case ExportFormat.LaTeX:
                    exportfile = new LaTeXExport();
                    break;
                case ExportFormat.Odt:
                    exportfile = new ODTExport();
                    break;
                case ExportFormat.Ods:
                    exportfile = new ODSExport();
                    break;
                case ExportFormat.Docx:
                    exportfile = new Word2007Export();
                    break;
                case ExportFormat.Pptx:
                    exportfile = new PowerPoint2007Export();
                    break;
                case ExportFormat.Xlsx:
                    exportfile = new Excel2007Export();
                    break;
                case ExportFormat.Xps:
                    exportfile = new XPSExport();
                    break;
                case ExportFormat.Ppml:
                    exportfile = new PPMLExport();
                    break;
                case ExportFormat.PS:
                    exportfile = new PSExport();
                    break;
                case ExportFormat.Richtext:
                    exportfile = new RTFExport();
                    break;
                case ExportFormat.Svg:
                    exportfile = new SVGExport();
                    break;
                case ExportFormat.Text:
                    exportfile = new TextExport();
                    break;
                case ExportFormat.Xaml:
                    exportfile = new XAMLExport();
                    break;
                case ExportFormat.Xml:
                    exportfile = new XMLExport();
                    break;
                case ExportFormat.Zpl:
                    exportfile = new ZplExport();
                    break;
            }
            
            filePath = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}{GetExprotFormat(format, imageFormat)}");
            exportfile.Export(report, filePath);
            /*if(format == ExportFormat.Html)
            {
                ZipFile.CreateFromDirectory($"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}", $"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}.zip");
                using(var fs = new FileStream($"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}.zip", FileMode.Open))
                {
                    using(var archive = new ZipArchive(fs, ZipArchiveMode.Update))
                    {
                        archive.CreateEntry($"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}.html");
                    }
                }
                filePath = $"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}.zip";
            }*/
            return filePath;
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
            case ExportFormat.Mht:
                return ".mht";
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