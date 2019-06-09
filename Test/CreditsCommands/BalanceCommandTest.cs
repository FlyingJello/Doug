using Doug;
using Doug.Commands;
using Doug.Models;
using Doug.Repositories;
using Doug.Slack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class BalanceCommandTest
    {
        private const string CommandText = "<@otherUserid|username>";
        private const string Channel = "coco-channel";
        private const string User = "testuser";

        private readonly Command command = new Command()
        {
            ChannelId = Channel,
            Text = CommandText,
            UserId = User
        };

        private CreditsCommands _creditsCommands;

        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<ISlackWebApi> _slack = new Mock<ISlackWebApi>();

        [TestInitialize]
        public void Setup()
        {
            _userRepository.Setup(repo => repo.GetUser(User)).Returns(new User() { Id = "bobob", Credits = 79});

            _creditsCommands = new CreditsCommands(_userRepository.Object, _slack.Object);
        }

        [TestMethod]
        public void WhenCheckingBalance_GetInformationFromUser()
        {
            _creditsCommands.Balance(command);

            _userRepository.Verify(repo => repo.GetUser(User));
        }

        [TestMethod]
        public void WhenCheckingBalance_MessageIsSentPrivately()
        {
            var message = _creditsCommands.Balance(command);

            Assert.AreEqual("You have 79 " + DougMessages.CreditEmoji, message);
        }
    }
}