using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour 
{
    public void MenuGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

}
