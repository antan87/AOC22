namespace Day10
{
    public record Spirit(int Middle)
    {
        public bool IsInRange(int value)
        {
            return Middle == value || Middle + 1 == value || Middle - 1 == value;
        }
    }
}