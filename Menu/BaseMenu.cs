namespace UNSERcasino
{
    internal abstract class BaseMenu
    {
        public virtual void enter(bool isReenter) { }
        public virtual void exit(bool isHide) { }
        public virtual void destroy() { }
        public virtual string[] update(TimeSpan s) { return update(s, null); }
        public abstract string[] update(TimeSpan s, string? i);

        public static void preventBug()
        {
            //Console.WriteLine("Test1234567891011121314151617181920"); // just dont ask
            Console.WriteLine("a"); // just dont ask WHY DOES IT WORK WITH a BUT NOT WITH #!!!!!
        }
    }
}
