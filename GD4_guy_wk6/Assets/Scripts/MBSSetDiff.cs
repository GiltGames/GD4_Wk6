using UnityEngine;
using UnityEngine.UI;
public class MBSSetDiff : MonoBehaviour
{
    [SerializeField] Button bButton;
    [SerializeField] MBSGameManager MBSGameManager;
    [SerializeField] int vDiffSet;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        bButton = GetComponent<Button>();
        bButton.onClick.AddListener( SetDiff );

       
        MBSGameManager = FindFirstObjectByType<MBSGameManager>().gameObject.GetComponent<MBSGameManager>();



    }

 void SetDiff()
    {
        
        //sets the game difficulty and sets up the initial secene

        MBSGameManager.vDifficulty = vDiffSet;

        MBSGameManager.vGameState = 1;
        MBSGameManager.vScene = 0;
        MBSGameManager.FnSceneSetup();
        MBSGameManager.FNSceneChange();


    }


}
