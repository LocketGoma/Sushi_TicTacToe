using System.Collections;
using System.Collections.Generic;


public class TicTacToAI {
    public TicTacToAI(TicTacToMap gameMap)
    {


    }


    public TicTacToMap tttBoard;//게임보드
    private int lookAheadLevel; //게임 레벨
    public int LookAheadLevel { get { return lookAheadLevel; } set { lookAheadLevel = value; } }
    public enum MapNode {
        None = 0,
        User = 1,
        AI = 2
    } // Node Data enum
    public MapNode mapNode; // Node

    private int bestX;  //최적 X 좌표
    private int bestY;  //최적 Y 좌표

    private void InitNode(){    //기본정보 초기화
    

    }
    private void GetBestMove(){     //최적 좌표 구하기
    

    }
    private int Minimize(){          //Eval 값중 Min 값을 구함
    

        return 1;
    }
    private int Maximize(){          //Eval 값중 Max 값을 구함
    

        return 1;
    }
    private int EvaluateBoard() {   //현재 보드판의 Eval값(평가값) 계산

        return 1;
    }
    private int CheckSymmetric(TicTacToMap preBoard, TicTacToMap newBoard){  //보드 대칭성 검사
    

        return 1;
    }
    private int GetPossibleMove(){ // 현재 보드 상태에서 갈수 있는 좌표 개수 검사.
    
        return 1;
    }
    private int GameEndCheck() {    //게임이 끝났는지 검사

        return 0;
    }



}
