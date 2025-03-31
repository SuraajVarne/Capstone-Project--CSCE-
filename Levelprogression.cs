// Level Progression System
// Team Members: Suraj Varne Sheela
// Description: Manages level progression and saves player progress

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    private int maxLevel = 5;

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("SavedLevel", 1);
    }

    public void CompleteLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            PlayerPrefs.SetInt("SavedLevel", currentLevel);
            PlayerPrefs.Save();
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevel <= maxLevel)
        {
            SceneManager.LoadScene("Level" + currentLevel);
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("SavedLevel", 1);
        PlayerPrefs.Save();
    }
}
