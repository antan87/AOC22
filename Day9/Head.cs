namespace Day9;
public record Head(Point Point) : IHead
{
    public Head Move(MovmentKind movment, int steps)
    {
        switch (movment)
        {
            case MovmentKind.Down:
                return new Head(this.Point with { Y = this.Point.Y - steps });

            case MovmentKind.Up:
                return new Head(this.Point with { Y = this.Point.Y + steps });

            case MovmentKind.Left:
                return new Head(this.Point with { X = this.Point.X - steps });

            case MovmentKind.Right:
                return new Head(this.Point with { X = this.Point.X + steps });

            default:
                throw new NotImplementedException();
        }
    }
}