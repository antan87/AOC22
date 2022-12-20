namespace Day11
{
    public record Monkey(int Index, Func<int, int> OperationFunc, Func<int, bool> IsDivisibleFunc, Func<bool, int> ThrowFunc)
    {
        public List<int> Items { get; set; } = new List<int>();

        public IEnumerable<(int index, int result)> Throws(bool divide = true)
        {
            foreach (var item in Items)
            {
                var operationResult = OperationFunc(item);
                int output = 0;
                if (divide)
                    output = (int)Math.Round((decimal)operationResult / 3, 10, MidpointRounding.ToEven);
                else
                    output = operationResult;

                var index = ThrowFunc(IsDivisibleFunc(output));
                yield return (index, output);
                Inspect++;
            }
            this.Items.Clear();
        }

        public int Inspect { get; private set; }
    }
}