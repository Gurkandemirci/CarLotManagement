using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool isGameEnded = false;
    public void NextLevel()
    {
        if(MoneyManager.instance.nextLevelMoney <= MoneyManager.instance.money)
        {
            isGameEnded = true;
            SceneManager.LoadScene("SecondScene");
        }
    }
    
}
