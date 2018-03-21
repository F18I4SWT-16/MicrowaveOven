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
    class IT3_CookController
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Door _door;
        private ILight _light;
        private IDisplay _display;
        private CookController _uut;
        private UserInterface _userInterface;
        private ITimer _timer;
        private IPowerTube _powerTube;


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
            _display = Substitute.For<IDisplay>();

            _uut = new CookController(_timer, _display, _powerTube);

            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _uut);

            _uut.UI = _userInterface;
        }

        [Test]
        public void Cookcontroller_StartCooking_PowertubeOn()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _powerTube.Received().TurnOn(50);
        }
        [Test]
        public void Cookcontroller_StartCooking_TimerStart()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _timer.Received().Start(60);
        }
        [Test]
        public void Cookcontroller_StartCooking_DisplayUpdate()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _display.Received().ShowTime(1,0);
        }
        [Test]
        public void Cookcontroller_PressStartButtonWhileCooking_PowertubeTurnOff()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            //Assert
            _powerTube.Received().TurnOff();
        }

        [Test]
        public void CookController_CookingIsDone_DisplayClear()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);


            //Assert
            _display.Received().Clear();
        }

        [Test]
        public void CookController_CookingIsDone_LightOff()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);


            //Assert
            _light.Received().TurnOff();
        }
    }
}
