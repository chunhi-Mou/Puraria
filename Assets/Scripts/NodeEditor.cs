using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Node node = (Node)target;
        if (GUILayout.Button("Update Node")) {
            node.SetComponents();
        }
    }
}
