using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
   private void OnDrawGizmos()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col)
        {
            OnDrawGizmos().color = color.red;
            OnDrawGizmos().DrawCube(trasform.position, col.size);
        }
    }
}
