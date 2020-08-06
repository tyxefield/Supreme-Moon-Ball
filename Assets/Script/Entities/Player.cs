using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public FUNC_PLAYER control;
    public GameObject pivcam;
    public GameObject objcam;
    public GameObject bubble;
    public Camera cam;

    protected SphereCollider col;
    public Rigidbody rb;

    public GameObject handL;
    public GameObject handR;
    public GameObject pivhandL;
    public GameObject pivhandR;
    public SpriteRenderer imghandL;
    public SpriteRenderer imghandR;
    protected Animator anHandL;
    protected Animator anHandR;
    public AudioSource asource;

    protected bool celebrating; //is finishing the level!

    protected GameObject shadow;
    protected SpriteRenderer imgshadow;

    public GameObject melvin;
    public SpriteRenderer imgmelvin;
    public Animator anmelvin;

    protected bool fallout;
    public bool grounded;

    protected MeshRenderer mbubble;

    protected Particle[] P_STARS_PLAYER;

    protected int LIVES = 4;

    protected int SCORE;
    protected int TIME_SECONDS;
    protected int TIME_MINUTES;

    public Vector3 SPAWN_POINT;

    public int SetLives(int LIVES) { return this.LIVES = LIVES; }
    public int GetLives() { return LIVES; }

    private float FALL_LIMIT = -124F;

    public Player(float x, float y, float z, float rot) : base(x, y, z, rot)
    {
        ent = new GameObject("Player", typeof(SphereCollider), typeof(Rigidbody), typeof(FUNC_PLAYER), typeof(AudioSource));
        pivcam = new GameObject("PivCam", typeof(Animator));
        objcam = new GameObject("Camera", typeof(Camera), typeof(AudioListener));
        pivhandL = new GameObject("PivHandL");
        pivhandR = new GameObject("PivHandR");
        handL = new GameObject("HandL", typeof(SpriteRenderer), typeof(Animator));
        handR = new GameObject("HandR", typeof(SpriteRenderer), typeof(Animator));
        bubble = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        melvin = new GameObject("Melvin", typeof(SpriteRenderer), typeof(Animator));
        shadow = new GameObject("Shadow", typeof(SpriteRenderer));

        control = ent.GetComponent<FUNC_PLAYER>();
        control.player = this;

        asource = ent.GetComponent<AudioSource>();
        asource.loop = false;


        ent.transform.position = VectorData.Set(x, y, z);
        pivcam.transform.SetParent(ent.transform);
        pivcam.transform.localPosition = VectorData.Set(0, 2, -5);
        pivcam.transform.Rotate(20, 0, 0);
        objcam.transform.SetParent(pivcam.transform);
        objcam.transform.localPosition = VectorData.zero;
        bubble.transform.SetParent(ent.transform);
        bubble.transform.localPosition = VectorData.zero;
        bubble.transform.localScale = VectorData.Set(3, 3, 3);
        melvin.transform.SetParent(ent.transform);
        melvin.transform.localPosition = VectorData.zero;
        melvin.transform.localScale = VectorData.Set(0.85F, 0.85F, 0.85F);
        pivhandL.transform.SetParent(objcam.transform);
        pivhandL.transform.localPosition = VectorData.Set(-0.15F, -0.1F, 0.2F);
        pivhandR.transform.SetParent(objcam.transform);
        pivhandR.transform.localPosition = VectorData.Set(0.15F, -0.1F, 0.2F);
        handL.transform.SetParent(pivhandL.transform);
        handL.transform.localPosition = VectorData.zero;
        handL.transform.localScale = VectorData.Set(0.03F, 0.03F, 0.03F);
        handR.transform.SetParent(pivhandR.transform);
        handR.transform.localPosition = VectorData.zero;
        handR.transform.localScale = VectorData.Set(0.03F, 0.03F, 0.03F);

        cam = objcam.GetComponent<Camera>();
        cam.fieldOfView = 80;
        cam.backgroundColor = Color.cyan;
        cam.clearFlags = CameraClearFlags.Skybox;
        cam.nearClipPlane = 0.01F;

        anmelvin = melvin.GetComponent<Animator>();
        anmelvin.runtimeAnimatorController = Main.game.ANIMATIONS.melvin;

        imghandL = handL.GetComponent<SpriteRenderer>();
        imghandR = handR.GetComponent<SpriteRenderer>();
        imghandL.sprite = Main.game.TEXTURES.plHand;
        imghandL.flipX = true;
        imghandR.sprite = Main.game.TEXTURES.plHand;
        handL.SetActive(false);
        handR.SetActive(false);

        imgshadow = shadow.GetComponent<SpriteRenderer>();
        imgshadow.sprite = Main.game.TEXTURES.Shadow;
        imgmelvin = melvin.GetComponent<SpriteRenderer>();
        imgmelvin.sortingOrder = 2;
        imgmelvin.flipX = true;

        anHandL = handL.GetComponent<Animator>();
        anHandR = handR.GetComponent<Animator>();

        anHandL.runtimeAnimatorController = Main.game.ANIMATIONS.plHand;
        anHandR.runtimeAnimatorController = Main.game.ANIMATIONS.plHand;

        //anHandL.Play("Hand", 0, 0);
        //anHandR.Play("Hand", 0, 1);

        mbubble = bubble.GetComponent<MeshRenderer>();
        mbubble.sharedMaterial = Main.game.TEXTURES.Bubble;

        col = ent.GetComponent<SphereCollider>();
        col.radius = 2.0F;
        col.sharedMaterial = Main.game.TEXTURES.PYS_Player;

        rb = ent.GetComponent<Rigidbody>();
        rb.drag = 0.36F;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public class FUNC_PLAYER : MonoBehaviour
    {
        public Player player;
        private float xo;
        private float yo;
        private float zo;
        public bool moving;
        private float torquerot;
        private float antick;
        private float rotcamZ;
        private float rotcamX;

        private float speed = 0.46F;
        private float scale = 1;

        private float fixedcamY;
        private float rotextraX;
        private float rotextraZ;
        private bool TOPSPEED; //Called When reaching the Max limit of bouncing speed
        private float YSPEED;
        public bool FPSMODE = false;
        float fov = 86;

        private Platform platform;
        private Platform platformcol;

        private LevelTag lvtag;
        private Goal.GoalFlag goal;
        private float stepRoll = 1;

        public IEnumerator CallReset()
        {
            player.LIVES--;

            if (player.LIVES < 0)
            {
                SetGameOver();
                player.LIVES = 0;
                yield break;
            }

            yield return new WaitForSeconds(2.36F);
            Restart();
            yield break;
        }

        protected void SetGameOver()
        {
            Main.game.isGameOver = true;
            Main.game.hud.ShowGameOver(player.asource);
            Main.game.audio.control.GameOver();
            return;
        }
        protected void SetVictory()
        {
            player.celebrating = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent<Platform>(out platformcol))
            {
                if (scale > 2.6F && TOPSPEED && platformcol.GetID() == 2)
                {
                    player.asource.PlayOneShot(Main.game.SOUNDS.SFX_Explosion);
                    platformcol.Break();
                }
            }

            if (player.P_STARS_PLAYER.Length > 0)
                for (int i = 0; i < player.P_STARS_PLAYER.Length; i++)
                {
                    if (TOPSPEED)
                    {
                        player.asource.PlayOneShot(Main.game.SOUNDS.SFX_Bounce);
                        player.P_STARS_PLAYER[i].MoveAndProduce(transform.position.x, transform.position.y, transform.position.z);
                        TOPSPEED = false;
                    }
                }

            return;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<Goal.GoalFlag>(out goal))
            {
                SetVictory();
            }
        }

        private void Start()
        {
            Main.game.level.player = player;
            Main.game.hud.GO();
            player.asource.PlayOneShot(Main.game.SOUNDS.VOC_Go);

            player.P_STARS_PLAYER = new Particle[3]
            {
                new Particle(0,transform.position.x,transform.position.y,transform.position.z),
                new Particle(0,transform.position.x,transform.position.y,transform.position.z),
                new Particle(0,transform.position.x,transform.position.y,transform.position.z)
            };

            lvtag = FindObjectOfType<LevelTag>();
        }

        public void Restart()
        {
            player.celebrating = false;
            fov = 86;
            player.ent.transform.position = player.SPAWN_POINT;
            player.ent.transform.rotation = Quaternion.Euler(0, 0, 0);
            Main.game.hud.ResetHUD();

            Main.game.hud.GO();
            player.asource.PlayOneShot(Main.game.SOUNDS.VOC_Go);

            return;
        }

        public IEnumerator CallForGameOver()
        {
            yield return new WaitForSeconds(5);
            Main.ResetGame();
            yield break;
        }

        public IEnumerator PrepareForNextLevel()
        {
            Main.game.level.DestroyGoals();

            yield return new WaitForSeconds(2);
            Level.CURRENT_LEVEL++;

            if (Level.CURRENT_LEVEL >= 4)
                Main.game.EndGame();

            Main.game.level.DestroyLevel();
            Main.game.level.CreateLevel();
            Restart();
        }

        private void FixedUpdate()
        {
            if (moving)
            {
                player.rb.AddRelativeForce(xo, yo, zo);
            }
        }

        private void Update()
        {
            player.ent.transform.localScale = VectorData.Set(scale, scale, scale);

            //General - FALLOUT
            player.fallout = transform.position.y < player.FALL_LIMIT && !player.celebrating;
            YSPEED = player.rb.velocity.y;

            if (player.fallout)
            {
                player.pivcam.transform.position = VectorData.Set(player.pivcam.transform.position.x, player.FALL_LIMIT, player.pivcam.transform.position.z);
                player.pivcam.transform.LookAt(player.ent.transform);

                if (!Main.game.hud.triggerDeath || Main.game.hud.triggerGameOver)
                {
                    StartCoroutine(CallReset());

                    if (player.LIVES != 0 && player.LIVES < 0)
                        Main.game.hud.ShowDeath(player.asource);
                    if (player.LIVES < 0)
                        StartCoroutine(CallForGameOver());
                }
            }

            if (player.rb.velocity.magnitude > 16 && YSPEED > -12)
                TOPSPEED = true;

            if (player.celebrating)
            {
                player.ent.transform.Rotate(0, 126 * Time.deltaTime, 0);
                player.anmelvin.Play("melvinVictory");

                if (!Main.game.hud.triggerCompletion)
                {
                    StartCoroutine(PrepareForNextLevel());
                    Main.game.hud.ShowCompletion(player.asource);
                }
            }

            //lvtag.transform.eulerAngles = VectorData.Set(0 + rotcamX / 36, 0, 0 + rotcamZ / 36);

            Main.game.hud.scalehand.transform.localPosition = VectorData.Set(-25.0F, 23 + player.ent.transform.localScale.y * 20, 0.0F);

            //DEBUG
            /*if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Main.game.hud.ResetHUD();
                player.ent.transform.position = VectorData.Set(0, 0, 0);
            }*/
            if (Input.GetKey(KeyCode.X))
                scale += 0.36F;
            if (Input.GetKey(KeyCode.Z))
                scale -= 0.36F;

            /*if (Input.GetKeyDown(KeyCode.V))
            {
                SetVictory();
            }*/

            if (scale < 0.3F)
                scale = 0.3F;
            if (scale > 3.0F)
                scale = 3.0F;

            //Ray
            RaycastHit hit;

            player.grounded = Physics.Raycast(player.ent.transform.position, -player.ent.transform.up, 3 + scale * 2);

            if (Physics.Raycast(player.ent.transform.position, -player.ent.transform.up, out hit, 8))
            {
                player.shadow.SetActive(true);
                Vector3 pshadow = hit.point;
                pshadow.y = hit.point.y + 0.2F;
                player.shadow.transform.position = pshadow;
                player.shadow.transform.rotation = Quaternion.Euler(90 + hit.transform.rotation.x * 106, hit.transform.rotation.y * 106, hit.transform.rotation.z * 106);
                player.shadow.transform.localScale = VectorData.Set(1 + player.ent.transform.localScale.x, 1 + player.ent.transform.localScale.z, 0);
                // rotextraX = hit.transform.eulerAngles.x;
                // rotextraZ = hit.transform.eulerAngles.z;
                //player.ent.transform.eulerAngles = VectorData.Set(hit.normal.x * 32, player.ent.transform.eulerAngles.y, hit.normal.z * 32);

                if (hit.transform.TryGetComponent<Platform>(out platform))
                {
                    if (platform.isFan)
                    {
                        player.asource.PlayOneShot(Main.game.SOUNDS.SFX_Bounce);

                        player.rb.AddRelativeForce(0, 640, 0, ForceMode.Acceleration);

                        if (!player.grounded)
                            player.rb.AddRelativeForce(0, 1040, 0, ForceMode.Acceleration);
                    }
                }
            }
            else
            {
                player.shadow.SetActive(false);
            }

            //Animations
            if (player.fallout && !player.grounded && !player.celebrating)
                player.anmelvin.Play("melvinFallout", 0);

            if (!player.fallout && !player.celebrating)
            {
                if (moving && !player.fallout && player.grounded || player.rb.velocity.magnitude > 6 && !player.fallout && player.grounded)
                    player.anmelvin.Play("melvinMoveBack");
                else if (player.grounded)
                    player.anmelvin.Play("melvinIdleBack");
            }

            if (!player.grounded && !player.fallout && !player.celebrating)
                player.anmelvin.Play("melvinScared", 0);

            player.melvin.transform.LookAt(player.objcam.transform);

            //Camera
            //player.cam.fieldOfView = 75 + player.rb.velocity.sqrMagnitude / 64;

            player.melvin.SetActive(!FPSMODE);

            if (!player.fallout)
                player.cam.fieldOfView = 86 + player.rb.velocity.sqrMagnitude / 86;
            else
            {
                player.cam.fieldOfView = fov--;

                if (player.cam.fieldOfView < 10)
                    player.cam.fieldOfView = 10;
            }

            if (!player.fallout)
            {
                player.pivcam.transform.localRotation = Quaternion.Euler(15 + fixedcamY * 2 * -player.rb.velocity.y / 32, rotcamZ, 0);
                if (!FPSMODE) player.pivcam.transform.localPosition = VectorData.Set(0, 2 + fixedcamY / 2, -5);
                else player.pivcam.transform.localPosition = VectorData.Set(0, 1, 0);
            }
            player.objcam.transform.localRotation = Quaternion.Euler(0, player.objcam.transform.localRotation.y, 0);

            if (player.cam.fieldOfView > 112)
                player.cam.fieldOfView = 112;

            if (fixedcamY > 3)
                fixedcamY = 3;
            if (fixedcamY < 0)
                fixedcamY = 0;

            if (rotcamZ > 8)
                rotcamZ = 8;
            if (rotcamZ < -8)
                rotcamZ = -8;
            if (rotcamX > 8)
                rotcamX = 8;
            if (rotcamX < -8)
                rotcamX = -8;

            if (rotcamZ < 0)
                rotcamZ += 0.34F;
            if (rotcamZ > 0)
                rotcamZ -= 0.34F;
            if (rotcamX < 0)
                rotcamX += 0.34F;
            if (rotcamX > 0)
                rotcamX -= 0.34F;

            if (player.grounded)
                fixedcamY -= 0.06F;
            else
                fixedcamY += 0.46F;

            //Movement
            if (!player.celebrating && !Main.game.isGameOver)
                moving = Input.GetKey(Bindings.kUp) || Input.GetKey(Bindings.kDown) || Input.GetKey(Bindings.kLeft) || Input.GetKey(Bindings.kRight);
            player.bubble.transform.Rotate(zo / 3 + player.rb.velocity.sqrMagnitude / 88, 0, -xo / 3 + player.rb.velocity.sqrMagnitude / 88);
            player.ent.transform.Rotate(0, torquerot, 0);
            player.ent.transform.eulerAngles = VectorData.Set(player.ent.transform.eulerAngles.x + rotextraX, player.ent.transform.eulerAngles.y, player.ent.transform.eulerAngles.z + rotextraZ);

            Main.game.tick.control.LIMIT_STEPTICKS = 0.86F / player.rb.velocity.magnitude * 4;

            if (Main.game.tick.control.aStepTick && player.grounded && moving)
            {
                player.asource.PlayOneShot(Main.game.SOUNDS.SFX_Roll);
            }

            if (torquerot > 2)
                torquerot = 2;
            if (torquerot < -2)
                torquerot = -2;

            if (antick > 1)
                antick = 1;
            if (antick < 0)
                antick = 0;

            float handLCast = antick - 0.26F;
            // player.anHandL.SetFloat("tick", handLCast);
            // player.anHandR.SetFloat("tick", antick);

            /*if (Input.GetKey(KeyCode.W))
                player.ent.transform.Rotate(-3, 0, 0);
            if (Input.GetKey(KeyCode.S))
                player.ent.transform.Rotate(3, 0, 0);*/
            if (Input.GetKeyDown(Bindings.kUse))
            {
                player.rb.AddRelativeForce(player.ent.transform.up * 3, ForceMode.Impulse);
                player.rb.AddRelativeForce(player.ent.transform.forward * 8, ForceMode.Impulse);
            }

            if (moving)
            {
                float castspeed = player.rb.velocity.magnitude / 32;

                if (Input.GetKey(Bindings.kUp) && player.rb.velocity.sqrMagnitude < 1286)
                {
                    if (player.rb.velocity.sqrMagnitude < 2)
                        zo += speed + 5.64F;
                    else
                        zo += speed + 1.64F;

                }
                if (Input.GetKey(Bindings.kDown) && player.rb.velocity.sqrMagnitude < 1286)
                {
                    if (player.rb.velocity.sqrMagnitude < 2)
                        zo -= speed + 5.64F;
                    else
                        zo -= speed + 1.64F;
                }
                if (Input.GetKey(Bindings.kLeft) && player.rb.velocity.sqrMagnitude < 1286)
                {
                    //xo -= speed * zo / 26 - castspeed;
                    if (castspeed > 80)
                    {
                        //xo = -60;
                        zo = castspeed * 3;
                    }
                    else
                        xo = -20;
                }
                if (Input.GetKey(Bindings.kRight) && player.rb.velocity.sqrMagnitude < 1286)
                {
                    if (castspeed > 80)
                    {
                        //xo = 60;
                        zo = castspeed * 3;
                    }
                    else
                        xo = 20;
                }

                //ROTATION
                if (Input.GetKey(Bindings.kUp))
                    rotcamX += 1.22F;
                if (Input.GetKey(Bindings.kDown))
                    rotcamX -= 1.22F;

                if (Input.GetKey(Bindings.kLeft))
                {
                    torquerot -= 0.32F;
                    rotcamZ -= 1.22F;
                }
                if (Input.GetKey(Bindings.kRight))
                {
                    torquerot += 0.32F;
                    rotcamZ += 1.22F;
                }
                antick += 0.1F;
            }
            else
            {
                if (xo < 0.4F && xo > -0.4F)
                    xo = 0;
                if (zo < 0.4F && zo > -0.4F)
                    zo = 0;
                if (rotcamZ < 0.4F && rotcamZ > -0.4F)
                    rotcamZ = 0;

                antick -= 0.1F;
            }

            if (zo > 0)
                zo -= 0.84F;
            if (zo < 0)
                zo += 0.84F;
            if (xo > 0)
                xo -= 0.84F;
            if (xo < 0)
                xo += 0.84F;
            if (torquerot < 0)
                torquerot += 0.04F;
            if (torquerot > 0)
                torquerot -= 0.04F;

        }
    }
}