namespace FastReportAPI.Services;
public class FastReportService : IFastReportService
{
    readonly IWebHostEnvironment _environment;

    public FastReportService(IWebHostEnvironment environment)
        => _environment = environment;

    public string FillReport(Dictionary<string, string> parametrsList, string filename, string format)
    {
        using (var report = new Report())
        {
            var filePath = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Templates\\{filename}");
            report.Load($"{filePath}.frx");
            foreach(var parametr in parametrsList)
                report.SetParameterValue(parametr.Key, parametr.Value);
            report.Prepare();
            switch (format)
            {
                case "pdf":
                    PDFSimpleExport pdf = new PDFSimpleExport();
                    filePath = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Files\\{filename}.{format}");
                    pdf.Export(report, filePath);
                    return filePath;
                    break;
            }
        }
        return "";
    }
}