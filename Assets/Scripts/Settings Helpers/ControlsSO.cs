using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Controls", order = 1)]
public class ControlsSO : ScriptableObject
{
    public float deadZone = 0;
    public string horizontalInputName = "Horizontal";
    public string shootInputName = "Fire";
}

