using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delagates
{
    public class ActionExecutor
    {
            private readonly ShowTimeAction showTimeAction = new ShowTimeAction();
            private readonly ShowDateAction showDateAction = new ShowDateAction();
            private readonly ShowVersionAction showVersionAction = new ShowVersionAction();
            private readonly CountCapitalsAction countCapitalsAction = new CountCapitalsAction();

            private readonly Dictionary<string, Action> actions;

            public ActionExecutor()
            {
                actions = new Dictionary<string, Action>
            {
                { "Time Show", showTimeAction.ShowTime },
                { "Date Show", showDateAction.ShowDate },
                { "Show Version", showVersionAction.ShowVersion },
                { "Count Capitals", countCapitalsAction.CountCapitals }
            };
            }

            public void ExecuteAction(string actionName)
            {
                if (actions.TryGetValue(actionName, out Action action))
                {
                    action();
                }
                else
                {
                    Console.WriteLine("Invalid action name.");
                }
            }
    }
}
