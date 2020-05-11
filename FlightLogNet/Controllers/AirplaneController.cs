namespace FlightLogNet.Controllers
{
    using System.Collections.Generic;
    using FlightLogNet.Facades;
    using FlightLogNet.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [RequireHttps]
    [Route("[controller]")]
    public class AirplaneController : ControllerBase
    {
        private readonly ILogger<AirplaneController> logger;
        private readonly AirplaneFacade airplaneFacade;

        public AirplaneController(ILogger<AirplaneController> logger, AirplaneFacade airplaneFacade)
        {
            this.logger = logger;
            this.airplaneFacade = airplaneFacade;
        }

        // TODO 3.1: Vystavte REST HTTPGet metodu vracející seznam klubových letadel
        // Letadla získáte voláním airplaneFacade
        // dotazované URL je /airplane
        // Odpověď by měla být kolekce AirplaneModel
        [HttpGet]
        public IEnumerable<AirplaneModel> GetClubAirplanes()
        {
            logger.LogDebug("Get club airplanes.");
            return airplaneFacade.GetClubAirplanes();
        }
    }
}
