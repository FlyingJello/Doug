using System.Collections.Generic;
using Doug.Items;
using Doug.Models.User;
using Doug.Repositories;
using Doug.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Shop
{
    [TestClass]
    public class SellTest
    {
        private const string User = "testuser";

        private ShopService _shopService;

        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IInventoryRepository>  _inventoryRepository = new Mock<IInventoryRepository>();
        private readonly Mock<IItemRepository> _itemRepository = new Mock<IItemRepository>();
        private readonly Mock<IGovernmentService> _governmentService = new Mock<IGovernmentService>();
        private readonly Mock<ICreditsRepository> _creditsRepository = new Mock<ICreditsRepository>();

        private User _user;

        [TestInitialize]
        public void Setup()
        {
            var items = new List<InventoryItem>()
            {
                new InventoryItem("testuser", "testitem") { InventoryPosition = 4, Item = new Consumable() {Price = 1337 * 2} },
                new InventoryItem("testuser", "testitem") { InventoryPosition = 3, Item = new EquipmentItem() }
            };

            _user = new User() { Id = "testuser", InventoryItems = items };
            _userRepository.Setup(repo => repo.GetUser(User)).Returns(_user);

            _shopService = new ShopService(_inventoryRepository.Object, _itemRepository.Object, _governmentService.Object, _creditsRepository.Object);
        }

        [TestMethod]
        public void WhenSelling_MoneyIsAdded()
        {
            _shopService.Sell(_user, 4);

            _creditsRepository.Verify(repo => repo.AddCredits(User, 1337));
        }

        [TestMethod]
        public void WhenSelling_ItemIsRemoved()
        {
            _shopService.Sell(_user, 4);

            _inventoryRepository.Verify(repo => repo.RemoveItem(_user, 4));
        }

        [TestMethod]
        public void GivenUserHasNoItemInSlot_WhenSelling_NoItemMessageSent()
        {
            var user = new User { Id = "testuser", InventoryItems = new List<InventoryItem>() };

            var result = _shopService.Sell(user, 4);

            Assert.AreEqual("There is no item in slot 4.", result.Message);
        }

        //[TestMethod]
        //public void GivenItemIsNotTradable_WhenSelling_ItemNotTradableMessage()
        //{
        //    var result = _shopService.Sell(_user, 3);

        //    Assert.AreEqual("This item is not tradable.", result.Message);
        //}
    }
}
