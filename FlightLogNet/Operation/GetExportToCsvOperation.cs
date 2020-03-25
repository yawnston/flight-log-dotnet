namespace FlightLogNet.Operation
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using CsvHelper;
    using CsvHelper.Configuration;
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
            // TODO 5.1: Naimplementujte export do CSV
            // TIP: CSV soubor je pouze string, který se dá vytvořit pomocí třídy StringBuilder
            // TIP: Do bytové reprezentace je možné jej převést například pomocí metody: Encoding.UTF8.GetBytes(..)
            using StringWriter writer = new StringWriter();
            using CsvWriter csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ReferenceHeaderPrefix = (_, name) => name
            });
            csv.WriteRecords(flightRepository.GetReport());

            return Encoding.UTF8.GetBytes(writer.ToString());
        }
    }
}
