using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT6_Timer
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Door _door;
        private Light _light;
        private Display _display;
        private CookController _cookController;
        private UserInterface _userInterface;
        private Timer _uut;
        private IPowerTube _powerTube;
        private IOutput _output;


        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _uut = new Timer();
            _powerTube = Substitute.For<IPowerTube>();
            _door = new Door();
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _display = new Display(_output);

            _cookController = new CookController(_uut, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _cookController.UI = _userInterface;
        }

        [Test]
        public void Timer_PressStart_ShowTime()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            Thread.Sleep(1000);

            //Assert
            _output.Received().OutputLine($"Display shows: 00:59");          
        }


        [Test]
        public void Timer_CookingIsDone_ClearDisplay()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            Thread.Sleep(60100); //Vent i 60.1s

            //Assert
            _output.Received().OutputLine($"Display cleared");
        }

    }
}
