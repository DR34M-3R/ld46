using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseView : MonoBehaviour
{
    [SerializeField]
    private string _menuScene;
    [SerializeField]
    private GameObject _pausePanel;
    
    public bool IsActive
    {
        get { return _pausePanel.active; }

        set { _pausePanel.SetActive(value);}
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(_menuScene);
    }
}
