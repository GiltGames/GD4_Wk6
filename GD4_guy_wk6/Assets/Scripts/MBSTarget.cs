using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[RequireComponent(typeof(Rigidbody))]




public class MBSTarget : MonoBehaviour
{

    [SerializeField] float vDistancefromBack;
    [SerializeField] float vDistanceBelow;
    [SerializeField] Vector2 vRandomForceLimits, vRandomTorqueLimits, vRandomForceLimitsSide;
    [SerializeField] float vStartPosLimit;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject gAura;
    [SerializeField] GameObject gGameManager;
    [SerializeField] MBSGameManager MBSGameManager;
    [SerializeField] float vOOB = -15f;
    [SerializeField] int vValue;
    [SerializeField] GameObject gLightningSource;
    [SerializeField] GameObject gLightning;
    [SerializeField] GameObject gBatwingSource;
    [SerializeField] GameObject gBatwing;
    [SerializeField] AudioClip aLightning;
    [SerializeField] MBSFXMarker MBSFXMarker;
    [SerializeField] AudioSource aSFX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.position = FnStartPos();

        rb.AddForce(FnForce(), ForceMode.Impulse);
        rb.AddTorque(FnTorque(),ForceMode.Impulse);

        gGameManager = FindFirstObjectByType<MBSGameManager>().gameObject;
        MBSGameManager = gGameManager.GetComponent<MBSGameManager>();

        MBSFXMarker = FindFirstObjectByType<MBSFXMarker>().GetComponent<MBSFXMarker>();
        aSFX = MBSFXMarker.GetComponent<AudioSource>();
        aSFX.clip = aLightning;

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
        MBSGameManager.FnUpdateScore(vValue);
        gLightning = Instantiate(gLightningSource, transform.position, Quaternion.identity);
        Destroy(gLightning, 0.5f);
        aSFX.Play();

        gBatwing=Instantiate(gBatwingSource, transform.position, Quaternion.identity);


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
