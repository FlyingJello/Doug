using System.Threading.Tasks;
using Doug.Controllers;
using Doug.Models.User;
using Doug.Repositories;
using Doug.Services.MenuServices;
using Doug.Slack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Stats
{
    [TestClass]
    public class AttributeTest
    {
        private const string Channel = "coco-channel";
        private const string User = "testuser";

        private readonly Interaction _interaction = new Interaction
        {
            ChannelId = Channel,
            UserId = User,
            Action = "attribution",
            Value = "luck"
        };

        private StatsMenuService _userMenuService;

        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IStatsRepository> _statsRepository = new Mock<IStatsRepository>();
        private readonly Mock<ISlackWebApi> _slack = new Mock<ISlackWebApi>();
        private readonly Mock<IPartyRepository> _partyRepository = new Mock<IPartyRepository>();
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            _user = new User() { Id = "testuser", Experience = 0 };
            _userRepository.Setup(repo => repo.GetUser(User)).Returns(_user);

            _userMenuService = new StatsMenuService(_statsRepository.Object, _userRepository.Object, _slack.Object, _partyRepository.Object);
        }

        [TestMethod]
        public async Task WhenAttributingLuck_LuckStatIncrease()
        {
            await _userMenuService.AttributeStatPoint(_interaction);

            _statsRepository.Verify(repo => repo.AttributeStatPoint(User, "luck"));
        }

        [TestMethod]
        public async Task GivenUserHasNoFreePoints_WhenAttributingLuck_LuckStatDoNotIncrease()
        {
            _userRepository.Setup(repo => repo.GetUser(User)).Returns(new User() { Id = "testuser", Luck = 10 });

            await _userMenuService.AttributeStatPoint(_interaction);

            _statsRepository.Verify(repo => repo.AttributeStatPoint(User, "luck"), Times.Never);
        }
    }
}
