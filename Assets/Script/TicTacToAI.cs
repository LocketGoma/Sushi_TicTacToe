using System.Collections;
using System.Collections.Generic;


public class TicTacToAI {
    //주요 내용 : 알파베타 가지치기 알고리즘.
    //https://ko.wikipedia.org/wiki/%EC%95%8C%ED%8C%8C-%EB%B2%A0%ED%83%80_%EA%B0%80%EC%A7%80%EC%B9%98%EA%B8%B0

    public TicTacToAI(TicTacToMap gameMap)
    {


    }


    public TicTacToMap tttBoard;//게임보드
    private int lookAheadLevel; //게임 레벨
    
    public MapNode mapNode; // Node

    private int bestX;  //최적 X 좌표
    private int bestY;  //최적 Y 좌표

    private int gameMode = 4;            //3 = 3x3, 4 = 4x4
    public int GameMode { get { return gameMode; } set { gameMode = value; } }
    public int LookAheadLevel { get { return lookAheadLevel; } set { lookAheadLevel = value; } }


    //최종 결과값 리턴.
    //외부에는 이 메소드만 호출되어야 함.
    public int AnswerNode(MapNode [,] mapData) {



        return 1;
    }


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




}
