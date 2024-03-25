using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float timer;
    private float attackTime = 2.0f;

    private float range = 8.0f;

    PlayerController player;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;

    [SerializeField] int enemyNumber;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (DataManager.enemyHealth[enemyNumber] <= 0)
        {
            gameObject.SetActive(false);
        }
        HealthScript health = GetComponent<HealthScript>();
        health.ResetHealth(DataManager.enemyHealth[enemyNumber]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            timer += Time.deltaTime;
            transform.transform.LookAt(player.transform.position);
            if (timer >= attackTime)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.back * 20.0f);

                timer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            other.gameObject.SetActive(false);
            HealthScript health = GetComponent<HealthScript>();
            health.ApplyDamage(10);
            DataManager.enemyHealth[enemyNumber] = health.currentHealth;
            if (health.currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
