using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal static class ShapeTranslator
    {
        public static ShapeKind GetPlayerShape(this string shapeLetter)
        {
            switch (shapeLetter)
            {
                case "Y":
                    return ShapeKind.Papper;

                case "X":
                    return ShapeKind.Rock;

                default:
                    return ShapeKind.Scissor;
            }
        }

        public static ShapeKind GetPlayerShapeBasedOnOutcome(this GameOutcomeKind outcome, ShapeKind opponentShape)
        {
            switch (outcome)
            {
                case GameOutcomeKind.Win when opponentShape == ShapeKind.Rock:
                    return ShapeKind.Papper;

                case GameOutcomeKind.Win when opponentShape == ShapeKind.Scissor:
                    return ShapeKind.Rock;

                case GameOutcomeKind.Win when opponentShape == ShapeKind.Papper:
                    return ShapeKind.Scissor;

                case GameOutcomeKind.Draw when opponentShape == ShapeKind.Papper:
                    return ShapeKind.Papper;

                case GameOutcomeKind.Draw when opponentShape == ShapeKind.Scissor:
                    return ShapeKind.Scissor;

                case GameOutcomeKind.Draw when opponentShape == ShapeKind.Rock:
                    return ShapeKind.Rock;

                case GameOutcomeKind.Lost when opponentShape == ShapeKind.Rock:
                    return ShapeKind.Scissor;

                case GameOutcomeKind.Lost when opponentShape == ShapeKind.Scissor:
                    return ShapeKind.Papper;

                case GameOutcomeKind.Lost when opponentShape == ShapeKind.Papper:
                    return ShapeKind.Rock;

                default:
                    throw new NotImplementedException();
            }
        }

        public static ShapeKind GetOpponentShape(this string shapeLetter)
        {
            switch (shapeLetter)
            {
                case "A":
                    return ShapeKind.Rock;

                case "B":
                    return ShapeKind.Papper;

                default:
                    return ShapeKind.Scissor;
            }
        }
    }
}