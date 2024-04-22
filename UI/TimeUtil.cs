namespace UNSERcasino.UI
{
    internal class TimeUtil
    {
        public static double Tps {get; set; }
        //TODO: Fix memoryleak with many ENTER + ESC
        private static Dictionary<int, DateTime> objTicksToSleep = new Dictionary<int, DateTime>();
        public static void T(object obj) {
            if(objTicksToSleep.ContainsKey(obj.GetHashCode())) {
                if(DateTime.Now < objTicksToSleep[obj.GetHashCode()])
                {
                    throw new SkipThisUpdateException();
                } else
                {
                    objTicksToSleep.Remove(obj.GetHashCode());
                }
            }
        }
        public static void Sleep(double seconds, object obj)
        {
            objTicksToSleep[obj.GetHashCode()] = DateTime.Now.AddSeconds(seconds);
        }

        public static void OnlyEvery(double ticks, object obj)
        {
            T(obj);
            Sleep(ticks, obj);
        }
    }
}
