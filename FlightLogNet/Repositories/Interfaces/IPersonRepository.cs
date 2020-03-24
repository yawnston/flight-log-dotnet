namespace FlightLogNet.Repositories.Interfaces
{
    using FlightLogNet.Models;

    public interface IPersonRepository
    {
        long AddGuestPerson(PersonModel person);

        bool TryGetPerson(PersonModel airplaneModel, out long airplaneId);

        long CreateClubMember(PersonModel person);
    }
}