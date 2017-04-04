 using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	//Movements
	public float moveSpeed;
	private float currentMoveSpeed;
	public float diagonalMoveModifier;

	private Animator anim;

	private Rigidbody2D myRigidbody;

	private bool playerMoving;
	public Vector2 lastMove;

	private static bool playerExists;



	//Attacking and Porjectile
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;

	public FireBall fireBall;

	public EnergyBall energyBall;

	Vector2 prevDir = Vector2.down;

	private bool attacking;
	public float attakTime;
	private float attackTimeCounter;

	//Starting and entry postion for moving through scenes
	public string startPoint;

	//Inventory



	void Start () 
	{
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	

	void Update () 
	{
		
	
		playerMoving = false;

		if(!attacking)
		{

		if (Input.GetAxisRaw ("Horizontal") > 0.5f  || Input.GetAxisRaw ("Horizontal") < -0.5f) 
			
			{
				

				myRigidbody.velocity = new Vector2(Input.GetAxisRaw ("Horizontal") * currentMoveSpeed, myRigidbody.velocity.y);
				playerMoving = true;
				lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
			}


			if (Input.GetAxisRaw ("Vertical") > 0.5f  || Input.GetAxisRaw ("Vertical") < -0.5f) 
				
			{
				

				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw ("Vertical") * currentMoveSpeed);
				playerMoving = true;
				lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
			}

			if (Input.GetAxisRaw ("Horizontal") < 0.5f && Input.GetAxisRaw ("Horizontal") > -0.5f) 
			{
				myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
			}

			if (Input.GetAxisRaw ("Vertical") < 0.5f && Input.GetAxisRaw ("Vertical") > -0.5f)
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
			}

			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				attackTimeCounter = attakTime;
				attacking = true;
				myRigidbody.velocity = Vector2.zero;
				anim.SetBool ("Attack", true);

			}
		
			if(Mathf.Abs (Input.GetAxisRaw ("Horizontal")) > 0.5f && Mathf.Abs (Input.GetAxisRaw ("Vertical")) > 0.5f)
			{
				currentMoveSpeed = moveSpeed * diagonalMoveModifier;
			} 

			else

			{
				currentMoveSpeed = moveSpeed;
			}



	}

		if (attackTimeCounter >= 0) 
		{
			attackTimeCounter -= Time.deltaTime;
		}

		if (attackTimeCounter <= 0)
		{
			attacking = false;
			anim.SetBool ("Attack",false);
		}
		anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetBool ("PlayerMoving", playerMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);

		 
		Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		prevDir = (movement_vector.Equals(Vector2.zero)) ? prevDir : movement_vector;

		if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			FireBall tmp = (FireBall)Instantiate(fireBall, transform.position, Quaternion.identity);

			tmp.Dir(prevDir.normalized);

		}


	}

}


	