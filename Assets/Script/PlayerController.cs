using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded;
    public UIController controller;
    public Camera mainCamera;
    public LayerMask groundLayer;
    public Transform aimIndicator;
    public GameObject bulletPrefab, swordSlashPrefab;   
    public Transform firePoint;       
    public float fireRate = 0.2f;
    NavMeshAgent agent;
    private float nextFireTime = 0.1f;
    float timer = 0;
    bool canFire;
    int weaponType =0;
    int damage = 5;
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
      
    }

    void Start()
    {
        controller = GameObject.Find("Canvas").GetComponent<UIController>();
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        RotateTowardsMouse();
        Move();
        useWeapon();
    }

    void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }
        }
    }

    void Move()
    {
        float moveX = 0f;
        float moveZ = 0f;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            moveSpeed = 10;
        else moveSpeed = 5;

        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        else agent.isStopped = true;
            Vector3 moveDir = (transform.forward * moveZ + transform.right * moveX).normalized;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    
    void useWeapon()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (weaponType == 0)
                weaponType = 1;
            else
                weaponType = 0;
        }

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > nextFireTime)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (weaponType == 1)
            Shoot();
        else
            Sword();
    }

    void Sword()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            swordSlashPrefab.GetComponent<ParticleSystem>().Play();
            canFire = false;
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && canFire) 
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            canFire = false;
        }
    }

 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding");
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerStats.Instance.TakeDamage(damage);

        }
    }
}
