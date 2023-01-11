using Common.Parsing;
using Day12;

Console.WriteLine("Day 12");
Console.WriteLine();

string inputFile = @"Day12.Input.txt";
var input = await ParseHelper.GetInput(inputFile);

var grid = ParseHelper.Parse(new GridParser(), input);

var stepsPart1 = BFS(grid, new GridItem(grid.Positions.Single(pos => pos.Starting)));
Console.WriteLine("Part 1:");
Console.WriteLine(stepsPart1);
Console.WriteLine();

Console.WriteLine("Part 2:");
var stepsPart2 = grid.Positions.Where(pos => pos.Elevation == 1).Select(pos => BFS(grid, new GridItem(pos))).Where(res => res != null).Min();
Console.WriteLine(stepsPart2);

int? BFS(Grid grid, GridItem currentItem)
{
    var visited = new List<GridItem>();
    var queue = new Queue<GridItem>();
    GridItem? previousItem = null;
    int steps = 0;
    queue.Enqueue(currentItem);
    int iteration = currentItem.Iteration;
    while (queue.Count > 0)
    {
        currentItem = queue.Dequeue();

        if (iteration != currentItem.Iteration)
        {
            iteration = currentItem.Iteration;
            steps++;
        }

        visited.Add(currentItem);

        var neigbhours = currentItem.GetNeighbours(previousItem?.Position, grid);
        foreach (Position neighbor in neigbhours)
        {
            if (neighbor.Destination)
                return ++steps;

            if (!visited.Any(vis => vis.Position == neighbor) && !queue.Any(x => x.Position == neighbor))
            {
                var enquedItem = new GridItem(neighbor) { Iteration = iteration + 1 };
                queue.Enqueue(enquedItem);
            }
        }

        previousItem = currentItem;
    }

    return null;
}

record GridItem
{
    public GridItem(Position position)
    {
        Position = position;
    }

    public Position Position { get; }

    public int Iteration { get; init; }
    public IEnumerable<Position> GetNeighbours(Position? previous, Grid grid)
    {
        var neigbhours = grid.Positions.Where(pos => previous != pos && ((pos.Coordinate.X == Position.Coordinate.X && (pos.Coordinate.Y == Position.Coordinate.Y + 1 || pos.Coordinate.Y == Position.Coordinate.Y - 1))
                                                  || (pos.Coordinate.Y == Position.Coordinate.Y && (pos.Coordinate.X == Position.Coordinate.X + 1 || pos.Coordinate.X == Position.Coordinate.X - 1)))).ToList();

        return neigbhours.Where(pos => ((pos.Elevation <= Position.Elevation) || (pos.Elevation == Position.Elevation + 1))).ToList();
    }
}

record struct Coordinate(int Y, int X);

record Position(Coordinate Coordinate, int Elevation, bool Starting = false, bool Destination = false);

record Grid(IEnumerable<Position> Positions);