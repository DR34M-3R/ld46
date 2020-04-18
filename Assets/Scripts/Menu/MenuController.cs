using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _lvlChoosePanel;
    
    public void InvertLvlPanel()
    {
        _lvlChoosePanel.SetActive(!_lvlChoosePanel.active);
    }

    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
