namespace Ex04_Interfaces.Menus
{
    public class OptionsOperator : OptionsInterface
    {
        public void ShowDate()
        {
            new ShowDate().Execute();
        }

        public void ShowTime()
        {
            new ShowTime().Execute();
        }

        public void ShowVersion()
        {
            new ShowVersion().Execute();
        }

        public void CountCapitals()
        {
            new CountCapitals().Execute();
        }
    }

}
