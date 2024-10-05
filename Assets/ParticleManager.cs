using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] public List<GameObject> particles;

    void Start()
    {
        timer = 2f;
    }

    void Update()
    {
        if (timer < 0 && particles.Count != 0)
        {
            foreach (var p in particles)
            {
                Destroy(p);
            }
            particles.Clear();
            timer = 2;
        }

        if ( particles.Count != 0)
        {
            timer -= Time.deltaTime;
        }
    }
}