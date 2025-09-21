using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public int damage = 10;
    public float lifeTime = 3f;
    public bool isSword;
    private Rigidbody rb;

    void Start()
    {
        if(!isSword)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * speed;
            Destroy(gameObject, lifeTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision:");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        
        Destroy(gameObject);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
