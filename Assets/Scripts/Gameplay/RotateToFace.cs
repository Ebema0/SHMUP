using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFace : MonoBehaviour
{
    public bool facePlayer = false;
    public bool faceTarget = false;
    public GameObject target = null;
    public bool active = false;

    public void Activate()
    {
        active = true;
    }

    public void DeActivate()
    {
        active = false;
    }

    void FixedUpdate()
    {
        if(active)
        {
            if(facePlayer || faceTarget)
            {
                Vector2 targetPosition = vector.zero
                    if (facePlayer)
                {
                    if (!GameManager.instance || !GameManager.instance.playerOneCraft)
                        return;

                    targetPosition = GameManager.instance.playerOneCraft.trasform.position;
                }
                    else if (faceTarget)
                {
                    if (target == null)
                        return;

                    targetPosition = target.transform.position;
                }

                direction.Normalize();
                transform.rotation = Quaternion.LokkRotation(Vector3.forward, direction);
            }
        }
    }

}
