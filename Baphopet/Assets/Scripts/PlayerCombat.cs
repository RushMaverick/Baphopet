using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform punchPoint;
    public Transform netPoint;
    public float punchRange = 10f;
    public float netRange = 10f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
        {
    
        }

    // Update is called once per frame
    void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Punch();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                Net();
            }
        }

    void Punch()
        {
            //Play an attack animation
            anim.SetTrigger("punch");
            //Detect what enemies are in the hitbox
            Collider2D[] hitPunch = Physics2D.OverlapCircleAll(punchPoint.position, punchRange, enemyLayers);
            //Set enemy to stunned
            foreach(Collider2D enemy in hitPunch)
            {
                enemy.GetComponent<EnemyLogic>().GetPunched(1);
                Debug.Log("Stunned " + enemy.name);
            }
        }

    void Net()
        {
            //Play a net swing animation
            anim.SetTrigger("net");
            //Detect what enemies are in the net and if they are capturable(Stunned)
            Collider2D[] hitNet = Physics2D.OverlapCircleAll(netPoint.position, netRange, enemyLayers);
            //Set enemy to captured
            foreach(Collider2D enemy in hitNet)
                {
                    if(enemy.GetComponent<EnemyLogic>().stunned)
                    {
                        Debug.Log("Captured " + enemy.name);
                        enemy.GetComponent<EnemyLogic>().Captured();
                    }
                    else
                    {
                        Debug.Log("Can't capture " + enemy.name + "!");
                    }
                    return;
                }
        }

    void OnDrawGizmosSelected()
        {
            if (punchPoint == null)
                return;
            if (netPoint == null)
                return;

            Gizmos.DrawWireSphere(punchPoint.position, punchRange);
            Gizmos.DrawWireSphere(netPoint.position, netRange);      
        }
}
