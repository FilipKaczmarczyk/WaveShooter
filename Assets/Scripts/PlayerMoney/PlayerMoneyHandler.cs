using Events;

namespace PlayerMoney
{
    public static class PlayerMoneyHandler
    {
        public static event EventManager.VoidEventHandler RefreshMoney;
        public static int CurrentPlayerMoney { get; private set; }

        public static void AddMoney(int value)
        {
            CurrentPlayerMoney += value;
        
            RefreshMoney?.Invoke();
        }

        public static void DecreaseMoney(int value)
        {
            CurrentPlayerMoney -= value;
        
            RefreshMoney?.Invoke();
        }

        public static void SetMoney(int value)
        {
            CurrentPlayerMoney = value;
        
            RefreshMoney?.Invoke();
        }
    }
}
