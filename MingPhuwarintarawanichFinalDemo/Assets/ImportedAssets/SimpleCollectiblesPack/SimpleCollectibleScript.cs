using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, Type1, Type2, Type3, Type4, Type5}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioSource collectSound;

	[SerializeField] GameObject collectEffect;

	[SerializeField] GameObject keyPanel;

	PlayerController player;

	// Use this for initialization
	void Start () 
	{
		if (keyPanel != null)
		{
            keyPanel.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	public void Collect()
	{
		if (collectSound)
			collectSound.Play();

            //AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		if (CollectibleType == CollectibleTypes.Type1) 
		{
			//star
			player.SpeedBoost();
        }
		if (CollectibleType == CollectibleTypes.Type2) 
		{
            //time
            player.TimeExpand();
        }
		if (CollectibleType == CollectibleTypes.Type3) {

			//health
			player.AddHealth(10);

		}
		if (CollectibleType == CollectibleTypes.Type4) {

			//Key
			DataManager.isKeyCollected = true;
			keyPanel.SetActive(true);

        }
		Destroy (gameObject);
	}
}
