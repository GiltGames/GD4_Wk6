using TMPro.EditorUtilities;
using TMPro;
using UnityEngine;
using TMPro.SpriteAssetUtilities;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MBSInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject[] vCarried;
    [SerializeField] GameObject gGameManager;
    [SerializeField] MBSGameManager MBSGameManager;
    [SerializeField] TMP_Text tSlot;
    [SerializeField] Color vColorStart;
    [SerializeField] Image I;
    






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gGameManager = FindFirstObjectByType<MBSGameManager>().gameObject;
        MBSGameManager = gGameManager.GetComponent<MBSGameManager>();
        vColorStart = I.color;
    }

    // Update is called once per frame
    void Update()
    {



    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        
        I.color = Color.red;
        


        if (MBSGameManager.gCarried != null)
        {

            tSlot.text = MBSGameManager.gCarried.tag;
            Destroy(MBSGameManager.gCarried);
        I.color = vColorStart;

        }
    }




 public    void OnPointerExit(PointerEventData eventData)
    {
        I.color = vColorStart;  
    }



}
