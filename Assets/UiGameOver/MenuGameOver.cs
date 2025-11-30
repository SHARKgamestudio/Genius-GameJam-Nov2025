using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameOver : MonoBehaviour 
{
    [SerializeField] Text text;

    void Start()
    {
        text.text = $"Highest Score : {PlayerPrefs.GetInt("Highest Score")}";
    }

    public void MenuGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

}