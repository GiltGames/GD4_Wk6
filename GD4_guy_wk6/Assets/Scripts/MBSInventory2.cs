using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MBSInventory2 : MonoBehaviour
{

    [SerializeField] bool fMouseOver;
    [SerializeField] MBSInventory MBSInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fMouseOver)

        {
           MBSInventory.FnWhileIn();



        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

       

        fMouseOver = true;
        Debug.Log("Mouse over image itself");










    }

    public void OnPointerExit(PointerEventData eventData)
    {
      

        fMouseOver = false;

    }





}
