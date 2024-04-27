using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    private void Awake()
    {
        if(playButton != null)
        {
            playButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.GameScene);
            });
        }
        
        if(quitButton != null)
        {
            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        Time.timeScale = 1f;
    }

    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No hay más escenas para cargar. Considera reiniciar al inicio.");
        }
    }
}
