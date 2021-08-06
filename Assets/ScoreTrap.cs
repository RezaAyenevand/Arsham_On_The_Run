using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTrap : MonoBehaviour
{
    [SerializeField] public GameObject targetPose;
    [SerializeField] public float lighTriggerDistance;
    [SerializeField] public GameObject trapLight;
    [SerializeField] public GameSession scoreDisplay;
    private UnityEngine.Experimental.Rendering.Universal.Light2D bacheLight;
    private Transform robbieLightTransform;
    bool isLightenUp = false;
    Spawner spawner;
    private bool syncer = false;

    GameObject player;
    BoxCollider2D objectCollider;
    float critical;


    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = FindObjectOfType<GameSession>();
        robbieLightTransform = FindObjectOfType<lightcurve>().gameObject.transform;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        objectCollider = gameObject.GetComponent<BoxCollider2D>();
        objectCollider.isTrigger = true;
        Transform lastPlat = FindObjectOfType<Spawner>().lastPlat.transform;
        Vector2 lastLpos = transform.position - lastPlat.position;
        transform.parent = lastPlat;
        //transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 0.7f);
        transform.localPosition = new Vector2(lastLpos.x, lastLpos.y);
    }

    private void FixedUpdate()
    {
        if (!isLightenUp && Vector2.Distance(robbieLightTransform.position, this.transform.position) < lighTriggerDistance)
        {
            Instantiate(trapLight, new Vector2(this.transform.position.x, this.transform.position.y - 0.4f), Quaternion.identity);
            isLightenUp = true;
        }
        if (syncer)
        {
            //light = this.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>()
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == player)
        {
            scoreDisplay.resetMul();
        }
    }
    IEnumerator DeathHandler()
    {
        yield return new WaitForSeconds(1.15f);
        SceneManager.LoadScene(2);
    }
}
