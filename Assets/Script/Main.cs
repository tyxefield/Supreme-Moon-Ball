using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main
{
    public static string GAMENAME = "SUPREME MOON BALL";
    public static double VERSION = 0.01D;
    public static Main game;

    public ANIM_Data ANIMATIONS = Resources.Load<ANIM_Data>("DATA/ANIM_Data");
    public TextureData TEXTURES = Resources.Load<TextureData>("DATA/TextureData");
    public ParticleList PARTICLES = Resources.Load<ParticleList>("DATA/ParticleList");
    public Sounds SOUNDS = Resources.Load<Sounds>("DATA/Sounds");

    public TickSystem tick;

    public SoundControl audio;
    public HUD hud;
    public Level level;

    public bool isGameOver;

    public static void ResetGame()
    {
        Level.STAGE = 0;
        Level.CURRENT_LEVEL = 0;
        SceneManager.LoadScene("Title");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
        return;
    }

    public Main()
    {
        game = this;
        RunGame();
        audio = new SoundControl();
        tick = new TickSystem();
        hud = new HUD();
        level = new Level();
    }

    public void RunGame()
    {
        new Player(0, 0, 0, 0);
    }
}