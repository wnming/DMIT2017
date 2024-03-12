using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public int Damage;
    public int Speed;

    public Sword (string name, int id, int damage, int speed)
    {
        Name = name;
        ID = id;
        Damage = damage;
        Speed = speed;
    }
    public override void Attack()
    {
        Debug.Log("Attack with a " + Name + " for: " + Damage);
    }
}
