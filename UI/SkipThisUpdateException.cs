namespace UNSERcasino.UI
{
    internal class SkipThisUpdateException : Exception // Throw in Update of IUpdateable to skip this update. Thrown by TimeUtil.
    {
    }
}
