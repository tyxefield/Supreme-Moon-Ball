using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl
{
    public SoundFunc control;
    private GameObject handler;
    private AudioSource aeffect;
    private AudioSource amusic;

    public SoundControl()
    {
        handler = new GameObject("AudioControl", typeof(SoundFunc));
        aeffect = handler.AddComponent<AudioSource>();
        amusic = handler.AddComponent<AudioSource>();

        control = handler.GetComponent<SoundFunc>();
        control.script = this;

        amusic.clip = Main.game.SOUNDS.OST_Game;
        amusic.loop = true;
        amusic.Play();
        amusic.volume = 0.4F;
    }


    public class SoundFunc : MonoBehaviour
    {
        public SoundControl script;

        public void GameOver()
        {
            script.amusic.Play();
        }

        private void Update()
        {
            if (Main.game.isGameOver)
            {
                if (script.amusic.clip != Main.game.SOUNDS.OST_GameOver)
                    script.amusic.clip = Main.game.SOUNDS.OST_GameOver;
            }
            else
                script.amusic.clip = Main.game.SOUNDS.OST_Game;

        }
    }
}
