using System;
using System.Collections.Generic;

namespace FlightLogNet.Models
{
    public class CsvReportModel
    {
        public long FlightId { get; set; }

        public DateTime TakeoffTime { get; set; }

        public DateTime? LandingTime { get; set; }

        public string Immatriculation { get; set; }

        public string Type { get; set; }

        public string Pilot { get; set; }

        public string Copilot { get; set; }

        public string Task { get; set; }

        public string TowplaneID { get; set; }

        public string GliderID { get; set; }

        public CsvReportModel(FlightModel model)
        {
            FlightId = model.Id;
            TakeoffTime = model.TakeoffTime;
            LandingTime = model.LandingTime;
            Immatriculation = model.Airplane?.Immatriculation;
            Type = model.Airplane?.Type;
            Pilot = $"{model.Pilot?.FirstName} {model.Pilot?.LastName}";
            Copilot = $"{model.Copilot?.FirstName} {model.Copilot?.LastName}";
            Task = model.Task;
        }

        public static IList<CsvReportModel> FromReportModel(ReportModel reportModel)
        {
            List<CsvReportModel> list = new List<CsvReportModel>();
            if (reportModel.Glider != null)
            {
                list.Add(new CsvReportModel(reportModel.Glider));
            }
            list.Add(new CsvReportModel(reportModel.Towplane));

            foreach (CsvReportModel m in list)
            {
                m.GliderID = reportModel.Glider?.Id.ToString();
                m.TowplaneID = reportModel.Towplane?.Id.ToString();
            }

            return list;
        }
    }
}
