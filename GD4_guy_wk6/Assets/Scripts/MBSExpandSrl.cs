using TMPro;
using UnityEngine;

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
        
        MBSBrew1 = FindFirstObjectByType<MBSBrew1>().gameObject.GetComponent<MBSBrew1>();

        gScroll.SetActive(true);
        tPotionTitle.text = MBSBrew1.vPotion[vPotionNo];
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
        gScroll.SetActive(false);
        tScroll.text = string.Empty;
    }

}