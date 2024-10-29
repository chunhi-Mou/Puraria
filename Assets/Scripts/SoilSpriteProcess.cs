using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SoilSpriteProcess : Tile {
    [SerializeField] TileData tileData;
    protected override void Awake() {
        if (tileData == null) {
            Debug.LogWarning("No titleData");
            return;
        }
        base.Awake();
    }
    protected override void SetSprite() {
        base.SetSprite();
        this.dirtType = tileData.dirtType;
        this.spriteRenderer.sprite = tileData.sprite;
        Debug.Log(dirtType);
    }
}
