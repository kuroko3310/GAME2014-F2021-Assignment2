using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnMenuPressed()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnStartPressed()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnTutorialPressed()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnNextPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
