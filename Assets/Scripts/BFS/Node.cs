using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node : MonoBehaviour
{
    [SerializeField] MeshRenderer render;

    public NodeColorScriptable colorInfo;
    public EnumNodeData NodeData { get; set; }

    public int PosX { get; set; }
    public int PosY { get; set; }

    public void SetNodeInfo(EnumNodeData info, int x, int y)
    {
        PosX = x; PosY = y;
        NodeData = info;
        render.material.color = colorInfo.colors[(int)info];
    }
    public void SetNodeDiscovered()
    {
        NodeData = EnumNodeData.Covered;
        render.material.color = colorInfo.disCovered;
    }
}
