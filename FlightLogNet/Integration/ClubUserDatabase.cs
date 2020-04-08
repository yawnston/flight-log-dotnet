namespace FlightLogNet.Integration
{
    using System.Collections.Generic;
    using System.Linq;
    using FlightLogNet.Models;
    using Microsoft.Extensions.Configuration;
    using RestSharp;

    public class ClubUserDatabase : IClubUserDatabase
    {
        private readonly string baseAddress;

        // TODO 8.1: Přidejte si přes dependency injection configuraci
        public ClubUserDatabase(IConfiguration configuration)
        {
            baseAddress = configuration["ClubUsersApi"];
        }

        public bool TryGetClubUser(long memberId, out PersonModel personModel)
        {
            personModel = GetClubUsers().FirstOrDefault(person => person.MemberId == memberId);

            return personModel != null;
        }

        public IList<PersonModel> GetClubUsers()
        {
            IList<ClubUser> x = ReceiveClubUsers();
            return TransformToPersonModel(x);
        }

        private IList<ClubUser> ReceiveClubUsers()
        {
            // TODO 8.2: Naimplementujte volání endpointu ClubDB pomocí RestSharp
            RestClient client = new RestClient(baseAddress);
            RestRequest request = new RestRequest("club/user", DataFormat.Json);

            return client.Get<List<ClubUser>>(request).Data;
        }

        private IList<PersonModel> TransformToPersonModel(IList<ClubUser> users)
        {
            return users.Select(u => new PersonModel
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                MemberId = u.MemberId
            }).ToList();
        }
    }
}
