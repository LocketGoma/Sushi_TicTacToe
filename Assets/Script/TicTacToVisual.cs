using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToVisual : MonoBehaviour
{
    public TicTacToManager ticTacToeManager;
    public GameObject[] NodeArray;
    public GameObject winnerPopup;
    public int gameMode = 3;            //3 = 3x3, 4 = 4x4
    public Sprite[] nodeSushiImage;
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

    public void MapUpdate() {        
        for (int x = 0; x < gameMode; x++) {
            for (int y = 0; y < gameMode; y++) {
                NodeArray[x * 3 + y].GetComponent<TicTacToeGraphicNode>().VisualNode(tictactoGameMap.BoardData[x, y]);                
            }
        }
    }

    public void NodeUpdate(int input, MapNode nodeType) {        
        tictactoGameMap.InputMove(input, nodeType);
        tictactoGameMap.RandomMove();
        MapUpdate();

        WinnerPopup(ticTacToeManager.GamePlayState);
    }
    public void WinnerPopup(GameState gameState) { 
        if (gameState == GameState.WinUser) {
            winnerPopup.GetComponent<Image>().sprite = nodeSushiImage[1];
        } else if (gameState == GameState.WinAI) {
            winnerPopup.GetComponent<Image>().sprite = nodeSushiImage[2];
        } else {
            winnerPopup.GetComponent<Image>().sprite = nodeSushiImage[0];
        }
    
    }


}
