using NUnit.Framework;
using System.Collections;
using TMPro;

using UnityEngine;


public class MBSBrew1 : MonoBehaviour
{
    [SerializeField] MBSGameManager MBSGameManager;

    [Header("Bubbles")]
    [SerializeField] GameObject gBubble;
    [SerializeField] Vector3 vBubblesPosition;
    [SerializeField] float vBubbleTime;
    [SerializeField] Color vBubbleColour;


    [Header("Herbs")]
    [SerializeField] int[] vIngredients = new int[5];
    [SerializeField] int vSequence;

    [Header("Potion Setup")]
    [SerializeField] int vNoPotions =5;
    public string[] vPotion = new string[10];
    [SerializeField] int[] vPotionScore = new int[10];
    public int[,] vPotionIngredient = new int[10, 10];
    [SerializeField] bool[] fPotionMade = new bool[10];
    [SerializeField] bool fAnyPotionMade;
    [SerializeField] TMP_Text tPotionDisplay;
    [SerializeField] float vPotionDisplayTime=3;
    [SerializeField] MBSUIAudio MBSUIAudio;
    [SerializeField] AudioClip aBubble;
    [SerializeField] AudioClip aLaugh;
    [SerializeField] AudioClip aExplode;
   



    [SerializeField] TextMeshProUGUI tLives;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MBSGameManager = FindFirstObjectByType<MBSGameManager>().gameObject.GetComponent<MBSGameManager>();

        FnPotionSetUp();
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FnPotionSetUp()
    {

     

        for (int i = 0; i < vPotion.Length; i++)
        {


            vPotionIngredient[0, 0] = 0;
            vPotionIngredient[i,0] = i;
            vPotionIngredient[i, 1] = Random.Range(0,MBSGameManager.gHerbs.Count);
            vPotionIngredient[i, 2] = Random.Range(0, MBSGameManager.gHerbs.Count);

        }

    }


    private void OnMouseEnter()
    {
        if (MBSGameManager.gCarried != null)

        {
           
            
            FnPotionIn();

            Destroy(MBSGameManager.gCarried);
        }



    }


    void FnPotionIn()
    {

        //passes parapment for colour of bubbles
        // to be dependant on what works

      

        vIngredients[vSequence] = MBSGameManager.gCarried.GetComponent<MBSHerbs>().vHerbNo;
        vSequence++;
        // is sequence complete?

        if (vSequence == MBSGameManager.vDifficulty)



        {

            

            //check each potion ingrdietn against all recipes

            for (int i = 0; i < vNoPotions; i++)
            {
                fPotionMade[i] = true;
                fAnyPotionMade = false;

                for (int j = 0; j < MBSGameManager.vDifficulty; j++)

                    if (vPotionIngredient[i, j] != vIngredients[j])

                    {
                        fPotionMade[i] = false;


                    }



                Debug.Log("Potion: " + i + " - " + vPotion[i]);
                if (fPotionMade[i])
                {
                    Debug.Log(" YES ");
                }

            }

            for (int i = 0; i < vNoPotions; i++)
            {
                if (fPotionMade[i])
                {
                    fAnyPotionMade = true;
                    MBSGameManager.FnUpdateScore(vPotionScore[i]);
                    
                    StartCoroutine(FnPotionDisplay("You made a "+vPotion[i]));

                }

            }

            if (fAnyPotionMade)
            {
                vBubbleColour = Color.blue;
                MBSUIAudio.FnPlaySFX(aLaugh);
            }
            else
            {
                vBubbleColour = Color.red;
                MBSUIAudio.FnPlaySFX(aExplode);
                MBSGameManager.vLives -= 1;

                StartCoroutine(FnPotionDisplay("Your attempt exploded"));
                tLives.text = "Lives: " + MBSGameManager.vLives;

                if (MBSGameManager.vLives <= 0)
                {
                   MBSUIAudio.FnGameOver();


                }

            }

            vSequence = 0;

        }


        else
        {
            vBubbleColour = Color.green;
            MBSUIAudio.FnPlaySFX(aBubble);
        }


        StartCoroutine(FnBubbles(vBubbleColour));



    }

    IEnumerator FnBubbles(Color Bcolor)
    {
        gBubble.SetActive(true);

        ParticleSystem gBubbleParts = gBubble.GetComponent<ParticleSystem>();

        var Main = gBubbleParts.main;
        Main.startColor = Bcolor;

        yield return new WaitForSeconds(vBubbleTime);
        gBubble.SetActive(false);
    }


    IEnumerator FnPotionDisplay(string vPotionTmp)
    {
        tPotionDisplay.text = vPotionTmp;
       



        yield return new WaitForSeconds(vPotionDisplayTime);
        tPotionDisplay.text = string.Empty;

    }

}
