using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Collider : MonoBehaviour
{
    [SerializeField] GameObject levelIsLockedText;
    bool isTextShowing;

    void Start()
    {
        isTextShowing = false;
        levelIsLockedText.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !isTextShowing)
        {
            isTextShowing = true;
            StartCoroutine(DisplayText());
        }
    }

    IEnumerator DisplayText()
    {
        levelIsLockedText.SetActive(true);
        yield return new WaitForSeconds(3);
        levelIsLockedText.SetActive(false);
        isTextShowing = false;
    }
}
