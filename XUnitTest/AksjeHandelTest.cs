using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using Stock_trading_2.Controllers;
using Stock_trading_2.DAL;
using Stock_trading_2.Models;
using System.Text;

namespace XUnitTest
{
    public class AksjeHandelTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IAksjeRepository> mockRep = new Mock<IAksjeRepository>();
        private readonly Mock<ILogger<AksjeController>> mockLog = new Mock<ILogger<AksjeController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task HentAlleLoggetInnOK()
        {
            // Arrange
            var aksje1 = new Aksje
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Aksjenavn = "Apple",
                Pris = 154.2,
                Antall = 1
            };

            var aksje2 = new Aksje
            {
                Id = 1,
                Fornavn = "Ole",
                Etternavn = "Olsen",
                Aksjenavn = "Microsoft",
                Pris = 234.1,
                Antall = 2
            };

            var aksje3 = new Aksje
            {
                Id = 1,
                Fornavn = "Finn",
                Etternavn = "Finnsen",
                Aksjenavn = "Twitter",
                Pris = 107.3,
                Antall = 1
            };


            var aksjeListe = new List<Aksje>();
            aksjeListe.Add(aksje1);
            aksjeListe.Add(aksje2);
            aksjeListe.Add(aksje3);

            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(aksjeListe);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.HentAlle() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Aksje>>((List<Aksje>)resultat.Value, aksjeListe);
        }

        [Fact]
        public async Task HentAlleIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(It.IsAny<List<Aksje>>());

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.HentAlle() as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(k => k.Lagre(It.IsAny<Aksje>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Lagre(It.IsAny<Aksje>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Aksje lagret!", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.Lagre(It.IsAny<Aksje>())).ReturnsAsync(false);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Lagre(It.IsAny<Aksje>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Aksje ble ikke lagret!", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnFeilModel()
        {
            // Arrange
            var aksje1 = new Aksje
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Aksjenavn = "Apple",
                Pris = 154.2,
                Antall = 1
            };

            mockRep.Setup(k => k.Lagre(aksje1)).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            aksjeController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Lagre(aksje1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering!", resultat.Value);
        }

        [Fact]
        public async Task LagreIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Lagre(It.IsAny<Aksje>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Lagre(It.IsAny<Aksje>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Slett(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Aksje slettet!", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(false);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Slett(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Aksje ble ikke slettet!", resultat.Value);
        }

        [Fact]
        public async Task SletteIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var akjseController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            akjseController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await akjseController.Slett(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentEnLoggetInnOK()
        {
            // Arrange
            var aksje1 = new Aksje
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Aksjenavn = "Apple",
                Pris = 154.2,
                Antall = 1
            };

            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(aksje1);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.HentEn(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Aksje>(aksje1, (Aksje)resultat.Value);
        }

        [Fact]
        public async Task HentEnLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(() => null); // merk denne null setting!

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.HentEn(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke aksjen!", resultat.Value);
        }

        [Fact]
        public async Task HentEnIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(() => null);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.HentEn(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(k => k.Endre(It.IsAny<Aksje>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Endre(It.IsAny<Aksje>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Aksje endret!", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.Lagre(It.IsAny<Aksje>())).ReturnsAsync(false);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Endre(It.IsAny<Aksje>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Aksje ble ikke endret!", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnFeilModel()
        {
            // Arrange
            var aksje1 = new Aksje
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Aksjenavn = "Apple",
                Pris = 154.2,
                Antall = 1
            };

            mockRep.Setup(k => k.Endre(aksje1)).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            aksjeController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Endre(aksje1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering!", resultat.Value);
        }

        [Fact]
        public async Task EndreIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Endre(It.IsAny<Aksje>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.Endre(It.IsAny<Aksje>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LoggInnOK()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnInputFeil()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            aksjeController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server!");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await aksjeController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server!", resultat.Value);
        }

        [Fact]
        public void LoggUt()
        {
            var aksjeController = new AksjeController(mockRep.Object, mockLog.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            aksjeController.LoggUt();

            // Assert
            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }
    }
}
