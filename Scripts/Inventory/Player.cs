using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
                
            }

            return instance; 
        
        }
    }




    /// <summary>
    /// A reference to the inventory
    /// </summary>
    public Inventory inventory;

    public Inventory charPanel;

    /// <summary>
    /// A reference to the chest
    /// </summary>
    private Inventory chest;

    /// <summary>
    /// Shows the player what he needs to do
    /// </summary>
    public Text helperText;

    public Text statsText;

    public ItemScript[] items = new ItemScript[10];

    public int baseIntellect;
    public int baseAgility;
    public int baseStrength;
    public int baseStamina;

    private int intellect;
    private int agility;
    private int strength;
    private int stamina;



    // Use this for initialization
    void Start()
    {
        SetStats(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
	{
		HandleMovement();
		
		if (Input.GetKeyDown(KeyCode.B))
		{
			inventory.Open();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (chest != null)
			{
				chest.Open();
			}
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (charPanel != null)
			{
				charPanel.Open();
			}
		}
	}

	private void HandleMovement()
	{
	}

    /// <summary>
    /// Handles the player's collision
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item") //If we collide with an item that we can pick up
        {
            ///Picks a random category, consumeable, equipment, weapons
            int randomType = UnityEngine.Random.Range(0, 3);

            //instantiates for adding to the inventory
            GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);

            //Adds the item script to the object
            tmp.AddComponent<ItemScript>();

            //variable for selecting an item inside the category
            int randomItem;

            tmp.AddComponent<ItemScript>();

            ItemScript newEquipment = tmp.GetComponent<ItemScript>();

            switch (randomType) //Selects an item
            {
                case 0:
                    //Find selects an item
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);

                    //Ginds the item in the list
                    newEquipment.Item = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];
                    break;

                case 1:
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
                    newEquipment.Item = InventoryManager.Instance.ItemContainer.Weapons[randomItem];
                    break;
                case 2:

                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
                    newEquipment.Item = InventoryManager.Instance.ItemContainer.Equipment[randomItem];

                    break;
            }

            inventory.AddItem(newEquipment);
            Destroy(tmp); 

        }

		if (other.tag == "Chest") //If we enter a chest we need to be able to open it
		{
			helperText.gameObject.SetActive(true);
			chest = other.GetComponent<ChestScript>().chestInventory;
		}

		if (other.tag == "Material") //Creates some test materials
		{
			for (int i = 0; i < 5; i++)
			{
				for (int x = 0; x < 3; x++)
				{
					GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
					
					tmp.AddComponent<ItemScript>();
					
					ItemScript newMaterial = tmp.GetComponent<ItemScript>();
					
					newMaterial.Item = InventoryManager.Instance.ItemContainer.Materials[x];
					
					inventory.AddItem(newMaterial);
					
					Destroy(tmp);
				}
			}
		}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item") //If we collide with an item that we can pick up
        {
            if (inventory.AddItem(collision.gameObject.GetComponent<ItemScript>())) //Adds the item to the inventory.
            {
                Destroy(collision.gameObject);
            }



        }
    }

    /// <summary>
    /// Sets the player's stats
    /// </summary>
    /// <param name="agility">The player's agility</param>
    /// <param name="strength">The player's strength</param>
    /// <param name="stamina">The player's stamina</param>
    /// <param name="intellect">The player's intellect</param>
    public void SetStats(int agility, int strength, int stamina, int intellect)
    {
        this.agility = agility + baseAgility;
        this.strength = strength + baseStrength;
        this.stamina = stamina + baseStamina;
        this.intellect = intellect + baseIntellect;

        //Writes the stats text
        statsText.text = string.Format("Stamina: {0}\nStrength: {1}\nIntellect:{2}\nAgility: {3}", this.stamina, this.strength, this.intellect, this.agility);
    }
}
