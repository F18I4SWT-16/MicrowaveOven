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
    class IT4_Display_CookController
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
        }

        [Test]
        public void Display_PressStart_ShowTime()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _output.Received().OutputLine($"Display shows: 01:00");
        }
    }
}
