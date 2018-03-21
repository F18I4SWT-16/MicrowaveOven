using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace SimpleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup all the objects, 
            var door = new Door();
            var powerButton = new Button();
            var timeButton = new Button();
            var startCancelButton = new Button();
            var output = new Output();
            var light = new Light(output);
            var display = new Display(output);
            var timer = new Timer();
            var powerTube = new PowerTube(output);
            var cookController = new CookController(timer, display, powerTube);
            var UI = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);

            // Simulate user activities
            door.Open();
            door.Close();
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            Thread.Sleep(1000);
            
            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();

        }
    }
}
