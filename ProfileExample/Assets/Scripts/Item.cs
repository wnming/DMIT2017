[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
}


public abstract class Weapon : Item
{
    public int Damage;
    public int Speed;

    public abstract void Attack();
}

public class Armor : Item
{
    public int Type;
    public int Defense;
}

public abstract class Tool : Item
{
    public abstract void Use();
}

public abstract class Consumable : Item
{
    public abstract void Consume();
}
