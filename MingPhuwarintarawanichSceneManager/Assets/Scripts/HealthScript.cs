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
        //currentHealth = maxHealth;
        healthValue.text = currentHealth.ToString();
        healthUI.transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
        healthValue.text = currentHealth.ToString();
    }

    public void ResetHealth(int newHealthValue)
    {
        currentHealth = newHealthValue;
        healthValue.text = currentHealth.ToString();
        healthUI.transform.localScale = new Vector3(currentHealth / (float)maxHealth, 1, 1);
    }
}
