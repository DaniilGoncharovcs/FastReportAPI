namespace FastReportAPI.Services;
public interface IFastReportService
{
    string FillReport(Dictionary<string, string> parametrsList, string path, string format);
}