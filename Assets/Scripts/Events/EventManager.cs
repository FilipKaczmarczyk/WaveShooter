using Input;

namespace Events
{
    public static class EventManager
    {
        public delegate void VoidEventHandler();
        
        public delegate void ActionMapEventHandler(GlobalInputManager.ActionMaps actionMap);
    }
}
