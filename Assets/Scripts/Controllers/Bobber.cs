using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] Vector2 defaultPosition = Vector2.zero;

    bool isFlying = false;

    private void Start()
    {
        SetDefaultPosition(transform.position);
        lineRenderer.sortingOrder = 999;
        lineRenderer.enabled = false; // Disabled for now
    }

    public Vector2 GetPosition()
       => transform.position;

    public void AddImmediateForce(Vector2 power)
    {
        rigidBody.AddForce(power);
        isFlying = true;
    }

    public void UpdateLineRendererPosition(int index, Vector2 pos)
    => lineRenderer.SetPosition(index, pos);

    public void ResetBobber()
    {
        ResetBobberVelocity();
        ResetBobberPosition();
    }

    private void Update()
    {
        if (isFlying)
            if (rigidBody.velocity.magnitude <= 0.03f)
            {
                ResetBobberVelocity();
                isFlying = false;
                gameObject.layer = 6;
            }
    }

    void SetDefaultPosition(Vector2 newPos)
    => defaultPosition = newPos;

    void ResetBobberPosition()
        => transform.position = defaultPosition;

    void ResetBobberVelocity()
        => rigidBody.velocity = Vector2.zero;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject fish = collision.gameObject;
        if (fish.layer == 6)
        {
            // TODO - Enter Fish Clash
        }
    }
}
