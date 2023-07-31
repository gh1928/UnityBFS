using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour
{
    [SerializeField] string mazeInput;

    [SerializeField] Node nodePrefab;

    private List<List<Node>> mazeInfo = new List<List<Node>>();

    private GameObject mazeHolder;

    private Camera cam;

    [SerializeField]
    private BFS bfs;
    private void Start()
    {
        cam = Camera.main;
        SetMazeInfo();
    }

    private void SetMazeInfo()
    {
        mazeHolder = new GameObject("MazeHolder");

        int length = mazeInput.Length;

        int colNum = 0;
        int rowNum = 0;

        mazeInfo.Add(new List<Node>());
        
        bfs.SetMazeInfo(mazeInfo);

        for (int i = 0; i < length; i++)
        {
            if (mazeInput[i].Equals('"'))
                continue;

            if (mazeInput[i].Equals(','))
            {
                rowNum = 0;
                colNum++;
                mazeInfo.Add(new List<Node>());
                continue;
            }
            
            Node newNode = Instantiate(nodePrefab, new Vector3(rowNum, colNum, 0f), Quaternion.identity, mazeHolder.transform);
            newNode.SetNodeInfo(CharToNodeData(mazeInput[i]), rowNum, colNum);
            mazeInfo[colNum].Add(newNode);
            rowNum++;

            if (mazeInput[i].Equals('S'))
            {
                newNode.SetNodeDiscovered();                
                bfs.StartNode = newNode;
            }
        }

        cam.transform.position = new Vector3(mazeInfo.Count*0.5f, mazeInfo[0].Count*0.5f, -10);
        cam.orthographicSize = mazeInfo.Count * 0.7f;
        bfs.Init();
    }

    private EnumNodeData CharToNodeData(char c) => c switch
    {        
        'L' => EnumNodeData.Lever,
        'E' => EnumNodeData.Exit,
        'X' => EnumNodeData.Covered,
        _ => EnumNodeData.Path,
    };

    private void ClearMaze()
    {
        Destroy(mazeHolder);

        int length = mazeInfo.Count;
        for(int i = 0; i < length; i++)
        {
            mazeInfo[i].Clear();
        }
        mazeInfo.Clear();
    }
    public List<List<Node>> GetMazeInfo() => mazeInfo;
}

public enum EnumNodeData
{
    None = -1,    
    Path,
    Covered,
    Lever,
    Exit,
}