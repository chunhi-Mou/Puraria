using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum SoilType {
    Good, //Đất tốt
    Cracked, //Đất nứt nẻ
    Polluted, // Đất Ô nhiễm
    Radioactive // Đất phóng xạ
}
public class Node : MonoBehaviour {
    [SerializeField] TileData tileData;
    public bool isObstacle;
    protected int dirtID = 0;

    public float gCost;
    public float hCost;
    public float FCost => gCost + hCost;

    public Node prevNode;
    public List<Node> neighbors = new List<Node>();

    protected SpriteRenderer spriteRenderer;

    public virtual void SetComponents() {
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.spriteRenderer.sprite = tileData.sprite;
        this.dirtID = tileData.dirtType;
        SetIsObstacle();
    }
    public void SetIsObstacle() {
        if (dirtID == (int)SoilType.Good) { 
            this.isObstacle = false;
        } else {
            this.isObstacle= true;
        }
    } 
}
