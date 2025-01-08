using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rSpriteRenderer = GetComponent<SpriteRenderer>();
        vFadeColour = rSpriteRenderer.color;
        vStrongColour = vFadeColour;
        vStrongColour.a = 1f;

        gGameManager = FindFirstObjectByType<MBSGameManager>().gameObject;
        MBSGameManager = gGameManager.GetComponent<MBSGameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
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



    private void OnMouseDrag()
    {
        Vector3 vCurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        transform.position = vScreenPosStart + vCurMousePos - vMousePosStart;

       

    }





}
