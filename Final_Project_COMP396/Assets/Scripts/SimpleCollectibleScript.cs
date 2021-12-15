using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour  {

	public enum CollectibleTypes {NoType, Type1, Type2, Type3, Type4, Type5}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;
	int i = 0;
	public AudioClip collectSound;
	public AudioClip frog;
	public GameObject collectEffect;

	// Use this for initialization
	void Start () {
		if(this.gameObject.tag=="Heart")
        {
			if (frog)
				AudioSource.PlayClipAtPoint(frog, transform.position);
		}
	}
	// Update is called once per frame
	void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}
	void OnTriggerEnter(Collider other)
	{
	
		if (other.tag == "Player") {
			if (this.gameObject.tag == "ring")
			{
				
					CmdAdjustScore(1);
					Collect();
				
				
			}
			else
			{
				Collect();
				other.GetComponent<Health>().currentHealth = 100;
			}
		}
	}
	 void CmdAdjustScore(int scoreChange)
	{
		GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSystem>().totalScore = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSystem>().totalScore+scoreChange;
		GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSystem>().text.text = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreSystem>().totalScore.ToString();
	}
	public void Collect()
	{
		if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if (collectEffect)
		{
			var effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
			Destroy(effect, 1);
		}

		//Below is space to add in your code for what happens based on the collectible type

		if (CollectibleType == CollectibleTypes.NoType) {

			//Add in code here;

			//Debug.Log ("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type1) {

			//Add in code here;

			Debug.Log("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type2) {

			//Add in code here;

			Debug.Log("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type3) {

			//Add in code here;

			Debug.Log("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type4) {

			//Add in code here;

			Debug.Log("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type5) {

			//Add in code here;

			Debug.Log("Do NoType Command");
		}
		if (gameObject.tag == "ring")
		{
			
			//PlayerController.increaseRings();
		}
		
		
			Destroy(gameObject);
		
	}
}
