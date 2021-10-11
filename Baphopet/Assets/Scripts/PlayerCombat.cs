using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform punchPoint; 
    public float punchRange = 10f;
    public LayerMask punchLayers;
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
    }

    void Punch()
    {
        //Play an attack animation
        anim.SetTrigger("punch");
        //Detect what enemies are in the hitbox
        Collider2D[] hitPunch = Physics2D.OverlapCircleAll(punchPoint.position, punchRange, punchLayers);
        //Set enemy to stunned
        foreach(Collider2D punch in hitPunch)
        {
            Debug.Log("Stunned " + punch.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (punchPoint == null)
            return;

        Gizmos.DrawWireSphere(punchPoint.position, punchRange);    
    }
}
