using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Meele,
    Range
}

public class EnemyAttackController : MonoBehaviour , IDamageable
{
    public GameObject Target;


    public AttackType AttackType;
    [Range(0 , 5)]public float AttackCooldown;
    [Range(0, 10)] public float AttackRange;

    [Tooltip("Si tu ataque es de rango")]public float ProyectileSpeed;
    [Tooltip("Si tu ataque es de rango")] public List<GameObject> ProyectileList;

    


    private float currentAttackCooldown;
    private bool AttackEnable = true;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(Target.transform.position, transform.position);

        if (distance <= AttackRange)
        {
            EnableAttackMechanism();

            if(AttackEnable)
            {
                switch (AttackType)
                {
                    case AttackType.Meele:
                        {
                            Target.GetComponent<IDamageable>().OnTakeDamage();
                        }
                        break;
                    case AttackType.Range:
                        {
                            Vector2 dir =  (Target.transform.position - transform.position).normalized;

                            GameObject proyectile = Instantiate(ProyectileList[Random.Range(0, ProyectileList.Count)],transform.position,Quaternion.identity);
                            proyectile.transform.up = dir;
                            proyectile.GetComponent<ProyectileController>().Set(gameObject);
                        }
                        break;
                }


                AttackEnable = false;
            }
        }

        
    }
    public void EnableAttackMechanism()
    {
        if (!AttackEnable)
        {
            currentAttackCooldown += Time.deltaTime;
            if (currentAttackCooldown >= AttackCooldown)
            {
                AttackEnable = true;
                currentAttackCooldown = 0;
            }
        }
    }
    public void OnTakeDamage()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
