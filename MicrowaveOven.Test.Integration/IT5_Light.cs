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
    class IT5_Light
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Door _door;
        private Light _uut;
        private Display _display;
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
            _output = Substitute.For<IOutput>();
            _uut = new Light(_output);
            _display = new Display(_output);

            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _uut, _cookController);

            _cookController.UI = _userInterface;

        }

        [Test]
        public void Light_OpenDoor_LightOn()
        {
            //Act
            _door.Open();

            //Assert
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void Light_PressStart_LightOn()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void Light_CloseDoor_LightOff()
        {
            //Act
            _door.Open();
            _door.Close();

            //Assert
            _output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void Light_CookingDone_LightOff()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            //Assert
            _output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void Light_CookingPressCancel_LightOff()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            //Assert
            _output.Received().OutputLine("Light is turned off");
        }
        
    }
}
