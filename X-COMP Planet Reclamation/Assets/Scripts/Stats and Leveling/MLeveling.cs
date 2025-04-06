using UnityEngine;

public static class MLeveling
{

    static int Level2 = 75;
    static int Level3 = 200;
    static int Level4 = 450;
    static int Level5 = 1000;

    static int Level2Points = 4;
    static int Level3Points = 6;
    static int Level4Points = 4;
    static int Level5Points = 6;

    public static int GetRequiredEXP(int currentLevel)
    {
        switch(currentLevel)
        {
            case 1:
                return Level2;
            case 2: 
                return Level3;
            case 3:
                return Level4;
            case 4: 
                return Level5;
            case 5:
                return -1;
            default:
                Debug.LogError("Player Level is outside of the range (1 - 5)");
                return -1;
        }
    }

    public static bool CanLevelUp(int currentLevel, int totalEXP)
    {
        return (totalEXP >= GetRequiredEXP(currentLevel));
    }

    public static int GetLevelingPoints(int level)
    {
        switch (level)
        {
            case 2:
                return Level2Points;
            case 3:
                return Level3Points;
            case 4:
                return Level4Points;
            case 5:
                return Level5Points;
            default:
                Debug.LogError("Player Level is outside of the range (2 - 5)");
                return -1;
        }

    }
}
