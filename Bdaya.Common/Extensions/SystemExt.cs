namespace System
{
    public static class SystemExt
    {
        public static int CeilDiv(this int first, int second)
        {
            return (int)Math.Ceiling((double)first / second);
        }
    }
}
