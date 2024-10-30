using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoilSpriteProcess))]
public class SoilSpriteProcessEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        SoilSpriteProcess soilSpriteProcess = (SoilSpriteProcess)target;
        if (GUILayout.Button("Update Soil")) {
            soilSpriteProcess.SetSpriteOnEditor();
        }
    }
}
