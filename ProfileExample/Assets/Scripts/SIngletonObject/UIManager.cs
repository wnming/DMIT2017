using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    void Start()
    {
        //layerName.text = GameManager.playerName;
        GameManager.playerName = "Haha";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
