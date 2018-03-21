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
    class IT4_Display_UserInterface
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Door _door;
        private ILight _light;
        private Display _uut;
        private CookController _cookController;
        private UserInterface _userInterface;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;


        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _door = new Door();
            _light = Substitute.For<ILight>();
            _output = Substitute.For<IOutput>();
            _uut = new Display(_output);

            _cookController = new CookController(_timer, _uut, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _uut, _light, _cookController);
            
            _cookController.UI = _userInterface;

        }

        [Test]
        public void Display_PressPowerButton_ShowPower()
        {
            //Act
            _powerButton.Press();

            //Assert
            _output.Received().OutputLine($"Display shows: 50 W");
        }

        [Test]
        public void Display_PressTimeButton_ShowTime()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();

            //Assert
            _output.Received().OutputLine($"Display shows: 01:00");
        }

        [Test]
        public void Display_CookingDone_ClearDisplay()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            //Assert
            _output.Received().OutputLine($"Display cleared");

        }

        [Test]
        public void Display_CancelCooking_ClearDisplay()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            //Assert
            _output.Received().OutputLine($"Display cleared");

        }
    }
}
