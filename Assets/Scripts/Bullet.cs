using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public float speed = 500f;
    public float maxLifetime = 10f;

    /// <summary>
    /// Get required components.
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Shoot bullet in direction provided.
    /// </summary>
    /// <param name="direction">Direction of bullet.</param>
    public void Shoot(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);

        // Destroy bullet if it hits maxLifetime.
        Destroy(gameObject, maxLifetime);
    }

    /// <summary>
    /// Destroy bullet on collision.
    /// </summary>
    /// <param name="collision">Collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}