using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum TileType {
    Good, //Đất tốt
    Cracked, //Đất nứt nẻ
    Polluted, // Đất Ô nhiễm
    Radioactive // Đất phóng xạ
}
public class Tile : MonoBehaviour {
    protected int dirtType = 0;
    protected bool isWalkable = false;
    protected SpriteRenderer spriteRenderer;
    protected virtual void Awake() {
        this.SetSprite();
    }
    protected virtual void SetSprite() {
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
