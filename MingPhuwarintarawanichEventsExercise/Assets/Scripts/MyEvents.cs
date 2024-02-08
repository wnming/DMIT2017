using UnityEngine;
using UnityEngine.Events;
using System.Collections;

static public class MyEvents
{
    static public UnityEvent Activate = new UnityEvent();
    static public UnityEvent ChangeColor = new UnityEvent();
    
    static public UnityEvent<int> CollectCoin = new UnityEvent<int>();
    static public UnityEvent SpawnCoin = new UnityEvent();
    //static public UnityEvent dd = new UnityEvent();
}
