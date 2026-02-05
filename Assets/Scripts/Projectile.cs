using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10 ;

    public Rigidbody2D rb;
    void Start()
    {
       // rb.AddForce(transform.up * Speed, ForceMode2D.Impulse);

        rb.linearVelocity = transform.up * Speed;

        Destroy(gameObject,10);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            rb.linearVelocity = Vector2.zero;
            Invoke(nameof(Disapear), 1);
        }
    }
    public void Disapear()
    {
        Destroy(gameObject);
    }

}
