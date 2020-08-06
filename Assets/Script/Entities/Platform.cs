using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] private byte type;
    [SerializeField] private byte behave;
    [SerializeField] private int SCORE_SPAWNS;
    [SerializeField] private int RANGE_SCORE = 5;
    [SerializeField] private bool isGoal;
    public bool randomSpeed;
    [Space]
    public bool isFan;
    [SerializeField] private GameObject fanobj;

    public int GetID() { return type; }

    private float move;

    public void Behave()
    {
    }

    public void Control()
    {
    }

    private void Start()
    {
        /*if (SCORE_SPAWNS > 0)
        {
            for (int i = 0; i < SCORE_SPAWNS; i++)
            {
                new ScoreItem(null, 0, transform.position.x + Random.Range(0, RANGE_SCORE),
                                       transform.position.y + transform.localScale.y + 0.5F,
                                       transform.position.z + Random.Range(0, RANGE_SCORE));
            }
        }*/

        if (isGoal)
        {
            new Goal(transform.position.x, transform.position.y + transform.localScale.y - 4.7F, transform.position.z);
        }


        if (behave == 1)
        {
            Animator an = gameObject.AddComponent<Animator>();
            an.applyRootMotion = true;
            an.updateMode = AnimatorUpdateMode.UnscaledTime;
            an.runtimeAnimatorController = Main.game.ANIMATIONS.PLAT_Xmove;

            float rand = Random.Range(0.3F, 0.7F);

            if (randomSpeed)
                an.speed = rand;
        }
    }


    public void Break()
    {
        Object.Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (isFan)
        {
            fanobj.transform.Rotate(0, 0, 360 * Time.deltaTime);
        }

    }
}