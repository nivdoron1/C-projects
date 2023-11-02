using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delagates
{
    public class MenuManagerDelagates
    {
        private readonly ActionExecutor actionExecutor = new ActionExecutor();

        public void ShowMainMenu()
        {
            while (true)
            {
                string menuFormat = "-----------------------" + Environment.NewLine +
                                    "1 -> Show Date/Time" + Environment.NewLine +
                                    "2 -> Version and Capitals" + Environment.NewLine +
                                    "0 -> Exit" + Environment.NewLine +
                                    "-----------------------" + Environment.NewLine +
                                    "Enter your request: (1-2 or '0' to Exit)";

                Console.WriteLine(menuFormat);
                string choice = Console.ReadLine();

                if (!int.TryParse(choice, out int parsedChoice) || parsedChoice < 0 || parsedChoice > 2)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (parsedChoice)
                {
                    case 1:
                        ShowSubMenu("Time/Date Show", new Dictionary<string, Action>
                        {
                            { "Time Show", () => actionExecutor.ExecuteAction("Time Show") },
                            { "Date Show", () => actionExecutor.ExecuteAction("Date Show") }
                        });
                        break;
                    case 2:
                        ShowSubMenu("Capitals and Version", new Dictionary<string, Action>
                        {
                            { "Show Version", () => actionExecutor.ExecuteAction("Show Version") },
                            { "Count Capitals", () => actionExecutor.ExecuteAction("Count Capitals") }
                        });
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        return;
                }
            }
        }


        public static void ShowSubMenu(string i_title, Dictionary<string, Action> i_options)
        {
            while (true)
            {
                Console.WriteLine(Environment.NewLine + "** " + i_title + " **");
                StringBuilder menuBuilder = new StringBuilder();
                menuBuilder.AppendLine("-----------------------");

                int index = 1;
                foreach (var key in i_options.Keys)
                {
                    menuBuilder.AppendLine(string.Format("{0} -> {1}", index, key));
                    index++;
                }

                menuBuilder.AppendLine("0 -> Exit" + Environment.NewLine +
                                    "-----------------------" + Environment.NewLine +
                                    "Enter your request: (1-2 or '0' to Exit)");

                string menuFormat = menuBuilder.ToString();
                Console.Write(menuFormat);
                string choiceStr = Console.ReadLine();

                if (!int.TryParse(choiceStr, out int choice) || choice < 0 || choice > i_options.Count)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                if (choice == 0)
                {
                    return;
                }

                var selectedOption = new List<string>(i_options.Keys)[choice - 1];
                i_options[selectedOption]?.Invoke();
            }
        }
    }
}
