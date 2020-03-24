using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//				TicTacToeAI* tttAI = new TicTacToeAI(m_board);	/* 새로운 AI 객체를 생성 */
//				tttAI->GetBestMove();							/* 최적의 좌표를 구함 */
//				m_board.DoMove(tttAI->bestX, tttAI->bestY);		/* 해당 좌표에 수를 둠 */
//				Node* node = tttAI->GetRootNode();			/* 최적의 좌표를 구하는동안 저장한 트리 중 루트노드 반환 */
//				this->PrintTreeNode(node);					/* 트리 출력 */
//				UpdateGame();							/* 게임판 업데이트 */


public class TicTacToAI : MonoBehaviour {
    //주요 내용 : 알파베타 가지치기 알고리즘.
    //https://ko.wikipedia.org/wiki/%EC%95%8C%ED%8C%8C-%EB%B2%A0%ED%83%80_%EA%B0%80%EC%A7%80%EC%B9%98%EA%B8%B0
    //http://musicdiary.egloos.com/v/4274653
    //Int32.MinValue;
    public TicTacToManager ticTacToManager;
    [SerializeField] private int lookAheadLevel; //게임 레벨
    [SerializeField] private int callCutline;
    [Range(0,5)]
    [SerializeField] private int weightDiagonal;

    public TicTacToeNode tttNodeRoot; // 
    private int callCount = 0;


    private int gameMode = 1;            //3 = 3x3, 4 = 4x4
    public int GameMode { get { return gameMode; } set { gameMode = value; } }
    public int LookAheadLevel { get { return lookAheadLevel; } set { lookAheadLevel = value; } }


    private void Start() {
        GameMode = ticTacToManager.GameMode;
        if (callCutline == 0) {
            callCutline = Int32.MaxValue;
        }
    }

