using UnityEngine;

public enum CellType
{
    Empty,
    Block,
    Gear
}

[CreateAssetMenu(fileName = "NewLevelLayout", menuName = "Game/Level Layout")]
public class LevelLayout : ScriptableObject
{
    public int width = 6;
    public int height = 6;
    public int coin = 0;
    public int Count = 0;
    public CellType[] cellTypes;
    public Direction[] directions;
}