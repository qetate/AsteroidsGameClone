using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Asteroid : MonoBehaviour
{
    // Array of asteroid sprites.
    [SerializeField]
    private Sprite[] sprites;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 30f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Assign random properties to each asteroid for variety.
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // Scale and mass is based on asteroid size.
        transform.localScale = Vector3.one * size;
        _rigidbody.mass = size;

        // Destroy asteroid if it reaches maxLifetime.
        Destroy(gameObject, maxLifetime);
    }

    /// <summary>
    /// Set trajectory of asteroid.
    /// </summary>
    /// <param name="direction">Direction asteroid will move.</param>
    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * movementSpeed);
    }

    /// <summary>
    /// Called when asteroid collides with another objct.
    /// </summary>
    /// <param name="collision">Collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collision was with bullet.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Split asteroid on collision if it is large enough.
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            // GameManager notification that asteroid was destroyed.
            GameManager.Instance.OnAsteroidDestroyed(this);

            // Destroy asteroid.
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Used to create smaller asteroids when a large asteroid splits.
    /// </summary>
    /// <returns>New smaller asteroid.</returns>
    private Asteroid CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized);

        return half;
    }

}