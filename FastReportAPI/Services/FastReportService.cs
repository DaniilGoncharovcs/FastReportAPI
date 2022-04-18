namespace FastReportAPI.Services;
public class FastReportService : IFastReportService
{
    public string FillReport(Dictionary<string, string> parametrsList, string path, string format)
    {
        using (var report = new Report())
        {
            report.Load($"{path}.frx");
            foreach(var parametr in parametrsList)
                report.SetParameterValue(parametr.Key, parametr.Value);
            report.Prepare();
            switch (format)
            {
                case "pdf":
                    PDFSimpleExport pdf = new PDFSimpleExport();
                    path = $"{path}.{format}";
                    pdf.Export(report, path);
                    return path;
                    break;
            }
        }
        return "";
    }
}