using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToVisual : MonoBehaviour
{
    public TicTacToManager ticTacToeManager;
    public GameObject[] NodeArray;
    public GameObject baseNode;
    public GameObject winnerPopup;
    
    public Sprite[] nodeSushiImage;
    private TicTacToMap tictactoGameMap;

    [SerializeField] private int gameMode = 4;            //3 = 3x3, 4 = 4x4
    public int GameMode { get { return gameMode; } set { gameMode = value; } }

    // Start is called before the first frame update
    void Start()
    {
        GameMode = ticTacToeManager.GameMode;
        NodeArray = new GameObject[gameMode * gameMode];        
        tictactoGameMap = ticTacToeManager.GetComponent<TicTacToMap>();        
        //Debug.Log("NodeArray.Length : " + NodeArray.Length);
        


        for (int i = 0; i < gameMode * gameMode ; i++) {
            Instantiate(baseNode).transform.parent = gameObject.transform;

            NodeArray[i] = transform.GetChild(i).gameObject;

            NodeArray[i].GetComponent<TicTacToeGraphicNode>().NodeNumber = i;
        }

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<GridLayoutGroup>().cellSize.x * gameMode, gameObject.GetComponent<GridLayoutGroup>().cellSize.y * gameMode);

    }


    public void MapUpdate() {        
        for (int x = 0; x < gameMode; x++) {
            for (int y = 0; y < gameMode; y++) {
                NodeArray[x * gameMode + y].GetComponent<TicTacToeGraphicNode>().VisualNode(tictactoGameMap.BoardData[x, y]);                
            }
        }
    }

    public void NodeUpdate(int input, MapNode nodeType) {        
        tictactoGameMap.InputMove(input, nodeType);
        tictactoGameMap.AIMove();        
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
