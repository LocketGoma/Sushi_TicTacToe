using UnityEngine;

/*
public class TreeNode {
    private int depth;
    private int eval;
    private int childCnt;

    public int Depth { get { return depth;} set { depth = value; } }            //노드 깊이
    public int Evaluation { get { return eval; } set { eval = value; } }        //게임 평가값
    public int ChildCount { get { return childCnt; } set { childCnt = value; } }//노드가 가지고 있는 자식 노드 개수

    public TreeNode[] next;             // 자식 노드들

    public TreeNode() {
        next = new TreeNode[16];
    }
}
*/
// GameBoard 3x3, 4x4 게임은 상속시켜서 만드는거로.
// "수" = Turn.
public class TicTacToMap : MonoBehaviour {

    [SerializeField] private TicTacToManager ticTacToeManager;
    [SerializeField] private TicTacToAI ticTacToAI;
    [SerializeField] private int gameMode = 4;           //게임모드 (3x3 or 4x4)    
    

    private MapNode [,] boardData;  //'맵 정보'
    private int moveCount;          //현재 움직일 수 있는 "수" 의 개수
    private GameState gamePlayState;//게임판 상태
    private int comLevel;           //AI 레벨
    private MapNode nodePlayer;     //플레이어 노드 번호 (판 표시 번호)
    private MapNode nodeAI;         //AI 노드 번호      (판 표시 번호, O/X같은 포지션)
    

    //게임 상태 반환
    public GameState GamePlayState { get { return gamePlayState; } }
    //게임판 선택 상태 반환
    public MapNode [,] BoardData { get { return boardData; } }
    //게임모드 상태
    public int GameMode { get { return gameMode; } set { gameMode = value; } }


    private void Start() {
        gameMode = ticTacToeManager.GameMode;
        boardData = new MapNode[gameMode, gameMode];        
    }

    public TicTacToMap() {           
        nodePlayer = MapNode.User;
        nodeAI = MapNode.AI;        
        gamePlayState = GameState.Play;
    }
    //깊-은 복사
    public TicTacToMap(TicTacToMap mapCopy){          
        nodePlayer = MapNode.User;
        nodeAI = MapNode.AI;        
        gamePlayState = GameState.Init;

        boardData = mapCopy.boardData;        
        moveCount = mapCopy.moveCount;
        gamePlayState = mapCopy.gamePlayState;
        comLevel = mapCopy.comLevel;        
    }
    
    /*
    public void InitBoard(int startCom, int moveCnt, int level) {
        moveCount = moveCnt;        //현재 "수" 를 저장
        nodePlayer = MapNode.User;
        nodeAI = MapNode.AI;
        comLevel = level;

        gamePlayState = GameState.Play;

        for (int i = 0; i < gameMode; i++) {
            for (int j = 0; j < gameMode; j++) {
                boardData[i, j] = MapNode.None;
            }
        }
        moveCount = 0;

    }
    */
    public void InitBoard() {
        nodePlayer = MapNode.User;
        nodeAI = MapNode.AI;


        gamePlayState = GameState.Play;

        for (int i = 0; i < gameMode; i++) {
            for (int j = 0; j < gameMode; j++) {
                boardData[i, j] = MapNode.None;
            }
        }
        moveCount = 0;

    }
    public void AIMove() {
        GameEndCheck();
        if (gamePlayState != GameState.End) {
            if (ticTacToAI == null || ticTacToAI.LookAheadLevel == 1) {
                RandomMove();
            }
            else {
                Debug.Log("000");
                InputMove(ticTacToAI.AnswerNode(boardData), MapNode.AI);
            }
        }

    }
    //무르기용? 일단 임시용으로 만들어줘야지.
    public void RandomMove() {
        if (GamePlayState == GameState.Play) {
            int randomInput = UnityEngine.Random.Range(0, gameMode * gameMode);


            while (CheckMove(randomInput) == false) {
                randomInput = UnityEngine.Random.Range(0, gameMode * gameMode);
            }
            InputMove(randomInput, MapNode.AI);
        }
    }
    public void InputMove(int input, MapNode selected) {
        if (GamePlayState == GameState.Play) {
            DoMove(input / gameMode, input % gameMode, selected);
        }        
    }
    private bool CheckMove(int input) {
        return boardData[input / gameMode, input % gameMode] == MapNode.None ? true : false;
    }
    private void DoMove(int x, int y, MapNode selected) {
        //Debug.Log(x + ":" + y);
        boardData[x, y] = selected;
       // prePos[moveCount].X = x;
       // prePos[moveCount].Y = y;

        moveCount++;
        ticTacToeManager.gameTern = moveCount;
        gamePlayState = ticTacToeManager.GameResult(boardData);
        if (gamePlayState != GameState.Play) {
            Debug.Log("gamePlayState : "+ gamePlayState);
        }
        GameEndCheck();
    }

    private void GameEndCheck() {
        if (moveCount == GameMode * GameMode) {
            gamePlayState = GameState.End;
        }
    }
    
}


