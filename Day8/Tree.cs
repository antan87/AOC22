namespace Day8;

public record Tree(int Height, Point Point, int EdgeX, int EdgeY)
{
    public bool IsVisible(IEnumerable<Tree> trees)
    {
        Mode mode = Mode.IsVisable;
        if (this.IsEdge(mode))
            return true;

        Func<Point, Point> traverserLeft = (point) => new Point(point.X - 1, point.Y);
        int countLeft = 0;
        if (FindNextTree(traverserLeft, trees).Traverse(this.Height, traverserLeft, trees, ref countLeft, mode))
            return true;

        Func<Point, Point> traverserRight = (point) => new Point(point.X + 1, point.Y);
        int countRight = 0;
        if (FindNextTree(traverserRight, trees).Traverse(this.Height, traverserRight, trees, ref countRight, mode))
            return true;

        Func<Point, Point> traverserDown = (point) => new Point(point.X, point.Y + 1);
        int countDown = 0;
        if (FindNextTree(traverserDown, trees).Traverse(this.Height, traverserDown, trees, ref countDown, mode))
            return true;

        Func<Point, Point> traverserUp = (point) => new Point(point.X, point.Y - 1);
        int countUp = 0;
        if (FindNextTree(traverserUp, trees).Traverse(this.Height, traverserUp, trees, ref countUp, mode))
            return true;

        return false;
    }

    public int GetScenicScore(IEnumerable<Tree> trees)
    {
        Mode mode = Mode.Scenic;
        Func<Point, Point> traverserLeft = (point) => new Point(point.X - 1, point.Y);
        int countLeft = 0;
        FindNextTree(traverserLeft, trees)?.Traverse(this.Height, traverserLeft, trees, ref countLeft, mode);
        Func<Point, Point> traverserRight = (point) => new Point(point.X + 1, point.Y);
        int countRight = 0;
        FindNextTree(traverserRight, trees)?.Traverse(this.Height, traverserRight, trees, ref countRight, mode);

        Func<Point, Point> traverserDown = (point) => new Point(point.X, point.Y + 1);
        int countDown = 0;
        FindNextTree(traverserDown, trees)?.Traverse(this.Height, traverserDown, trees, ref countDown, mode);

        Func<Point, Point> traverserUp = (point) => new Point(point.X, point.Y - 1);
        int countUp = 0;
        FindNextTree(traverserUp, trees)?.Traverse(this.Height, traverserUp, trees, ref countUp, mode);

        return countUp * countRight * countLeft * countDown;
    }

    public bool Traverse(int height, Func<Point, Point> traverser, IEnumerable<Tree> trees, ref int count, Mode mode)
    {
        count++;

        if (this.IsEdge(mode, traverser) && height > this.Height)
        {
            return true;
        }
        if (height <= this.Height)
            return false;

        Tree? nextTree = FindNextTree(traverser, trees);
        if (nextTree == null)
            return true;

        return nextTree.Traverse(height, traverser, trees, ref count, mode);
    }

    private Tree? FindNextTree(Func<Point, Point> traverser, IEnumerable<Tree> trees)
    {
        var nextPoint = traverser(this.Point);

        var nextTree = trees.FirstOrDefault(tree => tree.Point == nextPoint);
        return nextTree;
    }

    public bool IsEdge(Mode mode, Func<Point, Point>? traverser = null)
    {
        var directionPoint = traverser(this.Point);
        if (this.Point.Y == 0 && (mode == Mode.IsVisable || directionPoint.Y == -1))
            return true;

        if (this.Point.X == 0 && (mode == Mode.IsVisable || directionPoint.X == -1))
            return true;

        if (this.Point.Y == EdgeY && (mode == Mode.IsVisable || directionPoint.Y == EdgeY + 1))
            return true;

        if (this.Point.X == EdgeX && (mode == Mode.IsVisable || directionPoint.X == EdgeX + 1))
            return true;

        return false;
    }

    public enum Mode
    {
        IsVisable,
        Scenic
    }
}