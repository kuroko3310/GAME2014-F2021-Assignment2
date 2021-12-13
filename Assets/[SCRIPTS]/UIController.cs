/*  Source file name: UIController.cs
 *  Author's name: Jen Marc Capistrano
 *  Student number: 101218004
 *  Date last modified: 12 December 2021
 *  Program description: This script is mainly for scene transition and buttons function
 *  Revision history: Added functions for UI buttons
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
   
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
