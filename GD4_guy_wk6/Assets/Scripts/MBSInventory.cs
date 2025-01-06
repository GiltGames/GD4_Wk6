using UnityEngine;

public class MBSInventory : MonoBehaviour
{

    public GameObject[] vCarried;
    [SerializeField] GameObject gGameManager;
    [SerializeField] MBSGameManager MBSGameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gGameManager = FindFirstObjectByType<MBSGameManager>().gameObject;
        MBSGameManager = gGameManager.GetComponent<MBSGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        



    }
}
