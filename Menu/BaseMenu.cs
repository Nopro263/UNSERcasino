namespace UNSERcasino
{
    internal class BaseMenu
    {
        public virtual void enter(bool isReenter) { }
        public virtual void exit(bool isHide) { }
        public virtual void destroy() { }
        public virtual string[] update() { return new string[0]; }
    }
}
