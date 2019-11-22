using Doug.Commands;
using Doug.Effects;
using Doug.Models;
using Doug.Models.Slurs;
using Doug.Repositories;
using Doug.Services;
using Doug.Slack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Slurs
{
    [TestClass]
    public class AddSlurCommandTest
    {
        private const string CommandText = "heheahahasod";
        private const string Channel = "coco-channel";
        private const string User = "testuser";

        private readonly Command _command = new Command()
        {
            ChannelId = Channel,
            Text = CommandText,
            UserId = User
        };

        private SlursCommands _slursCommands;

        private readonly Mock<ISlurRepository> _slurRepository = new Mock<ISlurRepository>();
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<ISlackWebApi> _slack = new Mock<ISlackWebApi>();
        private readonly Mock<IAuthorizationService> _adminValidator = new Mock<IAuthorizationService>();
        private readonly Mock<IEventDispatcher> _eventDispatcher = new Mock<IEventDispatcher>();
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
        private readonly Mock<ICreditsRepository> _creditsRepository = new Mock<ICreditsRepository>();

        [TestInitialize]
        public void Setup()
        { 
            _slursCommands = new SlursCommands(_slurRepository.Object, _userRepository.Object, _slack.Object, _adminValidator.Object, _eventDispatcher.Object, _userService.Object, _creditsRepository.Object);
        }

        [TestMethod]
        public void WhenAddingASlur_SlurIsAdded()
        {
            _slursCommands.AddSlur(_command);

            _slurRepository.Verify(repo => repo.AddSlur(It.IsAny<Slur>()));
        }

        [TestMethod]
        public void WhenAddingASlur_UserGetTwoRupee()
        {
            _slursCommands.AddSlur(_command);

            _creditsRepository.Verify(repo => repo.AddCredits(User, 2));
        }
    }
}
