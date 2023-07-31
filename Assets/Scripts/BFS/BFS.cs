using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
    private List<List<Node>> mazeInfo;

    private Queue<Node> bfsQueue = new Queue<Node>();    
    private Queue<Node> buffer = new Queue<Node>();
    public Node StartNode { get; set; }

    public void SetMazeInfo(List<List<Node>> mazeInfo) => this.mazeInfo= mazeInfo;

    public float searchPeriod;

    private int rowNum;
    private int colNum;

    private int far;
    public Action<int> farCount;
    public Action<int> end;

    private bool foundLever = false;
    private bool escaped = false;

    public void StartBFSCoroutine()
    {
        StartCoroutine(BFSCoroutine());
    }
    private IEnumerator BFSCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(searchPeriod);

        while(CheckQueue())
        {
            DoSearch();
            yield return wait;
        }

        SetResult();
    }

    private void SetResult()
    {
        if (!escaped)
        {
            far = -1;
            farCount.Invoke(far);
        }

        end.Invoke(far);
    }

    private bool CheckQueue()
    {
        if (bfsQueue.Count == 0)
        {
            if (buffer.Count == 0)
            {   
                return false;
            }

            if (buffer.Count > 0)
            {
                FarCountUp();
                (bfsQueue, buffer) = (buffer, bfsQueue);
            }
        }

        return true;
    }
    public void DoSearch()
    {
        Node dequeued = bfsQueue.Dequeue();

        const float step = 90f * Mathf.Deg2Rad;
        for (int i = 0; i < 4; i++)
        {
            float angleRad = i * step;
            int x = dequeued.PosX + (int)Mathf.Cos(angleRad);
            int y = dequeued.PosY + (int)Mathf.Sin(angleRad);

            if (CheckValidPoint(x, y))
            {
                Node node = mazeInfo[y][x];
                CheckSymbolPoint(node);
                SetNodeDiscovered(node);
            }            
        }
    }

    public void SearchOneStep()
    {
        if (!CheckQueue())
        {
            SetResult();
            return;
        }

        DoSearch();
    }

    private void CheckSymbolPoint(Node node)
    {
        if(node.NodeData == EnumNodeData.Lever)
        {
            bfsQueue.Clear();
            buffer.Clear();
            foundLever = true;
        }

        if(node.NodeData == EnumNodeData.Exit && foundLever)
        {
            bfsQueue.Clear();
            buffer.Clear();
            escaped = true;
        }        
    }

    public void Init()
    {
        buffer.Enqueue(StartNode);
        colNum = mazeInfo.Count; 
        rowNum = mazeInfo[0].Count;        
    }
    private bool CheckValidPoint(int x, int y)
    {
        if (x < 0 || x >= rowNum || y < 0 || y >= colNum) return false;

        return mazeInfo[y][x].NodeData != EnumNodeData.Covered;
    }

    private void SetNodeDiscovered(Node node)
    {
        node.SetNodeDiscovered();

        if(!escaped)
            buffer.Enqueue(node);
    }
    public void FarCountUp()
    {   
        farCount.Invoke(++far);
    }
}
