using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData")]
public class TileData : ScriptableObject {
    public int dirtType = 0;
    public bool isUnlocked = false;
    public Sprite sprite;
}
