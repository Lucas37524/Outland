using UnityEngine;
using UnityEngine.UI;  // Import for using UI Button components

public class ToggleCursor : MonoBehaviour
{
    public Camera playerCamera;  // Reference to the player's camera
    public MonoBehaviour cameraMovementScript;  // Reference to the camera movement script (e.g. FirstPersonController)

    public Button closeButton;  // Reference to the close button of the inventory UI
    public string chestTag = "Chest";  // The tag to identify chests (you can change this tag in Unity)

    private bool isInventoryOpen = false;  // Track if the inventory is open

    // Start is called before the first frame update
    void Start()
    {
        // Start with the cursor disabled (hidden) and camera movement enabled
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Ensure the camera is moving initially
        if (cameraMovementScript != null)
            cameraMovementScript.enabled = true;

        // Add listener to the close button to hide the cursor when clicked
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideCursorAndLock);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "I" key is pressed to open/close the inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Toggle the inventory and cursor visibility
            ToggleCursorVisibility();
        }

        // Check for chest interaction (when the player clicks on the chest)
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            // Cast a ray to detect if the player clicks on a chest
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(chestTag)) // Check if the clicked object has the chest tag
                {
                    // Open the chest and show the cursor
                    OpenChest(hit.collider.gameObject);
                }
            }
        }
    }

    // Function to toggle cursor visibility and lock state
    private void ToggleCursorVisibility()
    {
        // Toggle cursor visibility
        Cursor.visible = !Cursor.visible;

        // Lock or unlock cursor based on visibility
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None; // Unlocks the cursor

            // Disable camera movement when the cursor is visible
            if (cameraMovementScript != null)
                cameraMovementScript.enabled = false;

            // Mark inventory as open
            isInventoryOpen = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center

            // Enable camera movement when the cursor is hidden
            if (cameraMovementScript != null)
                cameraMovementScript.enabled = true;

            // Mark inventory as closed
            isInventoryOpen = false;
        }
    }

    // This function hides the cursor and locks it (called by the close button)
    private void HideCursorAndLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Enable camera movement when the cursor is hidden
        if (cameraMovementScript != null)
            cameraMovementScript.enabled = true;

        // Mark inventory as closed
        isInventoryOpen = false;
    }

    // This function opens the chest and shows the cursor (called by chest interaction)
    private void OpenChest(GameObject chest)
    {
        // Here you can add any chest opening logic (animations, UI, etc.)
        // For now, we'll just show the cursor and unlock it

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Disable camera movement when the cursor is visible
        if (cameraMovementScript != null)
            cameraMovementScript.enabled = false;

        // Optionally, mark inventory as open or show a chest UI (add chest-specific UI elements here)
        isInventoryOpen = true;

        // You can also play any chest opening animation or trigger here, if needed:
        // chest.GetComponent<Animator>().SetTrigger("Open"); (For example)
    }
}
