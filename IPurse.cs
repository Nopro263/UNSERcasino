namespace UNSERcasino
{
    internal interface IPurse
    {
        public void Add(int amount);
        public void Remove(int amount);
        public bool CanRemove(int amount);
        public bool CanAdd(int amount);

        public int Balance { get; }
    }
}
