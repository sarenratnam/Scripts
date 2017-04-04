using UnityEngine;
using System.Collections;

public class LoadNewArea : MonoBehaviour {

	public string levelToLoad;

	public string exitPoint;

	private PlayerController thePlayer;
	void Start () 
	{
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	
	{
		if (other.gameObject.name == "Player") {

			Application.LoadLevel (levelToLoad);
			thePlayer.startPoint = exitPoint;
		}
	}
	
}
