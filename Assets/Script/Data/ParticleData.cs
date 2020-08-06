using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleData", menuName = "SMB/ParticleData")]
public class ParticleData : ScriptableObject
{
    [Header("Particle Data")]
    public string NAME = "Particle";
    public float lifetime = 1;
    public float duration = 1;
    public float emission = 101;
    public float scale = 1;
    public float speed = 1;
    public float gravity = 1;
    public bool cancol;
    public float damp;
    public float bounce;
    public bool istrail;
    public bool loop;

    [Header("Effects")]
    public Material mat;
    public Material trailmat;
    public Gradient color;
}
