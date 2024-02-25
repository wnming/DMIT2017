using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;
    float rotationSpeed;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;

    [SerializeField] TextMeshProUGUI treasure;
    [SerializeField] TextMeshProUGUI restingText;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] TextMeshProUGUI endGameText;

    private bool isResting;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 25.0f;
        rotationSpeed = 70.0f;

        isResting = false;

        HealthScript health = GetComponent<HealthScript>();
        health.ResetHealth(SaveInfo.playerInfo.currentHealth);

        restingText.text = "";

        pausePanel.SetActive(false);
        endGamePanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isResting)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
            rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);
        }
    }

    private void Update()
    {
        treasure.text = $"Treasure(s): {SaveInfo.playerInfo.treasure}/3";

        SaveInfo.playerInfo.currentLocation = transform.position;

        if (!isResting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.forward * 15.0f);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void SaveGame()
    {
        SaveInfo.SaveInfoToFile();
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator Resting()
    {
        restingText.text = "Resting...";
        yield return new WaitForSeconds(10);
        restingText.text = "";
        isResting = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            other.gameObject.SetActive(false);
            HealthScript health = GetComponent<HealthScript>();
            health.ApplyDamage(10);
            SaveInfo.playerInfo.currentHealth = health.currentHealth;
            if (health.currentHealth <= 0)
            {
                Time.timeScale = 0;
                endGameText.text = "Game Over!";
                endGamePanel.SetActive(true);
                SaveInfo.DeleteFile();
            }
        }
        if (other.gameObject.tag == "Treasure")
        {
            SaveInfo.playerInfo.townInfoList.Where(x => x.TownName == SaveInfo.playerInfo.currentScene).FirstOrDefault().isTreasureCollected = true;
            other.gameObject.SetActive(false);
            SaveInfo.playerInfo.treasure += 1;
            if (SaveInfo.playerInfo.treasure >= 3)
            {
                Time.timeScale = 0;
                endGameText.text = "All treasures have been collected!";
                endGamePanel.SetActive(true);
                SaveInfo.DeleteFile();
            }
        }
        if (other.gameObject.tag == "Inn" && !isResting)
        {
            isResting = true;

            HealthScript health = GetComponent<HealthScript>();
            health.ResetHealth(50);
            SaveInfo.playerInfo.currentHealth = health.currentHealth;
            //reset all enemies 
            for(int index = 0; index < 3; index++)
            {
                for(int enemyIndex = 0; enemyIndex < SaveInfo.playerInfo.townInfoList[index].enemyHealth.Count; enemyIndex++)
                {
                    SaveInfo.playerInfo.townInfoList[index].enemyHealth[enemyIndex] = 30;
                }
            }
            StartCoroutine(Resting());
        }
    }

}
