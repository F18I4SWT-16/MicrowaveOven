using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    public class Driver
    {
        private Button powerButton;
        private Button timeButton;
        private Button startCancelButton;
        private IDoor _door;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
        private UserInterface UI;

        [SetUp]
        public void SetUp()
        {
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();

            UI = new UserInterface(powerButton, timeButton, startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void testbawf()
        {
            powerButton.Press();


            _display.Received().ShowPower();
            //Assert.That(UI.OnPowerPressed(powerButton.Pressed), _display.Received())
        }


    }
}
