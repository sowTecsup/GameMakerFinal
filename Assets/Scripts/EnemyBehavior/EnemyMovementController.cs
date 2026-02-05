using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public enum EnemyMovementType
{
    Chase,
    Flee
}

public enum EnemyState
{
    Idle,
    Acting,
    Alert,
}


public class EnemyMovementController : MonoBehaviour
{
    public GameObject Target;
    public EnemyMovementType Behavior;
    public EnemyState State;    
    public Animator Anim;


    public float Speed;
    [Range(1, 10)]
    [Tooltip("La distancia con la que se va activar su comportamiento , siempre debe ser menor  que el ignore range!")]
    public float InteractRange;

    [Range(0, 10)]
    [Tooltip("La distancia con la que se va desactivar su comportamiento")]
    public float IgnoreRange;

    [Tooltip("Despues de cuanto tiempo queremos que regrese a su posicion Inicial?")]
    [Range(2,10)]public float TimeToReset;


    private float resetSpeed;
    private float currentTimeToReset;
    private Vector3 originPos;
    void Start()
    {
        gameObject.tag = "Enemy";
        originPos = transform.position;
        resetSpeed = Speed * 2;

        if (!Target)
            Target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float interactDistance = Vector3.Distance(Target.transform.position, transform.position);
        if(interactDistance <= InteractRange && (State == EnemyState.Idle || State == EnemyState.Alert))
            State = EnemyState.Acting;



        switch (State)
        {
            case EnemyState.Idle:
                {
                    currentTimeToReset = 0;

                    float originPointDistance = Vector3.Distance(originPos, transform.position);
                    if(originPointDistance >= 0.2f)
                        OnReset();

                    Anim.SetBool("IsMoving", false);
                }
                break;
            case EnemyState.Acting:
                {
                    currentTimeToReset = 0;

                    float ignoreDistance = Vector3.Distance(Target.transform.position, transform.position);
                    if (ignoreDistance >= IgnoreRange && State == EnemyState.Acting)
                        State = EnemyState.Alert;

                    OnMove(Behavior);

                    Anim.SetBool("IsMoving", true);
                }
                break;
            case EnemyState.Alert:
                {
                    currentTimeToReset += Time.deltaTime;
                    if(currentTimeToReset >= TimeToReset)
                    {
                        State = EnemyState.Idle;
                        currentTimeToReset = 0;
                    }
                    Anim.SetBool("IsMoving", false);
                }
                break;
            default:
                break;
        }
        Mirror();
    }
    public void Mirror()
    {
        float x = transform.position.x;
        float targetX = Target.transform.position.x;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.flipX =( x >= targetX) ? true : false;
    }

    public void OnMove(EnemyMovementType type)
    {
        Vector2 dir = (Target.transform.position - transform.position).normalized;
        switch (type)
        {
            case EnemyMovementType.Chase:
                if(Vector2.Distance(Target.transform.position , transform.position)>0.8f)
                    transform.position += (Vector3)dir * Speed * Time.deltaTime;
                break;
            case EnemyMovementType.Flee:
                transform.position += (Vector3)(-dir) * Speed * Time.deltaTime;
                break;
        }
      
    }

    public void OnReset()
    {
        Vector3 dir = ( originPos - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, InteractRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, IgnoreRange);

    }
}
