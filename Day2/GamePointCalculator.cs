using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal static class GamePointCalculator
    {
        public static int CalculatePoints(ShapeKind shapeKind, GameOutcomeKind gameOutCome)
        {
            return GetShapePoints(shapeKind) + GameOutComePoints(gameOutCome);
        }

        private static int GetShapePoints(ShapeKind shapeKind)
        {
            switch (shapeKind)
            {
                case ShapeKind.Rock:
                    return 1;

                case ShapeKind.Papper:
                    return 2;

                default:
                    return 3;
            }
        }

        private static int GameOutComePoints(GameOutcomeKind gameOutCome)
        {
            switch (gameOutCome)
            {
                case GameOutcomeKind.Win:
                    return 6;

                case GameOutcomeKind.Draw:
                    return 3;

                default:
                    return 0;
            }
        }
    }
}