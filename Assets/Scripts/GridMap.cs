using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridMap : MonoBehaviour {
    public int xSize, ySize; // Độ rộng, dài của lưới
    public float side1 = 3.9f, side2 = 2.42f, angle = 107.8f; // số đo của hình thoi
    public float gap = 0.44f; // để cột sau thấp hơn cột trước 'gap' đơn vị
    public GameObject prefab;

    private Vector2[,] vertices;

    public void GenerateMap() {
        ClearOldPrefabs();
        vertices = new Vector2[(xSize + 1), (ySize + 1)];
        float angleRad = angle * Mathf.Deg2Rad;

        float xOffset = side1;
        float yOffset = side2 * Mathf.Sin(angleRad);
        float xOffsetY = side2 * Mathf.Cos(angleRad);

        for (int x = 0; x <= xSize; x++) {
            for (int y = 0; y <= ySize; y++) {
                float posX = x * xOffset + y * xOffsetY;
                float posY = y * yOffset - x * gap;
                vertices[x, y] = new Vector2(posX, posY);
                if (prefab != null) {
                    GameObject tile = Instantiate(prefab, vertices[x, y], Quaternion.identity, transform);
                    
                    Renderer renderer = tile.GetComponentInChildren<SpriteRenderer>();
                    if (renderer != null) {
                        renderer.sortingOrder = -(int)(y * 1000) + x;
                    }
                }
            }
        }
    }
    private void ClearOldPrefabs() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            if (Application.isPlaying)
                Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
