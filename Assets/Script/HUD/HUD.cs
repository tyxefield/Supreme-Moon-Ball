using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUD
{
    public Player player;
    public HUD_Control control;

    public GameObject handler;
    protected Canvas c;
    protected CanvasScaler cscaler;
    public AudioSource asource;

    //Fallout
    public GameObject youdied;
    protected Animator youdiedAN;

    //Celebrate
    public GameObject lvcompletep1;
    public GameObject lvcompletep2;
    public Animator lvcompletep1AN;
    public Animator lvcompletep2AN;

    //Fallout
    public GameObject gameoverp1;
    public GameObject gameoverp2;
    public Animator gameoverp1AN;
    public Animator gameoverp2AN;

    public bool triggerDeath;
    public bool triggerCompletion;
    public bool triggerGameOver;

    //Fade
    public GameObject fade;

    //GO
    public GameObject go;
    public Animator goAN;


    //Head
    public GameObject head;
    protected Animator headAN;

    //Scale
    public GameObject scale;
    public GameObject scalehand;
    public Animator scaleAN;

    //Lifes
    public GameObject life;
    public TMPro.TextMeshProUGUI txtlife;

    public void GO()
    {
        go.SetActive(true);
        goAN.Play("GUI_Go", 0, 0);
        return;
    }

    public void ShowDeath(AudioSource source)
    {
        youdied.SetActive(true);
        youdiedAN.Play("GUI_YouDied", 0, 0);
        headAN.Play("GUIHEAD_Death", 0, 0);
        triggerDeath = true;

        if (source != null)
        {
            source.PlayOneShot(Main.game.SOUNDS.SFX_Fall);
            source.PlayOneShot(Main.game.SOUNDS.VOC_Fall);
        }
        return;
    }

    public void ShowGameOver(AudioSource source)
    {
        if (source != null && !triggerGameOver)
        {
            source.PlayOneShot(Main.game.SOUNDS.OST_GameOver);
        }

        youdied.SetActive(false);
        gameoverp1.SetActive(true);
        gameoverp2.SetActive(true);
        gameoverp1AN.Play("GUI_LeftTextSpam", 0, 0);
        gameoverp1AN.Play("GUI_RightTextSpam", 0, 0);
        triggerGameOver = true;

        return;
    }

    public void ResetHUD()
    {
        triggerDeath = false;
        triggerCompletion = false;
        triggerGameOver = false;
        return;
    }

    public void ShowCompletion(AudioSource source)
    {
        lvcompletep1.SetActive(true);
        lvcompletep2.SetActive(true);
        lvcompletep1AN.Play("GUI_LeftTextSpam", 0, 0);
        lvcompletep2AN.Play("GUI_RightTextSpam", 0, 0);

        if (source != null)
        {
            source.PlayOneShot(Main.game.SOUNDS.OST_Victory);
        }

        triggerCompletion = true;
        return;
    }

    public void BuildInterface()
    {
        head = new GameObject("Head", typeof(Image), typeof(Animator));
        life = new GameObject("Life", typeof(TMPro.TextMeshProUGUI));
        scale = new GameObject("Scale", typeof(Image));
        scalehand = new GameObject("ScaleHand", typeof(Image), typeof(Animator));
        lvcompletep1 = new GameObject("LevelCompleteP1", typeof(Image), typeof(Animator));
        lvcompletep2 = new GameObject("LevelCompleteP2", typeof(Image), typeof(Animator));
        gameoverp1 = new GameObject("GameOverP1", typeof(Image), typeof(Animator));
        gameoverp2 = new GameObject("GameOverP2", typeof(Image), typeof(Animator));
        go = new GameObject("GO", typeof(Image), typeof(Animator));

        head.transform.SetParent(handler.transform);
        head.transform.localPosition = VectorData.zero;
        life.transform.SetParent(handler.transform);
        life.transform.localPosition = VectorData.zero;
        scale.transform.SetParent(handler.transform);
        scale.transform.localPosition = VectorData.zero;
        scale.transform.localScale = VectorData.Set(2, 2, 0);
        scalehand.transform.SetParent(scale.transform);
        scalehand.transform.localPosition = VectorData.zero;
        scalehand.transform.localScale = VectorData.Set(0.26F, 0.26F, 0);
        lvcompletep1.transform.SetParent(handler.transform);
        lvcompletep1.transform.localPosition = VectorData.zero;
        lvcompletep2.transform.SetParent(handler.transform);
        lvcompletep2.transform.localPosition = VectorData.zero;
        lvcompletep1.SetActive(false);
        lvcompletep2.SetActive(false);
        gameoverp1.transform.SetParent(handler.transform);
        gameoverp1.transform.localPosition = VectorData.zero;
        gameoverp2.transform.SetParent(handler.transform);
        gameoverp2.transform.localPosition = VectorData.zero;
        gameoverp1.SetActive(false);
        gameoverp2.SetActive(false);
        go.transform.SetParent(handler.transform);
        go.transform.localPosition = VectorData.zero;
        go.SetActive(false);

        headAN = head.GetComponent<Animator>();
        headAN.runtimeAnimatorController = Main.game.ANIMATIONS.GUI_Head;

        scale.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_Scale;
        scalehand.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_Indicator;

        lvcompletep1.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_LevelFinishP1;
        lvcompletep2.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_LevelFinishP2;
        lvcompletep1AN = lvcompletep1.GetComponent<Animator>();
        lvcompletep1AN.runtimeAnimatorController = Main.game.ANIMATIONS.GUI_LeftTextSpam;
        lvcompletep2AN = lvcompletep2.GetComponent<Animator>();
        lvcompletep2AN.runtimeAnimatorController = Main.game.ANIMATIONS.GUI_RightTextSpam;
        gameoverp1.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_GameOverP1;
        gameoverp2.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_GameOverP2;
        gameoverp1AN = gameoverp1.GetComponent<Animator>();
        gameoverp1AN.runtimeAnimatorController = Main.game.ANIMATIONS.GUI_LeftTextSpam;
        gameoverp2AN = gameoverp2.GetComponent<Animator>();
        gameoverp2AN.runtimeAnimatorController = Main.game.ANIMATIONS.GUI_RightTextSpam;
        go.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_Go;
        goAN = go.GetComponent<Animator>();
        goAN.runtimeAnimatorController = Main.game.ANIMATIONS.GUI_GO;


        txtlife = life.GetComponent<TMPro.TextMeshProUGUI>();
        txtlife.font = Main.game.TEXTURES.FONT;
        //txtlife.text = "X " + player.GetLives();
        txtlife.text = "x 3";
        txtlife.color = Color.yellow;
        txtlife.raycastTarget = false;
        txtlife.alignment = TMPro.TextAlignmentOptions.Left;

        return;
    }

    public void PrepareRects()
    {
        var rthead = head.GetComponent<RectTransform>();
        rthead.anchoredPosition = VectorData.zero;
        rthead.anchorMin = VectorData.Set2D(0, 0);
        rthead.anchorMax = VectorData.Set2D(0, 0);
        rthead.pivot = VectorData.Set2D(0, 0);

        var rtlife = life.GetComponent<RectTransform>();
        rtlife.anchoredPosition = VectorData.zero;
        rtlife.anchorMin = VectorData.Set2D(0, 0);
        rtlife.anchorMax = VectorData.Set2D(0, 0);
        rtlife.pivot = VectorData.Set2D(-0.6F, -0.5F);

        var rtscale = scale.GetComponent<RectTransform>();
        rtscale.anchoredPosition = VectorData.zero;
        rtscale.anchorMin = VectorData.Set2D(1, 0);
        rtscale.anchorMax = VectorData.Set2D(1, 0);
        rtscale.pivot = VectorData.Set2D(1.0F, 0.0F);

        return;
    }

    public HUD()
    {
        handler = new GameObject("HUD", typeof(Canvas), typeof(CanvasScaler), typeof(StandaloneInputModule), typeof(GraphicRaycaster), typeof(AudioSource),
            typeof(HUD_Control));

        c = handler.GetComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        cscaler = handler.GetComponent<CanvasScaler>();
        cscaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        control = handler.GetComponent<HUD_Control>();
        control.script = this;

        youdied = new GameObject("YouDied", typeof(Image), typeof(Animator));
        youdied.GetComponent<Image>().sprite = Main.game.TEXTURES.GUI_YouDied;
        youdiedAN = youdied.GetComponent<Animator>();
        youdiedAN.runtimeAnimatorController = Main.game.ANIMATIONS.GUI_YouDied;
        youdied.SetActive(false);

        youdied.transform.SetParent(handler.transform);
        youdied.transform.localPosition = VectorData.zero;

        asource = handler.GetComponent<AudioSource>();
        asource.loop = false;

        BuildInterface();
        PrepareRects();
    }

    public class HUD_Control : MonoBehaviour
    {
        public HUD script;

        private void Update()
        {
            if (script.player == null)
                script.player = FindObjectOfType<Player.FUNC_PLAYER>().player;
            else
            {
                script.txtlife.text = "x " + script.player.GetLives();


            }
        }
    }
}