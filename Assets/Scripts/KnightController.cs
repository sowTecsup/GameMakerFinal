using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public interface IDamageable
{
    public void OnTakeDamage()
    {

    }
}



public class KnightController : MonoBehaviour, IDamageable
{
    public float Speed = 5;
    public bool IsMoving = false;

    public Animator Anim;

    public Transform WeaponAnchor;

    public GameObject ProyectilePrefab;
    public Transform ProyectileSpawnPosition;

    public Rigidbody2D Rb;

    void Start()
    {
        
    }
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector2(horizontalInput, verticalInput);


        if (horizontalInput != 0 || verticalInput != 0)
        {
            IsMoving = true;
//            transform.position += moveDir * Speed * Time.deltaTime;
            Rb.linearVelocity = moveDir * Speed;
        }
        else
        {
            IsMoving = false;
        }
        Anim.SetBool("IsMoving", IsMoving);

        ProjectileMechanic();

        Mirror(horizontalInput);
    }
    public void Mirror(float x)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (x > 0)     
            renderer.flipX = false;
        else if (x < 0)     
            renderer.flipX = true;
    }

    public void ProjectileMechanic()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos-  (Vector2)transform.position).normalized;

        WeaponAnchor.up = dir;


       //print(mousePos);


        if(Input.GetMouseButtonDown(0))
        {
            GameObject proyectile =  Instantiate(ProyectilePrefab, ProyectileSpawnPosition.transform.position,Quaternion.identity);
            proyectile.transform.up = dir;
            proyectile.GetComponent<ProyectileController>().Set(gameObject);
        }
    }

    public void OnTakeDamage()
    {
        print("Oh no me golpearon");
    }

}
