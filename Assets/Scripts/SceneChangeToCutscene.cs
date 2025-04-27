using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToCutscene : MonoBehaviour
{
    public string sceneName; // The name of the scene to load

    public void loadScene()
    {
        if (SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
        {
            Debug.LogWarning(sceneName +" doesnt Exists");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}