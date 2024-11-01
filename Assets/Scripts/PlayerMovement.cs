using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] Transform startPlayerPoint;
    [SerializeField] Transform endPlayerPoint;
    [SerializeField] float moveSpeed = 2f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            PlayerMove();
        }
    }

    private void PlayerMove() {
        Node startNode = startPlayerPoint.GetComponent<Node>();
        Node endNode = endPlayerPoint.GetComponent<Node>();
        if (PathFindingAStar.Instance == null) {
            Debug.LogError("PathFindingAStar instance is not initialized.");
            return;
        }

        PathFindingAStar.Instance.StartFinding(startNode, endNode);
        List<Node> path = PathFindingAStar.Instance.resultPath;

        if (path != null && path.Count > 1) {
            transform.position = startNode.transform.position;
            StartCoroutine(FollowPath(path));
        }
    }

    private IEnumerator FollowPath(List<Node> path) {
        for (int i = 1; i < path.Count; i++) {
            Node currNode = path[i];
            Transform target = currNode.transform;

            while (Vector3.Distance(transform.position, target.position) > 0.1f) {
                transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = target.position;
        }
    }
}
