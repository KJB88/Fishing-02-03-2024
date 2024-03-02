using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] Vector2 defaultPosition = Vector2.zero;

    private void Start()
    {
        SetDefaultPosition(transform.position);
        lineRenderer.sortingOrder = 999;
    }

    public Vector2 GetPosition()
       => transform.position;

    public void SetDefaultPosition(Vector2 newPos)
        => defaultPosition = newPos;

    public void AddImmediateForce(Vector2 power)
        => rigidBody.AddForce(power);

    public void ResetBobberPosition()
        => transform.position = defaultPosition;

    public void ResetBobberVelocity()
        => rigidBody.velocity = Vector2.zero;

    public void UpdateLineRendererPosition(int index, Vector2 pos)
        => lineRenderer.SetPosition(index, pos);

    public void ResetBobber()
    {
        ResetBobberVelocity();
        ResetBobberPosition();
    }
}
