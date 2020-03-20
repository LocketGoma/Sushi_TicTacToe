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
    private int lookAheadLevel; //게임 레벨
    
    public MapNode mapNode; // Node


    private int gameMode = 4;            //3 = 3x3, 4 = 4x4
    public int GameMode { get { return gameMode; } set { gameMode = value; } }
    public int LookAheadLevel { get { return lookAheadLevel; } set { lookAheadLevel = value; } }


    private void Start() {
        GameMode = ticTacToManager.GameMode;
    }

    //최종 결과값 리턴.
    //외부에는 이 메소드만 호출되어야 함.
    public int AnswerNode(MapNode [,] mapData) {
        Position[] positionList = new Position[gameMode * gameMode];
        int bestValue = Int32.MinValue;
        int possibleCount = 0;

        for (int i = 0; i < possibleCount; i++) {




        }



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
