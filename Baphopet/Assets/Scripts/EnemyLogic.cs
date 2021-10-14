using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Animator anim;
    public bool stunned;

    public int HitsLeft = 1;
    int CurrentHits;
    // Start is called before the first frame update
    void Start()
    {
        stunned = false;
        CurrentHits = HitsLeft;
    }

    public void GetPunched(int hits)
        {
            CurrentHits -= hits;
            if(CurrentHits <= 0)
                {
                    stunned = true;
                }
        }

    public void Captured()
        {
            Debug.Log("Demon captured!");
            //Play capture animation
            anim.SetTrigger("Captured");
            //Disable Enemy
            Destroy(gameObject);
        }
}
