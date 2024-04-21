namespace UNSERcasino.UI
{
    internal class TimeUtil
    {
        //TODO: Fix memoryleak with many ENTER + ESC
        private static Dictionary<int, int> objTicksToSleep = new Dictionary<int, int>();
        public static void T(object obj) {
            if(objTicksToSleep.ContainsKey(obj.GetHashCode())) {
                int tts = objTicksToSleep[obj.GetHashCode()] - 1;
                if(tts > 0)
                {
                    objTicksToSleep[obj.GetHashCode()] = tts;
                    throw new SkipThisUpdateException();
                } else if(tts == 0)
                {
                    objTicksToSleep[obj.GetHashCode()] = -1;
                }
            }
        }
        public static void Sleep(int ticks, object obj)
        {
            objTicksToSleep[obj.GetHashCode()] = ticks;
        }

        public static void OnlyEvery(int ticks, object obj)
        {
            T(obj);
            Sleep(ticks, obj);
        }
    }
}
