using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Animator anim;
    public bool stunned;
    private Transform Captor; //Targets where Player is
    public int HitsLeft = 1;
    int CurrentHits;
    // Start is called before the first frame update
    void Start()
    {
        Captor = FindObjectOfType<PlayerMovement>().transform; //Location of Player
        stunned = false;
        CurrentHits = HitsLeft;
        Debug.Log("Player is" + Captor);
    }


    public void GetPunched(int hits)
        {
            CurrentHits -= hits;
            KnockedBack();

            if(CurrentHits <= 0)
                {
                    stunned = true;
                }
        }
    
    public void KnockedBack()
        {
            ////Play Stunned Animation
            anim.SetTrigger("Stun");
            Debug.Log("Knocked Back");
            //Knockback
            Vector2 difference = transform.position - Captor.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    public void Captured()
        {
            Debug.Log("Demon captured!");
            //Play capture animation
            anim.SetTrigger("Captured");
        }
    
    public void OnDeathAnimationFinished()
        {
            Destroy(gameObject);
        }
}
