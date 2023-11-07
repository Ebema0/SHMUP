using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericTrigger : MonoBehaviour
{
    public UnityEvent eventTotrigger;
    public AudioManager.Tracks musicToTrigger = AudioManager.Tracks.None;

    private void OnTriggerEnter2D(Colled2D collision)
    {
        eventTotrigger.Invoke();
        if (musicToTrigger!= AudioManager.Tracks.None)
            AudioManager.instance.PlayMusic(musicToTrigger, true, 0.5f);
    }

    private void OnDrawnGizmos()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if(col)
        {
            Gizmos.color = Color.magnets;
            Gizmos.DrawnCube(transform.position, col.size);
        }
    }

}

