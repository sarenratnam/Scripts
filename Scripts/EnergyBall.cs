using UnityEngine;
using System.Collections;

public class EnergyBall : MonoBehaviour {

	public float speed = 1;
	Vector3 dir = Vector2.zero;
	public void Dir(Vector3 value)
	{
		if (value.Equals(Vector3.zero)) Destroy(gameObject);// if we have zero direction we destroy this gameojbect;
		//rotate our sprite towards direction of moving
		float angle = Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	void Update()
	{
		transform.position += transform.right * speed * Time.deltaTime;
	}
}
