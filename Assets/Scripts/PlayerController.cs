using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    public float speed;
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce ,ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 resultante;
            resultante = Vector3.left * speed * Time.deltaTime;
            transform.position += resultante;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 resultante;
            resultante = Vector3.right * speed * Time.deltaTime;
            transform.position += resultante;
        }
    }
}
