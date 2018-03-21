using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    class IT2_UserInterface_Door
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private IDoor _door;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
        private UserInterface _uut;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _door =new Door();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();

            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void UserInterface_DoorOpen_LightOn()
        {
            //Act
            _door.Open();

            //Assert
            _light.Received().TurnOn();
        }

        [Test]
        public void UserInterface_DoorClose_LightOff()
        {
            //Act
            _door.Open();
            _door.Close();

            //Assert
            _light.Received().TurnOff();
        }

        //[Test]
        //public void UserInterface_DoorOpenPowersetup_LightOn()
        //{
        //    //Act
        //    _powerButton.Press();
        //    _door.Open();

        //    //Assert
        //    _light.Received().TurnOn();
        //}

        [Test]
        public void UserInterface_DoorOpenPowersetup_DisplayClear()
        {
            //Act
            _powerButton.Press();
            _door.Open();

            //Assert
            _display.Received().Clear();
        }
        [Test]
        public void UserInterface_DoorOpenTimesetup_DisplayClear()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _door.Open();

            //Assert
            _display.Received().Clear();
        }

        [Test]
        public void UserInterface_DoorOpenCooking_PowertubeOff()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _door.Open();

            //Assert
            _cookController.Received().Stop();

        }
    }
}
