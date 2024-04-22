using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float movementSpeed;
    float rotationSpeed;

    [SerializeField] GameObject level1Gun;
    [SerializeField] GameObject level2Gun;

    [SerializeField] GameObject level1Bullet;
    [SerializeField] GameObject level2Bullet;

    [SerializeField] GameObject level1BulletSpawn;
    [SerializeField] GameObject level2BulletSpawn;

    [SerializeField] GameObject weaponChangeParticle;

    bool isSpeedBoostOn;
    float speedBoostTime = 10f;
    float countSpeedBoostTime;

    HealthScript health;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementSpeed = 20.0f;
        rotationSpeed = 40.0f;
        isSpeedBoostOn = false;
        health = GetComponent<HealthScript>();
    }

    void Update()
    {
        if(Time.timeScale == 1)
        {
            if (DataManager.isTimeRun)
            {
                DataManager.time -= Time.deltaTime;
            }

            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
            rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = Instantiate(DataManager.currentLevel == 1 ? level1Bullet : level2Bullet, DataManager.currentLevel == 1 ?level1BulletSpawn.transform.position : level2BulletSpawn.transform.position, DataManager.currentLevel == 1? level1BulletSpawn.transform.rotation : level2BulletSpawn.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.forward * 15.0f);
            }

            if (isSpeedBoostOn)
            {
                if(countSpeedBoostTime <= speedBoostTime)
                {
                    countSpeedBoostTime += Time.deltaTime;
                    movementSpeed = 35.0f;
                }
                else
                {
                    countSpeedBoostTime = 0;
                    speedBoostTime = 10;
                    movementSpeed = 20.0f;
                    isSpeedBoostOn = false;
                }
            }
        }
    }

    public void SpeedBoost()
    {
        if (isSpeedBoostOn)
        {
            speedBoostTime += 10;
        }
        else
        {
            isSpeedBoostOn = true;
        }
    }

    public void TimeExpand()
    {
        DataManager.time += 10.0f;
    }

    public void AddHealth(int value)
    {
        health.AddHealthValue(value);
    }

    public void ChangeWeapon()
    {
        level1Gun.SetActive(false);
        Instantiate(weaponChangeParticle, level2Gun.transform);
        level2Gun.SetActive(true);
        Instantiate(weaponChangeParticle, level2Gun.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            GameObject.FindGameObjectWithTag("playerHurtAudio").GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            HealthScript health = GetComponent<HealthScript>();
            health.ApplyDamage(DataManager.currentLevel == 1 ? 5 : 10);
            if (health.currentHealth <= 0)
            {
                Time.timeScale = 0;
            }
        }
    }
}
