using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : Armor
{
    public int Type;
    public int Defense;

    public Helm(string name, int id, int type, int defense)
    {
        Name = name;
        ID = id;
        Type = type;
        Defense = defense;
    }
}
