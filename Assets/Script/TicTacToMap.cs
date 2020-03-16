using System.Collections;
using System.Collections.Generic;

public class Position {     //좌표 클래스
    private int x;
    private int y;
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }
}

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
// GameBoard
public class TicTacToMap
{
    public enum GameState {
        WinA = 3,
        WinB = 4,
        Draw = 5,
        Play = 7,
        Init = 8,
        Stop = 9
    }

    private Position prePos;        //이전 선택위치 저장





}


