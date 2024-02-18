using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float timer;
    private float attackTime = 3.0f;

    private float range = 8.0f;

    PlayerManager player;

    [SerializeField] string enemyTown;
    [SerializeField] int enemyNumber;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        if(SaveInfo.playerInfo.townInfoList.Where(x => x.TownName == enemyTown).FirstOrDefault().enemyHealth[enemyNumber] <= 0)
        {
            this.gameObject.SetActive(false);
        }
        HealthScript health = GetComponent<HealthScript>();
        health.ResetHealth(SaveInfo.playerInfo.townInfoList.Where(x => x.TownName == enemyTown).FirstOrDefault().enemyHealth[enemyNumber]);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            timer += Time.deltaTime;
            transform.transform.LookAt(player.transform.position);
            if(timer >= attackTime)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.back * 20.0f);

                timer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.tag == "PlayerBullet")
        {
            other.gameObject.SetActive(false);
            HealthScript health = GetComponent<HealthScript>();
            health.ApplyDamage(10);
            SaveInfo.playerInfo.townInfoList.Where(x => x.TownName == enemyTown).FirstOrDefault().enemyHealth[enemyNumber] = health.currentHealth;
            if (health.currentHealth <= 0)
            {
                //Instantiate(paticleExplode, other.transform.position, other.transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
 }
