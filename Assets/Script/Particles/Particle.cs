using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Entity
{
    public ParticleSystem psys;
    public ParticleSystemRenderer render;

    public Particle(int id, float x, float y, float z) : base(x, y, z, 0)
    {
        ent = new GameObject("Particle_" + Main.game.PARTICLES.PARTICLES[id].NAME, typeof(ParticleSystem));

        psys = ent.GetComponent<ParticleSystem>();
        var pmain = psys.main;
        var pcol = psys.collision;
        var pemit = psys.emission;
        var shape = psys.shape;
        var ptrail = psys.trails;
        render = ent.GetComponent<ParticleSystemRenderer>();
        psys.Stop();
        pmain.gravityModifier = Main.game.PARTICLES.PARTICLES[id].gravity;
        pmain.startLifetime = Main.game.PARTICLES.PARTICLES[id].lifetime;
        pmain.duration = Main.game.PARTICLES.PARTICLES[id].duration;
        pmain.startSize = Main.game.PARTICLES.PARTICLES[id].scale;
        render.sharedMaterial = Main.game.PARTICLES.PARTICLES[id].mat;
        pmain.startColor = Main.game.PARTICLES.PARTICLES[id].color;
        pmain.loop = Main.game.PARTICLES.PARTICLES[id].loop;
        pmain.startSpeed = Main.game.PARTICLES.PARTICLES[id].speed;
        pemit.rateOverTime = Main.game.PARTICLES.PARTICLES[id].emission;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.1F;

        if (Main.game.PARTICLES.PARTICLES[id].cancol)
        {
            pcol.enabled = true;
            pcol.dampen = Main.game.PARTICLES.PARTICLES[id].damp;
            pcol.bounce = Main.game.PARTICLES.PARTICLES[id].bounce;
            pcol.type = ParticleSystemCollisionType.World;
        }

        if (Main.game.PARTICLES.PARTICLES[id].istrail)
        {
            ptrail.enabled = true;
            render.trailMaterial = Main.game.PARTICLES.PARTICLES[id].trailmat;
        }

        ent.transform.position = VectorData.Set(x, y, z);
        Produce();
    }

    public void Produce()
    {
        psys.Play();
        return;
    }

    public void MoveAndProduce(float x, float y, float z)
    {
        Move(x, y, z);
        Stop();
        Produce();
        return;
    }

    public void Stop()
    {
        psys.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        return;
    }
}