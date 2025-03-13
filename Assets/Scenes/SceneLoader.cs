using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
