using System.Collections.Generic;
using UnityEngine;

public class LevelDatabase : MonoBehaviour
{
    public List<LevelLayout> allLevels;

    public LevelLayout GetLevel(int index)
    {
        if (index >= 0 && index < allLevels.Count)
            return allLevels[index];

        return null;
    }
}