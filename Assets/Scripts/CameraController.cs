using UnityEditor.XR;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float CameraSpeed = 5;

    public float ChaseRange = 5;//->2.5 para arriba 5 para los costados
    public float IdleRange = 1;

    public bool ChaseMode = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Target.position,transform.position)> ChaseRange)
        {
            ChaseMode = true;
        }

        if (Vector2.Distance(Target.position, transform.position) < IdleRange)
        {
            ChaseMode = false;
        }



        if (ChaseMode)
        {
            Vector2 dir = (Target.transform.position - transform.position).normalized;

            transform.position += (Vector3)dir * CameraSpeed * Time.deltaTime;
        }

    }
}
