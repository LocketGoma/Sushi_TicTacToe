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
    public MapNode WinnerCheck(MapNode[,] mapState) {
        bool findWinnerX = true;
        bool findWinnerY = true;    

        //X축 Y축 판독
        for (int i = 0; i < gameMode; i++) {
            findWinnerX = true;
            findWinnerY = true;
            if (mapState[i, 0] != MapNode.None) {
                for (int j = 1; j < gameMode; j++) {                    
                    if (mapState[i, 0] != mapState[i, j]) {
                        findWinnerX = false;
                        break;
                    }
                }
            } else {
                findWinnerX = false;
            }
            if (mapState[0, i] != MapNode.None) {                
                for (int k = 1; k < gameMode; k++) {
                    if (mapState[0, i] != mapState[k, i]) {
                        findWinnerY = false;
                        break;
                    }
                }
            } else {
                findWinnerY = false;
            }
            if (findWinnerX == true) {
                return mapState[i, 0];
            } else if(findWinnerY == true) {
                return mapState[0, i];
            } 
        }
        findWinnerX = true;
        findWinnerY = true;
        //대각선 판독
        //X - 정방향
        //Y - 역방향
        if (mapState[0, 0] != MapNode.None || mapState[0, gameMode - 1] != MapNode.None) {
            for (int i = 1; i < gameMode; i++) {
                if (mapState[0, 0] != mapState[i, i]) {
                    findWinnerX = false;
                }
                if (mapState[0, gameMode - 1] != mapState[i, gameMode - (i + 1)]) {
                    findWinnerY = false;
                }
            }
        } else {
            findWinnerX = false;
            findWinnerY = false;
        }
        
        //if(findWinnerX || findWinnerY) {
      //   Debug.Log(mapState[0, 0] + ":" + mapState[0, 1] + ":" + mapState[0, 2] + ":" + mapState[0, 3] + "\n" +
      //  mapState[1, 0] + ":" + mapState[1, 1] + ":" + mapState[1, 2] + ":" + mapState[1, 3] + "\n" +
      //  mapState[2, 0] + ":" + mapState[2, 1] + ":" + mapState[2, 2] + ":" + mapState[2, 3] + "\n" +
      //  mapState[3, 0] + ":" + mapState[3, 1] + ":" + mapState[3, 2] + ":" + mapState[3, 3] + "\n");
        //}
        


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