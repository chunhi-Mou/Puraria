using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridMap))]
public class GridMapEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GridMap gridMap = (GridMap)target;
        if (GUILayout.Button("Generate Map")) {
            gridMap.GenerateMap();
        }
    }
}
