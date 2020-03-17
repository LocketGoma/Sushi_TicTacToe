using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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