namespace UNSERcasino
{
    internal class BaseMenu
    {
        public virtual void enter(bool isReenter) { }
        public virtual void exit() { }
        public virtual void destroy() { }
        public virtual string[] update(string[] prev) { return prev; }
    }
}
