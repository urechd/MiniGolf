using System.Collections.Generic;

public static class GameState
{
    private static List<int> LevelsCompleted = new List<int>();

    public static bool IsLevelComplete(int levelNumber)
    {
        return LevelsCompleted.Contains(levelNumber);
    }

    public static void SetLevelComplete(int levelNumber)
    {
        if (!LevelsCompleted.Contains(levelNumber))
        {
            LevelsCompleted.Add(levelNumber);
        }
    }
}