using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToVisual : MonoBehaviour
{
    public GameObject ticTacToeManager;
    public GameObject[] NodeArray;    
    public int gameMode = 3;            //3 = 3x3, 4 = 4x4
    private TicTacToMap tictactoGameMap;

    // Start is called before the first frame update
    void Start()
    {
        tictactoGameMap = ticTacToeManager.GetComponent<TicTacToMap>();
        tictactoGameMap.GameMode = gameMode;

        for (int i = 0; i < NodeArray.Length; i++) {
            NodeArray[i].GetComponent<TicTacToeGraphicNode>().NodeNumber = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MapUpdate();
    }

    void MapUpdate() {
        for (int x = 0; x < gameMode; x++) {
            for (int y = 0; y < gameMode; y++) {
                NodeArray[x * 3 + y].GetComponent<TicTacToeGraphicNode>().VisualNode(tictactoGameMap.BoardData[x, y]);                
            }
        }
    }

    public void NodeUpdate(int input, MapNode nodeType) {
        tictactoGameMap.BoardData[input / gameMode, input % gameMode] = nodeType;
        tictactoGameMap.RandomMove();
    }
}
