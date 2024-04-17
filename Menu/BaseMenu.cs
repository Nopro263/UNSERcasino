namespace UNSERcasino
{
    internal class BaseMenu
    {
        public virtual void enter(bool isReenter) { }
        public virtual void exit(bool isHide) { }
        public virtual void destroy() { }
        public virtual string[] update(TimeSpan s) { return update(s, null); }
        public virtual string[] update(TimeSpan s, string? i) { return new string[0]; }

        protected void preventBug()
        {
            Console.WriteLine("Test1234567891011121314151617181920"); // just dont ask
        }
    }
}
