using UnityEngine;
using System.Collections;

public class HurtEnemy : MonoBehaviour {

	public int damageToGive;
	public GameObject damageBurst;



	void Start () {
	
	}
	

	void Update () {



	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy (damageToGive);

			Instantiate (damageBurst, transform.position, transform.rotation); 
			Destroy (gameObject);
		}

		if (other.gameObject.tag =="Object")
		{
			Instantiate (damageBurst, transform.position, transform.rotation);
			Destroy (gameObject);

		}
	
	}
}
