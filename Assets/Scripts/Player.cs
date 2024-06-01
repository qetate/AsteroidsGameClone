using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private Bullet bulletPrefab;

    public float thrustSpeed = 1f;
    private bool thrusting;
    public bool IsThrusting => thrusting;

    public float rotationSpeed = 0.1f;
    private float turnDirection;

    public float respawnDelay = 3f;
    public float respawnInvulnerability = 3f;

    public bool screenWrapping = true;
    private Bounds screenBounds;

    /// <summary>
    /// Get required components.
    /// </summary>
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Boundries
    /// </summary>
    private void Start()
    {
        GameObject[] boundaries = GameObject.FindGameObjectsWithTag("Boundary");

        // Disable all boundaries if screen wrapping is enabled
        for (int i = 0; i < boundaries.Length; i++) 
        {
            boundaries[i].SetActive(!screenWrapping);
        }

        // Convert screen space bounds to world space bounds
        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));
    }

    /// <summary>
    /// Turn off collisions on spawn.
    /// </summary>
    private void OnEnable()
    {
        TurnOffCollisions();
        Invoke(nameof(TurnOnCollisions), respawnInvulnerability);
    }

    /// <summary>
    /// Handle player movement/actions.
    /// </summary>
    private void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            turnDirection = 1f;
        } 
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            turnDirection = -1f;
        } 
        else 
        {
            turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    /// <summary>
    /// Apply thrust, rotation, screen wrapping.
    /// </summary>
    private void FixedUpdate()
    {
        if (thrusting) 
        {
            _rigidbody.AddForce(transform.up * thrustSpeed);
        }

        if (turnDirection != 0f) 
        {
            _rigidbody.AddTorque(rotationSpeed * turnDirection);
        }

        if (screenWrapping) 
        {
            ScreenWrap();
        }
    }

    /// <summary>
    /// Moves to opposite side of screen if player exits boundary.
    /// </summary>
    private void ScreenWrap()
    {
        if (_rigidbody.position.x > screenBounds.max.x + 0.5f) 
        {
            _rigidbody.position = new Vector2(screenBounds.min.x - 0.5f, _rigidbody.position.y);
        }
        else if (_rigidbody.position.x < screenBounds.min.x - 0.5f) 
        {
            _rigidbody.position = new Vector2(screenBounds.max.x + 0.5f, _rigidbody.position.y);
        }
        else if (_rigidbody.position.y > screenBounds.max.y + 0.5f) 
        {
            _rigidbody.position = new Vector2(_rigidbody.position.x, screenBounds.min.y - 0.5f);
        }
        else if (_rigidbody.position.y < screenBounds.min.y - 0.5f) 
        {
            _rigidbody.position = new Vector2(_rigidbody.position.x, screenBounds.max.y + 0.5f);
        }
    }

    /// <summary>
    /// Instantiate and shoot bullet.
    /// </summary>
    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Shoot(transform.up);
    }

    /// <summary>
    /// Ignore collisions.
    /// </summary>
    private void TurnOffCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
    }

    /// <summary>
    /// Re-enable collisions.
    /// </summary>
    private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    /// <summary>
    /// Player dies upon collision with asteroid.
    /// </summary>
    /// <param name="collision">Collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0f;
            
            GameManager.Instance.OnPlayerDeath(this);
        }
    }
}
