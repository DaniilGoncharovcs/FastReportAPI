namespace FastReportAPI.Services;
public interface IFastReportService
{
    public string FillReport(Dictionary<string, dynamic> parametrsList, string filename,
        ExportFormat format, ImageExportFormat imageExport);
}
