using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGraphicNode : MonoBehaviour
{
    public Sprite [] nodeSushiImage;

    public TicTacToVisual ticTacToVisual;
    [SerializeField] private MapNode sushiNode;
    [SerializeField] private bool isSelected = false;
    [SerializeField] private int nodeNumber;

    public int NodeNumber { get { return nodeNumber; } set { nodeNumber = value; } }

    public MapNode SushiNode { get { return sushiNode; } set { sushiNode = value; } }

    public void Start() {
        ticTacToVisual = transform.parent.gameObject.GetComponent<TicTacToVisual>();
    }
    public void VisualNode(MapNode node) {
        if(node == MapNode.None) {
            gameObject.GetComponent<Image>().sprite = nodeSushiImage[0];
        } else if (node == MapNode.User) {
            gameObject.GetComponent<Image>().sprite = nodeSushiImage[1];
        } else if (node == MapNode.AI) {
            gameObject.GetComponent<Image>().sprite = nodeSushiImage[2];
        }
        sushiNode = node;
    }
    public void ClickNode() {
        Debug.Log(sushiNode);
        if (sushiNode == MapNode.None) {            
            ticTacToVisual.NodeUpdate(nodeNumber,MapNode.User);
        }  /* else if (sushiNode != MapNode.None) {
            isSelected = true;
        }*/
    }
}
