using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Speed;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 resultante;
            resultante = Vector3.up * Speed *  Time.deltaTime;
            transform.position += resultante;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 resultante;
            resultante = Vector3.left * Speed * Time.deltaTime;
            transform.position += resultante;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 resultante;
            resultante = Vector3.down * Speed * Time.deltaTime;
            transform.position += resultante;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 resultante;
            resultante = Vector3.right * Speed * Time.deltaTime;
            transform.position += resultante;
        }
    }
}
