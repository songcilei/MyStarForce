using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    private ParticleSystem sys;
    [Range(0,2)]
    public float time = 0;
    void Start()
    {
        sys = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        sys.Simulate(time);
    }
}
