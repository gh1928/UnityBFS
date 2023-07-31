using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeColor", menuName = "Scriptable/NodeColor")]
public class NodeColorScriptable : ScriptableObject
{
    public Color[] colors;

    public Color disCovered = Color.red;
}
