using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject controlUI;

    private void Awake()
    {
        // Finds ALL GameObjects, including inactive ones
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "ControlsUI") // Replace with your exact object name
            {
                controlUI = obj;
                break;
            }
        }

        if (controlUI == null)
        {
            Debug.LogWarning("ControlsUI not found, zawg!");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Controls()
    {
        if (controlUI != null)
            controlUI.SetActive(true);
    }

    public void Exit()
    {
        if (controlUI != null)
            controlUI.SetActive(false);
    }
}