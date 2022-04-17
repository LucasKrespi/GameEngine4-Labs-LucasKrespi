using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameoverscene : MonoBehaviour
{
    public TextMeshProUGUI killnumber;
    // Start is called before the first frame update
    void Start()
    {
        killnumber.text = PlayerPrefs.GetInt("Kills").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
