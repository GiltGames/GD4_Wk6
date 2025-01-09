using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine.UI;

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

    public GameObject gCarried;
    [SerializeField] GameObject[] gTargetsD;

    [SerializeField] float vTimer;
    [SerializeField] float vLength;
    [SerializeField] MBSUIAudio MBSUIAudio;

    [Header("Herbs")]
    public List<GameObject> gHerbs;
    [SerializeField] Vector2 vHerbSpawnInt;


    [Header("UI")]
    public GameObject gGameplayScreen, gGameOverScreen, gPauseScreen, gInitialScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //spawning flying objects

        if (vGameState == 1)
        {
            FnSceneSetup();
            FNSceneChange();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
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

     /*   for (int i = 0; i < tSlot.Length; i++)
        {
            if (MBSInventory.vCarried[i] != null)
            {
                tSlot[i].text = MBSInventory.vCarried[i].name;

            }
            else
            {
                tSlot[i].text = string.Empty;

            }

        }

        */
    }


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

    IEnumerator FnSpawnHerb()


    {

        while (vScene != 4 && vGameState == 1)
        {
            //Random object scene
            // create random object

            int vObjIndTmp = Random.Range(0, gHerbs.Count);

            Instantiate(gHerbs[vObjIndTmp]);







            // wait

            float vWaitTmp = Random.Range(vHerbSpawnInt.x, vHerbSpawnInt.y);
            yield return new WaitForSeconds(vWaitTmp);



        }



        yield return null;


    }


    void FnSceneSetup()

    {


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
        vSceneName[0] = "Garden";
        vSceneName[1] = "Inside";
        vSceneName[2] = "Forest";
        vSceneName[3] = "Graveyard";
        vSceneName[4] = "River";
        
     


    }

    public void FnSceneButton0()
    {
        vScene = vNextScene0;
        FNSceneChange();


    }


    public void FnSceneButton1()
    {
        vScene = vNextScene1;
        FNSceneChange();


    }

    public void FnSceneButton2()
    {
        vScene = vNextScene2;
        FNSceneChange();


    }



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

        //Special Scene activities


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


        //river scence

        if (vScene ==4)
            {
            StartCoroutine(CRSpawnFlying());
        }
        

        //all but river and inside

        if(vScene !=4 && vScene!=1)

        {
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
        vGameState = 3;



    }

    public void FnUnPause()
    {


        vGameState = 1;


    }


    



}
