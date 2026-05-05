using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LoadSceneButton : MonoBehaviour
{
    [SerializeField] int sceneIndex;

    public void OnClick()
    {
        if (sceneIndex <= -1)
        {
            SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        { 
            SceneLoader.Instance.LoadScene(sceneIndex);
        }
    }
}
