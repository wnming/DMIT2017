using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int currentHealth;

    [SerializeField] int maxHealth;
    [SerializeField] Image healthUI;
    [SerializeField] TMP_Text healthValue;

    void Start()
    {
        currentHealth = maxHealth;
        healthValue.text = currentHealth.ToString();
        healthUI.transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
    }

    public void ApplyDamage(int damage)
    {
        if(currentHealth - damage < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }
        healthUI.transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
        healthValue.text = currentHealth.ToString();
    }

    public void AddHealthValue(int value)
    {
        int checkIfExceed = currentHealth + value;
        if(checkIfExceed > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = checkIfExceed;
        }
        healthUI.transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
        healthValue.text = currentHealth.ToString();
    }
}
