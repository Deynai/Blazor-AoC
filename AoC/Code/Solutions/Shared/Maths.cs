namespace AoC.Code.Solutions.Shared
{
    public static class Maths
    {
        public static int Min(int a, int b)
        {
            return a <= b ? a : b;
        }
        
        public static int Max(int a, int b)
        {
            return a >= b ? a : b;
        }
    }
}
