using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine.UI;
using System.Globalization;

public class MBSGameManager : MonoBehaviour
{

    public int vDifficulty;

    public List<GameObject> vTargets;
    [SerializeField] Vector2 vSpawnTimeLimits;
    public int vGameState;
    public GameObject vCurrentMouseOver;
    [SerializeField] TextMeshProUGUI tIDObject;
   // [SerializeField] MBSInventory MBSInventory;
    [SerializeField] TextMeshProUGUI[] tSlot;
    public int vScore;
    [SerializeField] int vLivesMax;
    public int vLives;
    [SerializeField] TMP_Text tScore;
    [SerializeField] MeshRenderer mBackground;
    [SerializeField] int[,] vSceneMap = new int[9, 9];
    [SerializeField] string[] vSceneName = new string[9];
    public int vScene = 0;
    [SerializeField] GameObject tButton0;
    [SerializeField] GameObject tButton1;
    [SerializeField] GameObject tButton2;
    [SerializeField] TextMeshProUGUI tButton0Txt;
    [SerializeField] TextMeshProUGUI tButton1Txt;
    [SerializeField] TextMeshProUGUI tButton2Txt;
    public int vNextScene0;
    public int vNextScene1;
    public int vNextScene2;
    [SerializeField] Material[] vSceneBackground;
    [SerializeField] AudioClip[] aSceneMusic;
    [SerializeField] AudioSource aSource;
    [SerializeField] List<GameObject> gValidSpawnArea;
    [SerializeField] int[,] vHerbinScene = new int[10,10];
    [SerializeField] int[] vNoofHerbsinScene = new int[10];
    [SerializeField] int[] vStartingHerbs = new int[10];
    


    public GameObject gCarried;
    [SerializeField] GameObject[] gTargetsD;

    [SerializeField] float vTimer;
    [SerializeField] float vLength;
    [SerializeField] MBSUIAudio MBSUIAudio;

    [Header("Herbs")]
    public List<GameObject> gHerbs;
    [SerializeField] Vector2 vHerbSpawnInt;


