using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PathFindingAStar : MonoBehaviour
{
    #region Singleton
    public static PathFindingAStar Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Show A* Algorithm Details
    [Header("List in Gameplay")]
    public List<Node> resultPath = new List<Node>();
    public List<Node> frontierNodes = new List<Node>();
    public List<Node> exploredNodes = new List<Node>();

    [Header("Nodes in Gameplay")]
    public Node player;
    public Node target;
    public Node currentNode;

    [Header("Time")]
    public float timeStepShowResult;
    public float timeStepFindSlow;
    float timeFind;
    #endregion
    public void StartFinding(Node startNode, Node endNode) {
        player = startNode;
        frontierNodes.Add(startNode);
        target = endNode;
        if(FindPath()) {
            BuildPath(endNode);
        } else {
            Debug.Log("Cant find Path!");
        }
    }
    bool FindPath() {
        timeFind = Time.time;
        if (frontierNodes.Count <= 0) {
            Debug.LogError("Zero Block to Move");
            return false;
        }
        if (frontierNodes.Contains(target)) {
            Debug.LogWarning("One Block");
            return true;
        }
        if (target == player) {
            Debug.LogWarning("Zero Block");
            return true;
        }
        while (currentNode != target) {
            currentNode = null;
            currentNode = BestNodeCostFrontier();

            if (currentNode == null) {
                Debug.LogError("Not found target");
                return false;
            }

            if (target == currentNode) {
                Debug.Log("Done");
                return true;
            }

            if (AddedExplored(currentNode)) {
                AddNeighborsFrontier(currentNode);
            } else {
                Debug.LogError("Bug Frontier to Explored Node");
            }
        }
        timeFind = Time.time - timeFind;
        return true;
    }
    Node BestNodeCostFrontier() {
        if (frontierNodes.Count == 0) return null;

        Node bestNode = frontierNodes[0];

        foreach (var node in frontierNodes) {
            if (node.FCost < bestNode.FCost || (node.FCost == bestNode.FCost && node.hCost < bestNode.hCost)) {
                bestNode = node;
            }
        }

        return bestNode;
    }
    void AddNeighborsFrontier(Node node) {
        foreach (var neighbor in node.neighbors) {
            if (neighbor.isObstacle == true) continue;
            if (frontierNodes.Contains(neighbor)) {
                CheckChangeNodePrevious(node, neighbor);
            }
            else {
                if (AddFrontier(neighbor)) neighbor.prevNode = node;
                neighbor.gCost = node.gCost + Vector2.Distance(node.transform.position, neighbor.transform.position);
            }
        }
    }
    void CheckChangeNodePrevious(Node current, Node neighbor) {
        float FCostNeighborWithCurrent = current.gCost + Vector2.Distance(current.transform.position, neighbor.transform.position);
        bool checkFCost = FCostNeighborWithCurrent < neighbor.FCost;
        bool checkHCost = (FCostNeighborWithCurrent == neighbor.FCost) && (current.hCost < neighbor.prevNode.hCost);
        if (checkFCost || checkHCost) {
            neighbor.prevNode = current;
            neighbor.gCost = FCostNeighborWithCurrent;
        }
    }
    bool AddFrontier(Node node) {
        if (node.isObstacle == true) return false;
        if (!frontierNodes.Contains(node) && !exploredNodes.Contains(node)) {
            frontierNodes.Add(node);
            return true;
        }
        return false;
    }
    bool AddedExplored(Node node) {
        if (!exploredNodes.Contains(node)) {
            exploredNodes.Add(node);
            frontierNodes.Remove(node);
            return true;
        }
        return false;
    }
    void BuildPath(Node endNode) {
        resultPath.Clear();
        Node current = endNode;

        while (current != null) {
            if (!current.isObstacle) {
                resultPath.Add(current);
            }
            current = current.prevNode;
        }

        resultPath.Reverse();
    }
}
