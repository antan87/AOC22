namespace Day2
{
    internal static class GameEvaluator
    {
        public static GameOutcomeKind Play(ShapeKind opponentShape, ShapeKind playerShape)
        {
            return GetGameOutcome(opponentShape, playerShape);
        }

        public static GameOutcomeKind TranslateGameOutcome(string outcomeLetter)
        {
            switch (outcomeLetter)
            {
                case "X":
                    return GameOutcomeKind.Lost;

                case "Y":
                    return GameOutcomeKind.Draw;

                default:
                    return GameOutcomeKind.Win;
            }
        }

        private static GameOutcomeKind GetGameOutcome(ShapeKind opponentShape, ShapeKind playerShape)
        {
            switch (opponentShape)
            {
                case ShapeKind.Scissor when playerShape == ShapeKind.Rock:
                    return GameOutcomeKind.Win;

                case ShapeKind.Rock when playerShape == ShapeKind.Papper:
                    return GameOutcomeKind.Win;

                case ShapeKind.Papper when playerShape == ShapeKind.Scissor:
                    return GameOutcomeKind.Win;

                case ShapeKind.Scissor when playerShape == ShapeKind.Scissor:
                    return GameOutcomeKind.Draw;

                case ShapeKind.Rock when playerShape == ShapeKind.Rock:
                    return GameOutcomeKind.Draw;

                case ShapeKind.Papper when playerShape == ShapeKind.Papper:
                    return GameOutcomeKind.Draw;

                default:
                    return GameOutcomeKind.Lost;
            }
        }
    }
}