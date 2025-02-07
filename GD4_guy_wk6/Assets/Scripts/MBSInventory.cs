
using TMPro;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using JetBrains.Annotations;


// This script manages taking items on and off the Inventory slots in the UI


public class MBSInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject[] vCarried;
    [SerializeField] GameObject gGameManager;
    [SerializeField] MBSGameManager MBSGameManager;
    [SerializeField] TMP_Text tSlot;
    [SerializeField] Color vColorStart;
    [SerializeField] Image I;
    [SerializeField] Image vHerbHeld;
    [SerializeField] int vHerbIndex;
    [SerializeField] bool fMouseOver;
    [SerializeField] Vector3 vPosCreate;
    






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gGameManager = FindFirstObjectByType<MBSGameManager>().gameObject;
        MBSGameManager = gGameManager.GetComponent<MBSGameManager>();
        vColorStart = I.color;
        vHerbIndex = 99;
    }

    // Update is called once per frame
    void Update()
    {
        //hecks if mouse is over the item slot


        if (fMouseOver)

        {
            FnWhileIn();



        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {

        // If over item slot set flag to true

        I.color = Color.red;

        fMouseOver = true;










    }




 public    void OnPointerExit(PointerEventData eventData)
    {
        // On leaving item slot set flag to false
        
        I.color = vColorStart;

        fMouseOver = false;

    }


   public void FnWhileIn()
    {


        // Is anything being carried by the mouse?

        if (MBSGameManager.gCarried != null)
        {

            // checked for button release

            if (Input.GetMouseButtonUp(0))
            {


                string vNameTmp = MBSGameManager.gCarried.GetComponent<MBSHerbs>().vHerbName;



                tSlot.text = vNameTmp;
                Texture2D vImageTmp = MBSGameManager.gCarried.GetComponent<MBSHerbs>().vImage;

                Sprite spritetmp = Sprite.Create(vImageTmp, new Rect(0, 0, vImageTmp.width, vImageTmp.height), new Vector2(0.5f, 0.5f));
                vHerbHeld.sprite = spritetmp;

                Destroy(MBSGameManager.gCarried);
                I.color = vColorStart;


                vHerbIndex = MBSGameManager.gCarried.GetComponent<MBSHerbs>().vHerbNo;



            }
        }

        else
        {

            FnGetHerb();



        }

       

    }
    public void FnGetHerb()
    {

        // is there anything in the inventory slot?
        if (vHerbIndex < 99)

        {

            // create a copy of the invenory object in the world ande set parameters so it is is not reset in position by MBSHerb on Start
            //c
            if (Input.GetMouseButtonDown(0))


            {

                vPosCreate = Camera.main.ScreenToWorldPoint(Input.mousePosition);




                MBSGameManager.gCarried = Instantiate(MBSGameManager.gHerbs[vHerbIndex]);
                MBSGameManager.gCarried.GetComponent<MBSHerbs>().transform.position = new Vector3(vPosCreate.x, vPosCreate.y, MBSGameManager.gCarried.GetComponent<MBSHerbs>().vInstantDistIn);
                MBSGameManager.gCarried.GetComponent<MBSHerbs>().vHerbNo = vHerbIndex;
                MBSGameManager.gCarried.GetComponent<MBSHerbs>().fFromInventory = true;
                MBSGameManager.gCarried.GetComponent<MBSHerbs>().fMouseClickOn = true;

                Debug.Log("Created at" + MBSGameManager.gCarried.GetComponent<MBSHerbs>().transform.position);
                //change inventory display
                vHerbIndex = 99;
                vHerbHeld.sprite = null;
                tSlot.text = string.Empty;
                MBSGameManager.gCarried.GetComponent<MBSHerbs>().name = "New Herb";



            }






        }


    }

}
