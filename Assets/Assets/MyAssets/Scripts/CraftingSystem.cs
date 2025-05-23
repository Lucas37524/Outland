using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI, survivalScreenUI, refineScreenUI, foodScreenUI;

    public Rigidbody playerRb; // Reference to player's Rigidbody

    public List<string> inventoryItemList = new List<string>();

    //Category Buttons
    Button toolsBTN, survivalBTN, refineBTN, foodBTN, exitToolBTN, exitSurvivalBTN, exitRefineBTN, exitFoodBTN;

    //Craft Buttons
    Button craftAxeBTN;
    Button craftPlankBTN;
    Button craftfireStarterBTN;
    Button craftcookedFishBTN;
    Button craftPieceBTN;
    Button craftBonfireBTN;

    //Requirement Text
    public TMP_Text AxeReq1, AxeReq2, PlankReq1, FishReq1, FishReq2, StarterReq1, StarterReq2, PieceReq1, BonfireReq1, BonfireReq2;

    public bool isOpen;

    //All Blueprints
    public Blueprint AxeBLP = new Blueprint("Axe", 2, "Stone", 3, "Stick", 3);
    public Blueprint PlankBLP = new Blueprint("Plank", 1, "Log", 1, "", 0);
    public Blueprint fireStarterBLP = new Blueprint("Fire Starter", 1, "Sticks", 3, "Stone", 1);
    public Blueprint cookedFishBLP = new Blueprint("Cooked Fish", 1, "Fish", 1, "Fire Starter", 1);
    public Blueprint PieceBLP = new Blueprint("Piece", 1, "Plank", 5, "", 0);
    public Blueprint BonfireBLP = new Blueprint("Bonfire", 2, "Piece", 5, "Starter", 3);

    public static CraftingSystem instance { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        survivalBTN = craftingScreenUI.transform.Find("SurvivalButton").GetComponent<Button>();
        survivalBTN.onClick.AddListener(delegate { OpenSurvivalCategory(); });

        refineBTN = craftingScreenUI.transform.Find("RefineButton").GetComponent<Button>();
        refineBTN.onClick.AddListener(delegate { OpenRefineCategory(); });

        foodBTN = craftingScreenUI.transform.Find("FoodButton").GetComponent<Button>();
        foodBTN.onClick.AddListener(delegate { OpenFoodCategory(); });

        exitToolBTN = toolsScreenUI.transform.Find("ExitButton").GetComponent<Button>();
        exitToolBTN.onClick.AddListener(delegate { OpenCraftingCategory(); });

        exitSurvivalBTN = survivalScreenUI.transform.Find("ExitButton").GetComponent<Button>();
        exitSurvivalBTN.onClick.AddListener(delegate { OpenCraftingCategory(); });

        exitRefineBTN = refineScreenUI.transform.Find("ExitButton").GetComponent<Button>();
        exitRefineBTN.onClick.AddListener(delegate { OpenCraftingCategory(); });

        exitFoodBTN = foodScreenUI.transform.Find("ExitButton").GetComponent<Button>();
        exitFoodBTN.onClick.AddListener(delegate { OpenCraftingCategory(); });

        // Axe Requirements
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TMP_Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TMP_Text>();

        // Fish Requirements
        FishReq1 = foodScreenUI.transform.Find("Fish").transform.Find("req1").GetComponent<TMP_Text>();
        FishReq2 = foodScreenUI.transform.Find("Fish").transform.Find("req2").GetComponent<TMP_Text>();

        // Piece Requirements
        PieceReq1 = refineScreenUI.transform.Find("Piece").transform.Find("req1").GetComponent<TMP_Text>();

        // Starter Requirements
        StarterReq1 = survivalScreenUI.transform.Find("Starter").transform.Find("req1").GetComponent<TMP_Text>();
        StarterReq2 = survivalScreenUI.transform.Find("Starter").transform.Find("req2").GetComponent<TMP_Text>();

        // Bonfire Requirements
        BonfireReq1 = survivalScreenUI.transform.Find("Bonfire").transform.Find("req1").GetComponent<TMP_Text>();
        BonfireReq2 = survivalScreenUI.transform.Find("Bonfire").transform.Find("req2").GetComponent<TMP_Text>();



        // Craft Button
        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });

        craftPlankBTN = refineScreenUI.transform.Find("Plank").transform.Find("Button").GetComponent<Button>();
        craftPlankBTN.onClick.AddListener(delegate { CraftAnyItem(PlankBLP); });

        craftfireStarterBTN = survivalScreenUI.transform.Find("Starter").transform.Find("Button").GetComponent<Button>();
        craftfireStarterBTN.onClick.AddListener(delegate { CraftAnyItem(fireStarterBLP); });

        craftcookedFishBTN = foodScreenUI.transform.Find("Fish").transform.Find("Button").GetComponent<Button>();
        craftcookedFishBTN.onClick.AddListener(delegate { CraftAnyItem(cookedFishBLP); });

        craftPieceBTN = refineScreenUI.transform.Find("Piece").transform.Find("Button").GetComponent<Button>();
        craftPieceBTN.onClick.AddListener(delegate { CraftAnyItem(PieceBLP); });

        craftBonfireBTN = survivalScreenUI.transform.Find("Bonfire").transform.Find("Button").GetComponent<Button>();
        craftBonfireBTN.onClick.AddListener(delegate { CraftAnyItem(BonfireBLP); });
    }

    void OpenCraftingCategory()
    {
        craftingScreenUI.SetActive(true);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        foodScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        foodScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
        Debug.Log("Tools category opened!");
    }

    void OpenSurvivalCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        foodScreenUI.SetActive(false);
        survivalScreenUI.SetActive(true);
        Debug.Log("Survival category opened!");
    }

    void OpenRefineCategory()
    {
        craftingScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        foodScreenUI.SetActive(false);
        refineScreenUI.SetActive(true);
        Debug.Log("Refine category opened!");
    }

    void OpenFoodCategory()
    {
        craftingScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        foodScreenUI.SetActive(true);
        Debug.Log("Refine category opened!");
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        StartCoroutine(Calculate());
    }

    public IEnumerator Calculate()
    {
        yield return null; // no delay
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
            FreezePlayerRotation();
            Debug.Log("U is pressed - Opening Crafting UI");
        }
        else if (Input.GetKeyDown(KeyCode.U) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpen = false;
            UnfreezePlayerRotation();
            Debug.Log("U is pressed - Closing Crafting UI");
        }
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;
        int log_count = 0;
        int fish_count = 0;
        int starter_count = 0;
        int plank_count = 0;
        int piece_count = 0;


        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;

                case "Stick":
                    stick_count += 1;
                    break;

                case "Log":
                    log_count += 1;
                    break;

                case "Starter":
                    starter_count += 1;
                    break;

                case "Fish":
                    fish_count += 1;
                    break;

                case "Plank":
                    plank_count += 1;
                    break;

                case "Piece":
                    piece_count += 1;
                    break;
            }
        }

        // ---- AXE ---- //
        AxeReq1.text = "3 stone [" + stone_count + "]";
        AxeReq2.text = "3 stick [" + stick_count + "]";

        // Check if requirements are met
        if (stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
            Debug.Log("Axe button visible! Requirements met.");
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
            Debug.Log("Axe button hidden. Requirements not met.");
        }

        // ---- PLANK ---- //
        PlankReq1.text = "1 log [" + log_count + "]";

        // Check if requirements are met
        if (log_count >= 1)
        {
            craftPlankBTN.gameObject.SetActive(true);
            Debug.Log("Plank button visible! Requirements met.");
        }
        else
        {
            craftPlankBTN.gameObject.SetActive(false);
            Debug.Log("Plank button hidden. Requirements not met.");
        }


        // ---- PIECE ---- //
        PieceReq1.text = "5 planks [" + plank_count + "]";

        // Check if requirements are met
        if (plank_count >= 5)
        {
            craftPieceBTN.gameObject.SetActive(true);
            Debug.Log("Piece button visible! Requirements met.");
        }
        else
        {
            craftPieceBTN.gameObject.SetActive(false);
            Debug.Log("Piece button hidden. Requirements not met.");
        }

        // ---- COOKED FISH ---- //
        FishReq1.text = "1 fish [" + fish_count + "]";
        FishReq2.text = "1 starter [" + starter_count + "]";

        // Check if requirements are met
        if (fish_count >= 1 && starter_count >= 1)
        {
            craftcookedFishBTN.gameObject.SetActive(true);
            Debug.Log("Fish button visible! Requirements met.");
        }
        else
        {
            craftcookedFishBTN.gameObject.SetActive(false);
            Debug.Log("Fish button hidden. Requirements not met.");
        }

        // ---- FIRE STARTER ---- //
        StarterReq1.text = "3 sticks [" + stick_count + "]";
        StarterReq2.text = "1 stone [" + stone_count + "]";

        // Check if requirements are met
        if (stick_count >= 3 && stone_count >= 1)
        {
            craftfireStarterBTN.gameObject.SetActive(true);
            Debug.Log("Starter button visible! Requirements met.");
        }
        else
        {
            craftfireStarterBTN.gameObject.SetActive(false);
            Debug.Log("Starter button hidden. Requirements not met.");
        }

        // ---- BONFIRE ---- //
        BonfireReq1.text = "5 pieces [" + piece_count + "]";
        BonfireReq2.text = "3 starters [" + starter_count + "]";

        // Check if requirements are met
        if (piece_count >= 5 && starter_count >= 3)
        {
            craftBonfireBTN.gameObject.SetActive(true);
            Debug.Log("Bonfire button visible! Requirements met.");
        }
        else
        {
            craftBonfireBTN.gameObject.SetActive(false);
            Debug.Log("Bonfire button hidden. Requirements not met.");
        }
    }

    public void FreezePlayerRotation()
    {
        if (playerRb != null)
        {
            playerRb.constraints |= RigidbodyConstraints.FreezeRotationY; // Freeze Y rotation
        }
    }

    public void UnfreezePlayerRotation()
    {
        if (playerRb != null)
        {
            playerRb.constraints &= ~RigidbodyConstraints.FreezeRotationY; // Unfreeze Y rotation
        }
    }
}


