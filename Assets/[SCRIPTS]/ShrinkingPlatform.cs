using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{

    
   

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            
            StartCoroutine(Shrink());
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            StartCoroutine(Revert());
        }
    }

    private IEnumerator Shrink()
    {

        yield return new WaitForSeconds(0.5f);
        transform.localScale = new Vector3(0.5f, 0.5f);



    }

    private IEnumerator Revert()
    {
       
            yield return new WaitForSeconds(5);
            transform.localScale = new Vector3(1, 1);
       
       
    }
}
