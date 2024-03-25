using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;
    float rotationSpeed;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject youWinPanel;

    [SerializeField] GameObject followSpot;

    public bool isRescuedAFriend;

    private void Awake()
    {
        DataManager.InitialEnemyHealth();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 35.0f;
        rotationSpeed = 70.0f;
        gameOverPanel.SetActive(false);
        youWinPanel.SetActive(false);
        isRescuedAFriend = false;
        HealthScript health = GetComponent<HealthScript>();
        health.currentHealth = 100;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.forward * 15.0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyBullet")
        {
            other.gameObject.SetActive(false);
            HealthScript health = GetComponent<HealthScript>();
            health.ApplyDamage(5);
            if (health.currentHealth <= 0)
            {
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
            }
        }
        if(other.gameObject.tag == "WinSpot")
        {
            if (isRescuedAFriend)
            {
                Time.timeScale = 0;
                youWinPanel.SetActive(true);
            }
        }
        if (other.gameObject.tag == "HelpAFriendSpot")
        {
            if (!isRescuedAFriend)
            {
                isRescuedAFriend = true;
                GameObject friend = GameObject.FindGameObjectWithTag("Friend");
                friend.transform.SetParent(followSpot.transform);
                friend.transform.localPosition = Vector3.zero;
                friend.transform.localRotation = followSpot.transform.localRotation;
            }
        }
    }
}
