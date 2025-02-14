using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    public GameObject inventoryScreenUI;
    public bool isItOpen = false;
    public Rigidbody playerRb; // Reference to player's Rigidbody

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    //public bool isFull;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }



    void Start()
    {
        isOpen = false;

        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isItOpen = !isItOpen;
        inventoryScreenUI.SetActive(isItOpen);

        if (isItOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            FreezePlayerRotation();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UnfreezePlayerRotation();
        }
    }

    void FreezePlayerRotation()
    {
        if (playerRb != null)
        {
            playerRb.constraints |= RigidbodyConstraints.FreezeRotationY; // Freeze Y rotation
        }
    }

    void UnfreezePlayerRotation()
    {
        if (playerRb != null)
        {
            playerRb.constraints &= ~RigidbodyConstraints.FreezeRotationY; // Unfreeze Y rotation
        }
    }

    public void AddToInventory(string itemName)
    {
            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(itemName);
    }

    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {

            if(slot.transform.childCount == 0)
            {
                return slot;
            }     
        }
        return new GameObject();
    }


    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if(counter == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
