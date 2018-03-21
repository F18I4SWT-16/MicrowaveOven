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
    public class IT1_UserInterface_Buttons
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

            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();

            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        #region PowerButton
        [TestCase(1, 50)]
        [TestCase(2, 100)]
        [TestCase(14, 700)]
        [TestCase(15, 50)]
        public void UserInterface_OnPowerPressed_PowerIsCorrect(int count, int expected)
        {
            //Act
            for (int i = 0; i < count; i++)
            {
                _powerButton.Press();
            }

            //Assert
            _display.Received().ShowPower(expected);
        }
        #endregion


        #region TimeButton
        [TestCase(1, 1)]
        [TestCase(50, 50)]
        [TestCase(100, 100)]
        public void UserInterface_OnTimePressed_TimeIsCorrect(int count, int expected)
        {
            //Act
            _powerButton.Press();
            for (int i = 0; i < count; i++)
            {
                _timeButton.Press();
            }

            //Assert
            _display.Received().ShowTime(expected, 0);
        }
        #endregion
        

        #region OnStartCancel
        [Test]
        public void UserInterfacePower_OnStartCancelPressed_LightOff()
        {
            //Act
            _powerButton.Press();
            _startCancelButton.Press();

            //Assert
            _light.Received().TurnOff();
        }

        [Test]
        public void UserInterfacePower_OnStartCancelPressed_DisplayClear()
        {
            //Act
            _powerButton.Press();
            _startCancelButton.Press();

            //Assert
            _display.Received().Clear();
        }

        [Test]
        public void UserInterfaceTime_OnStartCancelPressed_LightOn()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _light.Received().TurnOn();
        }

        
        [Test]
        public void UserInterfaceTime_OnStartCancelPressed_DisplayDidNotClear()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _display.DidNotReceive().Clear();
        }
        

        [Test]
        public void UserInterfaceTime_OnStartCancelPressed_StartCooking()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Assert
            _cookController.Received().StartCooking(50, 60);
        }

        [Test]
        public void UserInterfaceCooking_OnStartCancelPressed_StopCooking()
        {
            //Act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            //Assert
            _cookController.Received().Stop();
        }
        #endregion





    }
}
