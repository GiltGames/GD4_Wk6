
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MBSHerbs : MonoBehaviour
{
    [SerializeField] SpriteRenderer rSpriteRenderer;
    [SerializeField] Color vStrongColour;
    [SerializeField] Color vFadeColour;
    [SerializeField] float vDragSpeed;
    [SerializeField] Vector3 vScreenPosStart;
    [SerializeField] Vector3 vMousePosStart;
    [SerializeField] Vector3 vOffset;
    [SerializeField] float vDropLimitX;
    [SerializeField] GameObject gGameManager;
    [SerializeField] MBSGameManager MBSGameManager;
    public int vHerbNo;
    public string vHerbName;
    public Texture2D vImage;

    [SerializeField] float vDistancefromBack;
    [SerializeField] float vDistanceBelow;
   
    [SerializeField] float vStartPosLimit;


    [SerializeField] float vCheckDistance;
    [SerializeField] bool fValidSpawn;
    public bool fFromInventory;
    [SerializeField] float vRecreateOffset = -1;
    public bool fMouseClickOn;
    public bool fInInventory;
   public float vInstantDistIn;
    [SerializeField] GameObject gIDObj;
    [SerializeField] TextMeshProUGUI tIDObj;
    [SerializeField] Vector3 vCurMousePos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rSpriteRenderer = GetComponent<SpriteRenderer>();
        vFadeColour = rSpriteRenderer.color;
        vStrongColour = vFadeColour;
        vStrongColour.a = 1f;

     
        gGameManager = FindFirstObjectByType<MBSGameManager>().gameObject;
        MBSGameManager = gGameManager.GetComponent<MBSGameManager>();

        //If from inventory set to mouse position, otherwise set to random instatiate and allow if valid area


        if (fFromInventory)

        {
            transform.position = FnInventoryPos();
            fFromInventory = false;
            Debug.Log("Returned Start Position" + transform.position);

        }

        else
        {

            transform.position = FnStartPos();

            

            if (!fValidSpawn)
            { Destroy(gameObject); }

        }






    }

    // Update is called once per frame
    void Update()
    {
        
        //reset mouse button

        if (fMouseClickOn)
        {

            Vector3 vCurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = vScreenPosStart + vCurMousePos - vMousePosStart;

        }


        if (Input.GetMouseButtonUp(0))
        {

            fMouseClickOn = false;
        }


    }

    private void OnMouseEnter()
    {
        
        rSpriteRenderer.color = vStrongColour;


    }


    private void OnMouseExit()
    {

        rSpriteRenderer.color = vFadeColour;

    }

    private void OnMouseDown()
    {
        vScreenPosStart = transform.position;
       vMousePosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vOffset = vScreenPosStart - vMousePosStart;

        MBSGameManager.gCarried = gameObject;




    }



   /* private void OnMouseDrag()
    {
        Vector3 vCurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        transform.position = vScreenPosStart + vCurMousePos - vMousePosStart;

       

    }

    */
    private void OnMouseOver()
    {
        //tIDObj.text = gameObject.name;
        
        
        if (Input.GetMouseButton(0) )
        { 
       vCurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vCurMousePos = new Vector3(vCurMousePos.x,vCurMousePos.y,vInstantDistIn);

            Debug.Log("Mouse is now at " + vCurMousePos +"when it started at" + vMousePosStart);
            Vector3 vNewPosTmp = vScreenPosStart + vCurMousePos - vMousePosStart;

            transform.position = new Vector3(vNewPosTmp.x, vNewPosTmp.y, vInstantDistIn);
           
    }

    }

    


    Vector3 FnStartPos()
    {
        fValidSpawn = false;

        Vector3 vPosTmp = new Vector3(Random.Range(-vStartPosLimit, vStartPosLimit), Random.Range(-vDistanceBelow, vDistanceBelow), vDistancefromBack);

        Physics.Raycast(vPosTmp, new Vector3(0, 0, 1), out RaycastHit hit, vCheckDistance);

      //  Debug.Log("Spawn attempt at" + vPosTmp);


        if (hit.collider != null)
        {
            if (hit.collider.tag == "Valid")
            {

                fValidSpawn = true;

                
            }
        }
        return vPosTmp;
    }


    Vector3 FnInventoryPos()
    {
        Vector3 vPosTmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log("Create position called");

        vPosTmp = new Vector3(vPosTmp.x + vRecreateOffset, vPosTmp.y,vInstantDistIn);
        
        Debug.Log("start pos"+vPosTmp);

        vMousePosStart = vPosTmp;
        vScreenPosStart = vPosTmp;
        rSpriteRenderer.color = vStrongColour;

     //   gIDObj = GameObject.FindWithTag("IDDisp");
      //  tIDObj = gIDObj.GetComponent<TextMeshProUGUI>();


        return vPosTmp;
    }
}
