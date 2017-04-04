using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

	public int MaxHealth;
	public int CurrentHealth;

	void Start () 
	{
		
		CurrentHealth = MaxHealth;
		
	}
	
	
	
	void Update () 
	{
		if (CurrentHealth <= 0) 
		{
			Destroy (gameObject);
			
		}
	}
	
	public void HurtEnemy(int damageToGive)
		
	{
		CurrentHealth -= damageToGive;
	}
	
	public void SetMaxHealth()
		
	{
		CurrentHealth = MaxHealth;
	}
}