    [Header("UI")]
    public GameObject gGameplayScreen, gGameOverScreen, gPauseScreen, gRestartScreen, gInitialScreen, gOptionScreen, gInitialOptions;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        vLives = vLivesMax;
        FnSceneSetup();
        gValidSpawnArea[vScene].SetActive(true);
        //spawning flying objects

     
    }

    // Update is called once per frame
    void Update()
    {
       
        // is game running - Gamestte : 0 not strarted, 1 running, 2 paused, 3 over



        // timer runs down

        if (vGameState ==1)

        {
            vTimer = vTimer+ Time.deltaTime;


        }

        if (vTimer > vLength)
        {

            vGameState = 3;
            MBSUIAudio.FnGameOver();



        }

        
        //Display Object Name
        if (vCurrentMouseOver != null)
        {
            tIDObject.text = vCurrentMouseOver.name;
        }

        else
        {
            tIDObject.text = string.Empty;
        }

    
    }


    //spawn bat function

    IEnumerator CRSpawnFlying()


    {

        while (vScene ==4 && vGameState ==1)
        {
            //Random object scene
            // create random object

            int vObjIndTmp = Random.Range(0, vTargets.Count);

            Instantiate(vTargets[vObjIndTmp]);

            // wait

            float vWaitTmp = Random.Range(vSpawnTimeLimits.x, vSpawnTimeLimits.y);
            yield return new WaitForSeconds(vWaitTmp);



        }



        yield return null;


    }


    // spawn herbs function at the start of the scene

    void FnSpawnHerbInitial()
    {


        if (vScene != 4 && vGameState == 1 && vStartingHerbs[vScore] > 0)
        {

            for (int i = 0; i < vStartingHerbs[vScene]; i++)
            {

                int vObjIndTmp = Random.Range(0, vNoofHerbsinScene[vScene]);

                int vHerbTmp = vHerbinScene[vScene, vObjIndTmp];

                if (vHerbTmp < 99)
                {

                    Instantiate(gHerbs[vHerbTmp]);


                }


            }



        }


    }



    // spawn herbs function 

    IEnumerator FnSpawnHerb()


    {





        while (vScene != 4 && vGameState == 1)
        {
            //Random object scene
            // create random object
            // check not 99, for null herbs
            if (vNoofHerbsinScene[vScene] < 10)
            {

                int vObjIndTmp = Random.Range(0, vNoofHerbsinScene[vScene]);

                int vHerbTmp = vHerbinScene[vScene, vObjIndTmp];

                if (vHerbTmp < 99)
                {

                    Instantiate(gHerbs[vHerbTmp]);


                }




                // wait

                float vWaitTmp = Random.Range(vHerbSpawnInt.x, vHerbSpawnInt.y);
                yield return new WaitForSeconds(vWaitTmp);



            }

        }

        yield return null;


    }


    // initial game set up for scenes


    public void FnSceneSetup()

    {
        // sets up all of the scenes

        // how they map onto each other

        vSceneMap[0, 0] = 1;
        vSceneMap[0, 1] = 2;
        vSceneMap[0, 2] = 3;
        vSceneMap[1, 0] = 0;
        vSceneMap[1, 1] = 99;
        vSceneMap[1, 2] = 99;
        vSceneMap[2, 0] = 99;
        vSceneMap[2, 1] = 3;
        vSceneMap[2, 2] = 0;
        vSceneMap[3, 0] = 99;
        vSceneMap[3, 1] = 2;
        vSceneMap[3, 2] = 4;
        vSceneMap[4, 0] = 99;
        vSceneMap[4, 1] = 3;
        vSceneMap[4, 2] = 99;
        
        // 99 is code for null
     

      
    
        // herbs in scne 0
        vHerbinScene[0, 0] = 0;
            vHerbinScene[0, 1] = 1;
            vHerbinScene[0, 2] = 2;


        //herbs in scene 2
            vHerbinScene[2, 1] = 2;
        vHerbinScene[2, 0] = 1  ;
        //herbs in scene3
        
        vHerbinScene[3, 0] = 3;
        vHerbinScene[3, 1] = 4;

        //null herb in scene 1 (Cauldron)
            vHerbinScene[1, 0] = 99;

        
    }

    // check buttons on screen and change scene

    public void FnSceneButton0()
    {
        gValidSpawnArea[vScene].SetActive(false);
        vScene = vNextScene0;
        FNSceneChange();
        gValidSpawnArea[vScene].SetActive(true);

    }


    public void FnSceneButton1()
    {
        gValidSpawnArea[vScene].SetActive(false);
        vScene = vNextScene1;
        FNSceneChange();
        gValidSpawnArea[vScene].SetActive(true);

    }

    public void FnSceneButton2()
    {
        gValidSpawnArea[vScene].SetActive(false);
        vScene = vNextScene2;
        FNSceneChange();
        gValidSpawnArea[vScene].SetActive(true);

    }




    // Mains scene set up on new scene
    public void FNSceneChange()
    {

        mBackground.material = vSceneBackground[vScene];
        aSource.clip = aSceneMusic[vScene];
        aSource.Play(); 


        if (vSceneMap[vScene, 0] == 99)
        {
            tButton0.SetActive(false);
        }
        else
        {
            tButton0.SetActive(true);
            tButton0Txt.text = vSceneName[vSceneMap[vScene, 0]];

        }

        if (vSceneMap[vScene, 1] == 99)
        {
            tButton1.SetActive(false);
        }
        else
        {
            tButton1.SetActive(true);
            tButton1Txt.text = vSceneName[vSceneMap[vScene, 1]];

        }

        if (vSceneMap[vScene, 2] == 99)
        {
            tButton2.SetActive(false);
        }
        else
        {
            tButton2.SetActive(true);
            tButton2Txt.text = vSceneName[vSceneMap[vScene, 2]];

        }

        vNextScene0 = vSceneMap[vScene, 0];
        vNextScene1 = vSceneMap[vScene, 1];
        vNextScene2 = vSceneMap[vScene, 2];

        
        //destoy objects on change of scene

   MBSTarget[] gTargets = FindObjectsByType<MBSTarget>(FindObjectsSortMode.None);


   foreach (MBSTarget target in gTargets) 
            {
            Destroy(target.gameObject);

        }

        MBSHerbs[] gHerbs = FindObjectsByType<MBSHerbs>(FindObjectsSortMode.None);


        foreach (MBSHerbs herb in gHerbs)
        {
            Destroy(herb.gameObject);

        }

        //Special Scene activities
        //river scence

        if (vScene ==4)
            {
            StartCoroutine(CRSpawnFlying());
        }
        

        //all but river and inside

        if(vScene !=4 && vScene!=1)

        {
            FnSpawnHerbInitial();
            StartCoroutine(FnSpawnHerb());
        }

    }



    public void FnUpdateScore(int vUpdateTmp)
    {

        vScore = vScore + vUpdateTmp;
        tScore.text = "Score: " + vScore;

    }


    public void FnPause()

    {
        vGameState = 2;
        Time.timeScale = 0;
        gGameplayScreen.SetActive(false);
        gPauseScreen.SetActive(true);
        gRestartScreen.SetActive(true);
        gOptionScreen.SetActive(true);
        gInitialOptions.SetActive(false);


    }

    public void FnUnPause()
    {


        vGameState = 1;
        Time.timeScale = 1;
        gPauseScreen.SetActive(false);
        gRestartScreen.SetActive(false);
        gGameplayScreen.SetActive(true);
        gOptionScreen.SetActive(false);
        gInitialOptions.SetActive(true);

    }






}
