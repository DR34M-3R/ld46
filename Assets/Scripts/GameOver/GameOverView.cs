using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{

    [SerializeField]
    private string _menuScene;
    
    public void ReplayButton()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(_menuScene);
    }
}
