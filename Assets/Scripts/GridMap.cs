using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridMap : MonoBehaviour {
    #region Singleton
    public static GridMap Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion
    public int xSize, ySize; // Độ rộng, dài của lưới
    public float side1 = 3.9f, side2 = 2.42f, angle = 107.8f; // số đo của hình thoi
    public float gap = 0.44f; // để cột sau thấp hơn cột trước 'gap' đơn vị
    public GameObject prefab;
    public static Node[,] nodes;

    public void GenerateMap() {
        ClearOldPrefabs();
        nodes = new Node[xSize + 1, ySize + 1];

        float angleRad = angle * Mathf.Deg2Rad;

        float xOffset = side1;
        float yOffset = side2 * Mathf.Sin(angleRad);
        float xOffsetY = side2 * Mathf.Cos(angleRad);

        for (int y = 0; y <= ySize; y++) { // hàng
            for (int x = 0; x <= xSize; x++) { //cột
                float posX = x * xOffset + y * xOffsetY;
                float posY = y * yOffset - x * gap;

                if (prefab != null) {
                    GameObject tile = Instantiate(prefab, new Vector2(posX, posY), Quaternion.identity, transform);
                    
                    tile.name = "Đất" + "(" + x + ", " + y + ")";
                    Node node = tile.GetComponent<Node>();
                    nodes[x, y] = node;

                    Renderer renderer = tile.GetComponentInChildren<SpriteRenderer>();
                    if (renderer != null) {
                        renderer.sortingOrder = -(int)(y * 1000) + x;
                    }
                }
            }
        }
        UpdateNeighborsNodes();
    }
    private void ClearOldPrefabs() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    //Hướng 6 ô xung quanh
    public static readonly Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0),  // Phải
        new Vector2(-1, 0), // Trái
        new Vector2(-1, -1), //Dưới-Trái
        new Vector2(1, 1), // Trên-Phải
        new Vector2(0, 1), // Trên-Trái
        new Vector2(0, -1) //Dưới-Phải
    };
    private void UpdateNeighborsNodes() {
        for (int y = 0; y <= ySize; y++) {
            for (int x = 0; x <= xSize; x++) {
                nodes[x, y].neighbors = new List<Node>();
                foreach (var dir in directions) {
                    int curX = x + (int)dir.x;
                    int curY = y + (int)dir.y;

                    if (curX < 0 || curY < 0) continue;
                    if (curX > xSize || curY > ySize) continue;

                    nodes[x, y].neighbors.Add(nodes[curX, curY]);
                }
            }
        }
    }
}
