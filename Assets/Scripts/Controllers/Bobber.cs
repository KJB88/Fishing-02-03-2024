using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] Vector2 defaultPosition = Vector2.zero;

    bool isTravelling = false;

    private void Start()
    {
        lineRenderer.sortingOrder = 999;
        lineRenderer.enabled = false; // Disabled for now
    }

    public Vector2 GetPosition()
       => transform.position;

    public Vector2 GetDefaultPosition()
   => defaultPosition;

    public void AddImmediateForce(Vector2 power)
        => rigidBody.AddForce(power);

    public void UpdateLineRendererPosition(int index, Vector2 pos)
    => lineRenderer.SetPosition(index, pos);

    public void ResetBobber()
    {
        ResetBobberVelocity();
        ResetBobberPosition();
    }

    //private void Update()
    //{
    //    if (rigidBody.velocity.magnitude <= 0.03)
    //    {
    //        if (isTravelling)
    //        {
    //            MessageBroker.SendMessage(new Message("BobberStopped"));
    //            isTravelling = false;
    //        }
    //    }
    //    else if (rigidBody.velocity.magnitude > 0.03)
    //    {
    //        isTravelling = true;
    //    }
    //}

    void ResetBobberPosition()
        => transform.position = defaultPosition;

    void ResetBobberVelocity()
        => rigidBody.velocity = Vector2.zero;
}
