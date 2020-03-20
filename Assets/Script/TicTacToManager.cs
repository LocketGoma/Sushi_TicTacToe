using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToManager : MonoBehaviour
{
    public int gameMode = 4;            //3 = 3x3, 4 = 4x4
    public TicTacToVisual ticTacToVisual;
    public TicTacToMap ticTacToMap;
    //얘만 퍼블릭
    
    public int gameTern;
    public GameState gamePlayState;
    public MapNode[,] mapState;

    public int GameMode { get { return gameMode; } set { gameMode = value; } }
    public int GameTurn { get { return gameTern; } set { gameTern = value; } }

    public GameState GamePlayState { get { return gamePlayState; } }





    public void ResetGame () {
        gamePlayState = GameState.Play;
        ticTacToMap.InitBoard();
        ticTacToVisual.MapUpdate();
        ticTacToVisual.WinnerPopup(gamePlayState);
        Debug.Log("reset");
    }
    
    public GameState GameResult(MapNode[] mapState) {
        MapNode result;
        result = WinnerCheck(MapParser(mapState));

        
        switch (result) {
            case MapNode.User : {
                    gamePlayState = GameState.WinUser;
                    break;
                }
            case MapNode.AI : {
                    gamePlayState = GameState.WinAI;
                    break;
                }
            default: {
                if (GameTurn < gameMode * gameMode) {
                    gamePlayState = GameState.Play;
                    break;
                } else {
                    gamePlayState = GameState.Draw;
                    break;
                }
            }
        }
        return gamePlayState;
    }
    public GameState GameResult(MapNode[,] mapState) {
        MapNode result;
        result = WinnerCheck(mapState);


        switch (result) {
            case MapNode.User: {
                    gamePlayState = GameState.WinUser;
                    break;
                }
            case MapNode.AI: {
                    gamePlayState = GameState.WinAI;
                    break;
                }
            default: {
                if (GameTurn < gameMode * gameMode) {
                    gamePlayState = GameState.Play;
                    break;
                }
                else {
                    gamePlayState = GameState.Draw;
                    break;
                }
            }
        }
        return gamePlayState;
    }
    private MapNode[,] MapParser(MapNode[] mapState) {
        MapNode [,] resultMapState;
        resultMapState = new MapNode[gameMode, gameMode];

        for (int i = 0; i < gameMode * gameMode; i++) {
            resultMapState[i / gameMode, i % gameMode] = mapState[i];
        }
        return resultMapState;
    } 
    private MapNode WinnerCheck(MapNode[,] mapState) {
        bool findWinnerX = true;
        bool findWinnerY = true;

        //X축 Y축 판독
        for (int i = 0; i < gameMode; i++) {
            for (int j = 1; j < gameMode; j++) {
                Debug.Log("x:" + i + ", y:" + j);
                if (mapState[i,0] != mapState[i,j]) {
                    findWinnerX = false;
                    break;
                }
            }
            for (int k = 1; k < gameMode; k++) {
                if (mapState[0,i] != mapState[k, i]) {
                    findWinnerY = false;
                    break;
                }
            }
            if (findWinnerX == true) {
                return mapState[i, 0];
            } else if(findWinnerY == true) {
                return mapState[0, i];
            } else {
                findWinnerX = true;
                findWinnerY = true;
            }
        }
        //대각선 판독
        //X - 정방향
        //Y - 역방향
        for (int i = 1; i < gameMode; i++) {
            if (mapState[0, 0] != mapState[i, i]) {
                findWinnerX = false;
            }
            if (mapState[0,gameMode - 1] != mapState[i, gameMode - (i+1)]){
                findWinnerY = false;
            }

        }
        if (findWinnerX == true) {
            return mapState[0, 0];
        }
        else if (findWinnerY == true) {
            return mapState[0, gameMode - 1];
        }

        //전부 실패시 : 
        return MapNode.None;
    }



}
public class TicTacToNode {
    private int depth;
    private int eval;
    private int childCnt;

    public int Depth { get { return depth; } set { depth = value; } }            //노드 깊이
    public int Evaluation { get { return eval; } set { eval = value; } }        //게임 평가값
    public int ChildCount { get { return childCnt; } set { childCnt = value; } }//노드가 가지고 있는 자식 노드 개수

    public TicTacToNode[] next;             // 자식 노드들

    public TicTacToNode() {
        next = new TicTacToNode[16];
    }
}
public class Position {     //좌표 클래스
    private int x;
    private int y;
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
}

public enum MapNode {
    None = 0,
    User = 1,
    AI = 2
} // Node Data enum
public enum GameState {
    WinUser = 1,        //유저승리
    WinAI = 2,          //AI승리
    Draw = 3,           //비김
    End = 4,            //그 외 게임 종료
    Play = 5,           //게임진행중
    Init = 6,           //초기화
    Stop = 7            //일시정지
}