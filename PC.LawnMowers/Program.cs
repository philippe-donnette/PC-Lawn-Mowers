using Microsoft.Practices.Unity;
using PC.LawnMowers.Controllers;
using PC.LawnMowers.Validators;
using System;

namespace PC.LawnMowers
{
    class Program
    {
        private static IUnityContainer container;
        private static IMainController controller;

        static void Main(string[] args)
        {
            container = RegisterComponents();
            controller = container.Resolve<IMainController>();
            
            string input = "5 5" + Environment.NewLine;
            input += "1 2 N" + Environment.NewLine;
            input += "LMLMLMLMM" + Environment.NewLine;
            input += "3 3 E" + Environment.NewLine;
            input += "MMRMMRMRRM";
            
            try
            {
                string output = controller.Start(input);
                Console.WriteLine(output);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.ReadLine();
        }

        private static IUnityContainer RegisterComponents()
        {
            container = new UnityContainer();
            container.RegisterType<IRuleController, RuleController>();
            container.RegisterType<IMowerController, MowerController>();
            container.RegisterType<IMainController, MainController>();
            container.RegisterType<IInputValidator, InputValidator>();
            container.RegisterType<IInputController, InputController>();
            return container;
        }
    }
}
