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

    [SerializeField] GameObject killedParicle;

    [SerializeField] AudioSource bulletAudio;
    [SerializeField] AudioSource killedAudio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(DataManager.currentLevel == 2)
        {
            range = 10.0f;
            attackTime = 1.2f;
        }
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
            bulletAudio.Play();
            other.gameObject.SetActive(false);
            HealthScript health = GetComponent<HealthScript>();
            health.ApplyDamage(DataManager.currentLevel == 1 ? 10 : 25);
            if (health.currentHealth <= 0)
            {
                killedAudio.Play();
                Instantiate(killedParicle, other.transform.position, other.transform.rotation);
                DataManager.numberOfKilledGhosts += 1;
                gameObject.SetActive(false);
            }
        }
    }
}
