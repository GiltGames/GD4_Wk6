using TMPro;
using UnityEngine;


// expands the scrolls to a UI on the scree

public class MBSExpandSrl : MonoBehaviour
{

    [SerializeField] GameObject gScroll;
    [SerializeField] TextMeshProUGUI tScroll;
    [SerializeField] TextMeshProUGUI tPotionTitle;
    [SerializeField] string[] vMenu = new string[10];
    [SerializeField] MBSGameManager MBSGameManager;
    [SerializeField] MBSBrew1 MBSBrew1;
    [SerializeField] int vPotionNo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MBSGameManager = FindFirstObjectByType<MBSGameManager>().gameObject.GetComponent<MBSGameManager>();
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        // call MBSBrew now as the caudron is now active
        
        // fins the cauldron

        MBSBrew1 = FindFirstObjectByType<MBSBrew1>().gameObject.GetComponent<MBSBrew1>();

        gScroll.SetActive(true);

        // potion title display

        tPotionTitle.text = MBSBrew1.vPotion[vPotionNo];
        
        
        // ingredient list generated for potion
        
        tScroll.text = "";
        for (int i = 0; i < MBSGameManager.vDifficulty; i++)
        {

            GameObject vHerbTmp = MBSGameManager.gHerbs[MBSBrew1.vPotionIngredient[vPotionNo, i]];
                MBSHerbs MSBHerbTemp = vHerbTmp.GetComponent<MBSHerbs>();
            string vHerbNameTmp = MSBHerbTemp.vHerbName;


            tScroll.text = tScroll.text + vHerbNameTmp + "\n";


        }


       
    }


    private void OnMouseExit()
    {
        
        //deactiveate the UI for the recipe

        tScroll.text = string.Empty;
        gScroll.SetActive(false);
    }

}
