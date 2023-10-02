using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    public static EffectSystem instance = null;

    public GameObject craftExplosionPrefab = null;
    public GameObject craftParticlesPrefab = null;
    public ParticleSystem craftDebrisParticlesPrefab = null;

    // Start is called before the first frame update
    void Start()
    {

        if(instance)
        {
            Debug.LogError("Trying to create more than one Effectsystem");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

  public void CraftExplosion(Vector3 position)
    {
        Instantiate(craftExplosionPrefab, position, Quaternion.identity);
        Instantiate(craftParticlesPrefab, position, Quaternion.identity);
        Instantiate(craftDebrisParticlesPrefab, position, Quaternion.identity);

    }
}
