using System.Collections;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField] Collider2D collider2d;
    float moveSpeed;
    bool moveTarget = false;
    Vector3 newTarget = Vector2.zero;

    public float scoreValue = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(0.5f, 3.0f);

        newTarget = new Vector3(Random.Range(-3.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0.0f);
        moveTarget = true;

        Vector2 dir = newTarget - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, newTarget, Time.deltaTime * moveSpeed);

            if (transform.position == newTarget)
            {
                moveTarget = false;
                StartCoroutine("CR_Wander");
            }
        }
    }

    IEnumerator CR_Wander()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 8.0f));

        newTarget = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f);
        moveTarget = true;

        Vector2 dir = newTarget - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bobber")
        {
            MessageBroker.SendMessage(new MSG_ScoreUpdated("FishCaught", scoreValue));
            Destroy(this.gameObject);
        }
    }
}
