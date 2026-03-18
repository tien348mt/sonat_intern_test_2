using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelLayout))]
public class LevelLayoutEditor : Editor
{
    string[] icons = { " ", "■", "⚙" };
    string[] arrows = { "↑", "↓", "←", "→"};

    public override void OnInspectorGUI()
    {
        LevelLayout layout = (LevelLayout)target;

        layout.width = EditorGUILayout.IntField("Width", layout.width);
        layout.height = EditorGUILayout.IntField("Height", layout.height);
        layout.coin = EditorGUILayout.IntField("Coin", layout.coin);
        layout.Count = EditorGUILayout.IntField("Count", layout.Count);

        int size = layout.width * layout.height;

        if (layout.cellTypes == null || layout.cellTypes.Length != size)
            layout.cellTypes = new CellType[size];

        if (layout.directions == null || layout.directions.Length != size)
            layout.directions = new Direction[size];

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Màn chơi (Click để đổi loại / xoay):");

        for (int y = layout.height - 1; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int x = 0; x < layout.width; x++)
            {
                int index = x + y * layout.width;

                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.fixedWidth = 50;
                style.fixedHeight = 50;
                style.fontSize = 18;

                CellType type = layout.cellTypes[index];

                string label = icons[(int)type];

                if (type == CellType.Block)
                {
                    label += "\n" + arrows[(int)layout.directions[index]];
                }

                if (GUILayout.Button(label, style))
                {
                    if (type == CellType.Block && Event.current.shift)
                    {
                        layout.directions[index] = (Direction)(((int)layout.directions[index] + 1) % 4);
                    }
                    else
                    {
                        if (type == CellType.Empty)
                        {
                            layout.cellTypes[index] = CellType.Block;
                            layout.directions[index] = Direction.Right;
                        }
                        else if (type == CellType.Block)
                        {
                            layout.cellTypes[index] = CellType.Gear;
                        }
                        else if (type == CellType.Gear)
                        {
                            layout.cellTypes[index] = CellType.Empty;
                        }
                    }
                }
                if (Event.current.type == EventType.ContextClick &&
                    GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    layout.cellTypes[index] = CellType.Gear;
                    Event.current.Use();
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(layout);
        }
    }
}