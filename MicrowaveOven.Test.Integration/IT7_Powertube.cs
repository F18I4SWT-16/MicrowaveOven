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
    class IT7_Powertube
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Door _door;
        private Light _light;
        private Display _display;
        private CookController _cookController;
        private UserInterface _userInterface;
        private Timer _timer;
        private PowerTube _uut;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _uut = new PowerTube(_output);
            _door = new Door();
            _light = new Light(_output);
            _display = new Display(_output);

            _cookController = new CookController(_timer, _display, _uut);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _cookController.UI = _userInterface;
        }

        [Test]
        public void Powertube_StartCooking_PowertubeOn()
        {
            //Arrange
            double a = (50*100)/700;
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            
            //Assert
            _output.Received().OutputLine($"PowerTube works with " + a + " %");
        }
        [Test]
        public void Powertube_StopCooking_PowertubeOff()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            //Assert
            _output.Received().OutputLine($"PowerTube turned off");
        }


    }
}
