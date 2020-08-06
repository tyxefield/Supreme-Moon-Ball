using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitThanks : MonoBehaviour
{
    public GameObject fade;
    private float ticks;
    private bool end;

    void Update()
    {
        ticks += Time.deltaTime;

        if (ticks > 7 && !end)
        {
            fade.GetComponent<Animator>().Play("GUI_FadeIn", 0, 0);
            end = true;
        }

        if (ticks > 9)
        {
            Main.ResetGame();
        }

    }
}
