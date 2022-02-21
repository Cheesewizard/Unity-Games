namespace Logger
{
    public class DebugLog : ILog
    {
        public void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}