using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 




public class MBSTarget : MonoBehaviour
{

    [SerializeField] float vDistancefromBack;
    [SerializeField] float vDistanceBelow;
    [SerializeField] Vector2 vRandomForceLimits,vRandomTorqueLimits,vRandomForceLimitsSide;
    [SerializeField] float vStartPosLimit;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject gAura;
    [SerializeField] GameObject gGameManager;
    [SerializeField] MBSGameManager MBSGameManager;
    [SerializeField] float vOOB = -15f;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.position = FnStartPos();

        rb.AddForce(FnForce(), ForceMode.Impulse);
        rb.AddTorque(FnTorque(),ForceMode.Impulse);

        gGameManager = FindFirstObjectByType<MBSGameManager>().gameObject;
        MBSGameManager = gGameManager.GetComponent<MBSGameManager>();

    }

    // Update is called once per frame
    void Update()
    {
     
        if (transform.position.y < vOOB)
        {

            Destroy(gameObject);

        }

    }


    Vector3 FnForce()

    {

        return new Vector3(Random.Range(vRandomForceLimitsSide.x, vRandomForceLimitsSide.y), Random.Range(vRandomForceLimits.x, vRandomForceLimits.y), 0);

    }

    Vector3 FnTorque()

    {

        return new Vector3(Random.Range(vRandomTorqueLimits.x, vRandomTorqueLimits.y), Random.Range(vRandomTorqueLimits.x, vRandomTorqueLimits.y), Random.Range(vRandomTorqueLimits.x, vRandomTorqueLimits.y));

    }


    Vector3 FnStartPos()
    {

        return new Vector3(Random.Range(-vStartPosLimit, vStartPosLimit), vDistanceBelow, vDistancefromBack);
    }


    private void OnMouseOver()
    {
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        





    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        MBSGameManager.vCurrentMouseOver = null;

    }

    private void OnMouseEnter()
    {
        gAura.SetActive(true);
        MBSGameManager.vCurrentMouseOver = gameObject;
    }


    private void OnMouseExit()
    {
        gAura.SetActive(false);
        MBSGameManager.vCurrentMouseOver = null;
    }

}