    //최종 결과값 리턴.
    //외부에는 이 메소드만 호출되어야 함.
    public int AnswerNode(MapNode[,] mapData) {
        if (gameMode < 3 || gameMode > 5) {
            Debug.LogError("게임 모드 범위 초과");
            return -1;
        }
      //  Debug.Log("111");
        var answerPos = CacluateNode(mapData);
        Debug.Log("callCount::" + callCount);
        callCount = 0;
        return answerPos.X * gameMode + answerPos.Y;
    }
    private Position CacluateNode(MapNode[,] mapData) {
        List<Position> positionList;
        int bestValue = Int32.MinValue;
        
        tttNodeRoot = new TicTacToeNode(gameMode, 0);       //최초값 초기화.
        positionList = GetPossibleMove(mapData);
        Position returnPos = positionList[0];
        // Debug.Log("222");

        foreach (Position pos in positionList) {
            var answer = FindMiniMax(pos, mapData, Int32.MinValue, Int32.MaxValue, true, 0);
      //      Debug.Log("===================Pos:" + pos.X + "," + pos.Y + " ans:" + answer);
            if (answer > bestValue) {
                bestValue = answer;
                returnPos = pos;
            }
        }
        Debug.Log("===================returnPos" + returnPos.X + "," + returnPos.Y);
        return returnPos;
    }
    private List<Position> GetPossibleMove(MapNode[,] mapData) { // 현재 보드 상태에서 갈수 있는 좌표 개수 검사.
        List<Position> nextPositions = new List<Position>();
        if (mapData[0, 0] == MapNode.None) { nextPositions.Add(new Position(0, 0)); }
        if (mapData[gameMode - 1, gameMode - 1] == MapNode.None) { nextPositions.Add(new Position(gameMode - 1, gameMode - 1)); }
        if (mapData[0, gameMode - 1] == MapNode.None) { nextPositions.Add(new Position(0, gameMode - 1)); }
        if (mapData[gameMode - 1, 0] == MapNode.None) { nextPositions.Add(new Position(gameMode - 1, 0)); }


        for (int i = 0; i < gameMode; i++) {
            for (int j = 0; j < gameMode; j++) {                
                if (mapData[i, j] == MapNode.None && !((i==j && i==0) || (i==j && i==(gameMode - 1)))&&!((i==0&&j==gameMode-1) || (j == 0 && i == gameMode - 1))) {
                    nextPositions.Add(new Position(i, j));
                }
            }
        }
        return nextPositions;
    }
    //isPlayer = true => User 차례 / isPlayer = false => AI 차례
    //alpah = best // beta = worst
    private int FindMiniMax(Position pos, MapNode[,] mapData, int alpha, int beta, bool isPlayer, int depth) {
        int bestValue = 0;

        //        Debug.Log("333");
        //        Debug.Log("depth" + depth);
        MapNode[,] mapDataCopy = MapCopy(mapData);
        List<Position> nextPosition;
        
  

        if (isPlayer == true) {
            mapDataCopy[pos.X, pos.Y] = MapNode.User;
            bestValue = Int32.MaxValue;
        } else { 
            mapDataCopy[pos.X, pos.Y] = MapNode.AI;
            bestValue = Int32.MinValue;
        }
        

        nextPosition = GetPossibleMove(mapDataCopy);

        

        if (nextPosition.Count == 0 || depth == lookAheadLevel || callCount > callCutline || WinnerCheckTTT(mapDataCopy, MapNode.None) != true) {
            var pointUser = CalculateValueTTT(mapDataCopy, MapNode.User);
            var pointAI = CalculateValueTTT(mapDataCopy, MapNode.AI);
            if (isPlayer == true) {
                bestValue = pointUser - pointAI;
            }
            else {
                bestValue = pointAI - pointUser;
            }
            callCount++;
          //  Debug.Log("isPlayer?" + isPlayer + ",:" + bestValue+",depth"+depth+"\n"+ pointUser+":"+ pointAI);
            return bestValue;
        }



        //Max 고를때 : 아래의 Min 값 중 Max값
        //Min 고를때 : 아래의 Max 값 중 Min값
        //현재가 Player 일때 = Min을 고름
        //현재가 AI일때 = Max을 고름

        foreach (Position nextPos in nextPosition) {            
           var value = FindMiniMax(nextPos, mapDataCopy, alpha, beta, isPlayer == false, depth + 1);
           // Debug.Log("first best : " + bestValue);
            if(isPlayer == true) {
                bestValue = Math.Min(value, bestValue);
                beta = Math.Min(bestValue, beta);
            } else {
                bestValue = Math.Max(value, bestValue);
                alpha = Math.Max(bestValue, alpha);
            }
            // Debug.Log("A:" + alpha + ",B:" + beta);
           // Debug.Log("second best : " + bestValue);
            if (beta <= alpha) {
                break;
            }
        }
        
        
        return bestValue;
    }
    private MapNode[,] MapCopy(MapNode[,] mapData) {
        MapNode[,] copyMap;
        copyMap = new MapNode[gameMode, gameMode];
        for (int i = 0; i < gameMode; i++) {
            for (int j = 0; j < gameMode; j++) {
                copyMap[i, j] = mapData[i, j];
            }
        }

        return copyMap;
    }
    //Node별 추측값 계산기
    private int CalculateValueTTT(MapNode[,] mapData, MapNode mapNode) {  
        if(WinnerCheckTTT(mapData, mapNode) == true) {
            return 100;
        }

        int result = 0;
        var copyMapData = MapCopy(mapData);

        for (int i = 0; i < gameMode; i++) {
            //X축 검사
            for (int j = 0; j < gameMode; j++) {
                if (copyMapData[i, j] == MapNode.None) {
                    copyMapData[i, j] = mapNode;
                }
            }
            if (WinnerCheckTTT(copyMapData, mapNode) == true) {
                result++;
            }
            copyMapData = MapCopy(mapData);

            //Y축 검사
            for (int j = 0; j < gameMode; j++) {
                if (copyMapData[j, i] == MapNode.None) {
                    copyMapData[j, i] = mapNode;
                }
            }
            if (WinnerCheckTTT(copyMapData, mapNode) == true) {
                result++;
            }
            copyMapData = MapCopy(mapData);
        }
        //대각선 검사 1
        for (int i = 0; i < gameMode; i++) {
            if (copyMapData[i, i]==MapNode.None) {
                copyMapData[i, i] = mapNode;
            }
        }
        if (WinnerCheckTTT(copyMapData, mapNode) == true) {
            if (mapNode == MapNode.User) {
                result = +(weightDiagonal * weightDiagonal);  //대각선 가중치
            } else {
                result++;
            }
        }
        copyMapData = MapCopy(mapData);

        //대각선 검사 2
        for (int i = 0; i < gameMode; i++) {
            if (copyMapData[gameMode - (i+1), i] == MapNode.None) {
                copyMapData[gameMode - (i+1), i] = mapNode;
            }
        }
        if (WinnerCheckTTT(copyMapData, mapNode) == true) {
            if (mapNode == MapNode.User) {
                result = +(weightDiagonal * weightDiagonal);  //대각선 가중치
            }
            else {
                result++;
            }
        }

        //Debug.Log(mapNode+"Res:" + result);

        return result;
    }
    private bool WinnerCheckTTT(MapNode[,] mapData, MapNode mapNode) {
        return ticTacToManager.WinnerCheck(mapData) == mapNode ? true : false;
    }
    





    /*
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

    */
}




public class TicTacToeNode {
    private int depth;
    private int eval;
    private int childCnt;

    public int Depth { get { return depth; } set { depth = value; } }           //노드 깊이 ( = 레벨)
    public int Evaluation { get { return eval; } set { eval = value; } }        //게임 평가값
    public int ChildCount { get { return childCnt; } set { childCnt = value; } }//노드가 가지고 있는 자식 노드 개수

    public TicTacToeNode[] next;             // 자식 노드들


    /*
    public void InitNode(int gameMode, int inDepth) {           //= 생성자 포지션.
        Depth = inDepth;
        Evaluation = 0;
        ChildCount = 0;

        next = new TicTacToeNode[gameMode * gameMode];
    }
    */
    public TicTacToeNode(int gameMode, int inDepth) {           //생성자 / 초기화
        Depth = inDepth;
        Evaluation = 0;
        ChildCount = 0;

        next = new TicTacToeNode[gameMode * gameMode];
    }
}

public class Position {     //좌표 클래스
    private int x;
    private int y;

    public Position() {;}
    public Position(int inx, int iny) {
        x = inx;
        y = iny;
    }
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
}