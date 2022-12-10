using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9;
public record Tail : IHead
{
    private Point _point;

    public Tail()
    {
        this.Point = new Point();
    }
    public Tail FollowHead(IHead head)
    {
        if (IsHeadCloseBy(head))
            return this;

        var distanceX = head.Point.X - this.Point.X;
        var distanceY = head.Point.Y - this.Point.Y;
        int newY = 0;
        int newX = 0;
        if (Math.Abs(distanceX) >= 1 && Math.Abs(distanceY) >= 1)
        {
            if (Math.Abs(distanceX) > 1)
            {
                newX = distanceX < 0 ? this.Point.X - 1 : this.Point.X + 1;
            }
            else
            {
                newX = head.Point.X;
            }

            if (Math.Abs(distanceY) > 1)
            {
                newY = distanceY < 0 ? this.Point.Y - 1 : this.Point.Y + 1;
            }
            else
            {
                newY = head.Point.Y;
            }

            return this with { Point = new Point(newY, newX) };
        }

        if (Math.Abs(distanceX) > 0)
        {
            newX = distanceX < 0 ? this.Point.X - 1 : this.Point.X + 1;
        }
        else
            newX = this.Point.X;

        if (Math.Abs(distanceY) > 0)
        {
            newY = distanceY < 0 ? this.Point.Y - 1 : this.Point.Y + 1;
        }
        else
            newY = this.Point.Y;
        return this with { Point = new Point(newY, newX) };
    }

    private bool IsHeadCloseBy(IHead head)
    {
        var distanceX = Math.Abs(head.Point.X - this.Point.X);
        var distanceY = Math.Abs(head.Point.Y - this.Point.Y);
        if (distanceX <= 1 && distanceY <= 1)
            return true;

        return false;
    }

    public List<Point> Steps { get; init; } = new List<Point>();
    public Point Point
    {
        get => this._point;
        init
        {
            _point = value;
            if (!this.Steps.Any(step => step == _point))
                this.Steps.Add(value);
        }
    }
}