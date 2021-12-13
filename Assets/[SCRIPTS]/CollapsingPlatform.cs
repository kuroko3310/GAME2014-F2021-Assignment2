using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag( "Player"))
        {
            StartCoroutine(Collapse());
        }
    }

   private IEnumerator Collapse()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
