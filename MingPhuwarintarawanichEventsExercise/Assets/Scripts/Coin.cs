using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] bool isRed, isWaitForDisappearing;

    void Start()
    {
        isWaitForDisappearing = false;
    }

    void Update()
    {
        if (isRed && !isWaitForDisappearing)
        {
            Debug.Log("haha");
            isWaitForDisappearing = true;
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MyEvents.CollectCoin.Invoke(value);
            Destroy(gameObject);
        }
    }
}
