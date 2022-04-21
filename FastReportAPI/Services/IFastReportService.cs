namespace FastReportAPI.Services;
public interface IFastReportService
{
    public string FillReport(Dictionary<string, string> parametrsList, string filename, ExportFormat format);
}