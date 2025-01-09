using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MBSUIAudio : MonoBehaviour
{

    [SerializeField] AudioSource aMusic;
    [SerializeField] Slider sVol;
    [SerializeField] GameObject gGamePlayScreen, gGameOverScreen, gInitialScreen, gPauseMenu;
    [SerializeField] TMP_Text vHighScore;
    [SerializeField] TMP_Text vFinalScore;
    [SerializeField] MBSGameManager MBSGameManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aMusic = GetComponent<AudioSource>();

        FnInitialScreen();

    }

    // Update is called once per frame
    void Update()
    {
        aMusic.volume = sVol.value;
    }


    public void FnGameOver()
    {
        gGamePlayScreen.SetActive(false);
        gGameOverScreen.SetActive(true);
        vFinalScore.text = "Final Score: " + MBSGameManager.vScore;

        if (MBSGameManager.vScore > PlayerPrefs.GetInt("Highscore"))
        {

            PlayerPrefs.SetInt("Highscore",MBSGameManager.vScore);


        }


        vHighScore.text = "High Score: " + PlayerPrefs.GetInt("Highscore");




    }

    public void FnRestart()
    {

        SceneManager.LoadScene(0);



    }

    void FnInitialScreen()
    {


    }



}
