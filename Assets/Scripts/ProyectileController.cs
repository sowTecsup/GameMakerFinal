 using UnityEngine;

public class ProyectileController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Speed = 10;

    private GameObject Invoker;
    void Start()
    {
        rb.linearVelocity = transform.up * Speed;
        Destroy(gameObject,10);
    }
    public void Set(GameObject invoker)
    {
        Invoker = invoker;
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IDamageable>() != null && collision.gameObject != Invoker )
        {
            collision.GetComponent<IDamageable>().OnTakeDamage();
        }
    }
}
