namespace FlightLogNet.Operation
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CsvHelper;
    using CsvHelper.Configuration;
    using FlightLogNet.Models;
    using FlightLogNet.Repositories.Interfaces;

    public class GetExportToCsvOperation
    {
        private readonly IFlightRepository flightRepository;

        public GetExportToCsvOperation(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public byte[] Execute()
        {
            using StringWriter writer = new StringWriter();
            using CsvWriter csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ")));
            List<CsvReportModel> report = flightRepository.GetReport().SelectMany(x => CsvReportModel.FromReportModel(x)).ToList();
            csv.WriteRecords(report);

            return Encoding.UTF8.GetBytes(writer.ToString());
        }
    }
}
