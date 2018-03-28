using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public UIManager UIM;
    private Rigidbody2D rb2d;
    private Gun _gun;
    public TeleportPlayer telePlay;
    public Animator flash;

    Animator anim;

    public bool paused = false;
    public bool facingRight = true;
    public float maxSpeed = 25f;
    public float jumpForce = 1000f;
    public float rocketForce = 1750f;
    public int _cells;

    public bool grounded = false;
    public bool rocketGrounded = false;
    public Transform groundCheck;
    public Transform groundCheckL;
    public Transform groundCheckR;
    public float groundRadius = .2f;
    public LayerMask groundLayer;

    public bool teleported = false;
    public LayerMask teleportLayer;
    public Transform teleportCheck;
    public Transform teleportCheckL;
    public Transform teleportCheckR;
    public Transform teleportCheckC;
    public float teleportRadius = .2f;
    public float teleportRadiusCenter = .6f;

    public bool wasInverted = false;
    public float grav = 1.0f;
    public Quaternion wantedRotation;
    public Quaternion originalRotation;
    public static float _lastSwitch = 0.0f;
    public static float _gravityCoolDown = 5.0f;
    public bool firstUse = true;

    public bool proceedTeleport = false;
    public bool checkForRocketGround = false;
    public bool usedRocket = false;
    public bool doubleJump = false;
    public static bool hasNormalGun = false;
    public static bool hasRocketBoost = false;
    public static bool hasShotGun = false;
    public static bool hasSniper = false;
    public static bool hasGravityBoots = false;
    public bool gameOver = false;

    public static bool[] hasEnergyCell = new bool[15];

    //temporary bools for saving
    private static bool savedhasNormalGun = false;
    private static bool savedhasRocketBoost = false;
    private static bool savedhasShotGun = false;
    private static bool savedhasSniper = false;
    private static bool savedhasGravityBoots = false;
    private static bool[] savedhasEnergyCell = new bool[15];
    private static int savedCount;




    public Slider OxygenTank;
    public Slider RocketFuel;
    private Text _energyText;
    public static int _count = 0;

    public float MaxOxygen { get; set; }
    public float MaxFuel { get; set; }

    public static float currentOxygen = 100f;
    public static float currentFuel = 0f;

    public Image gravityTimer;
    public Image gravityOverlay;
    public static float fillValue;
    public static bool infiniteGrav = false;

    public Vector3 teleportLocation { get; set; }

    public string sceneName;

    
    public bool playing = false;

    public Text tutorial;
    public Scene _scene;

    public static bool isCheating = false;
    public bool turnCheatsOn = false;

    public Image oxygenFlash;
    public Color32 flashColor = new Color32(11, 215, 255, 255);

    private static int CurrentScene;

    public AudioClip tel;
    public AudioClip end;

    public static bool endPlaying = false;



    private void Start()
    {
    
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        sceneName = SceneManager.GetActiveScene().name;

        anim = GetComponent<Animator>();
        Cheat CheatObj = GameObject.FindObjectOfType<Cheat>();
        turnCheatsOn = CheatObj.cheating;

        //GameObject.Destroy(CheatObj.gameObject); causes null error when restarted

        originalRotation = transform.rotation;


        OxygenTank = GameObject.Find("OxygenLevel").GetComponent<Slider>();
        RocketFuel = GameObject.Find("RocketBoostLevel").GetComponent<Slider>();
        gravityTimer = GameObject.Find("Gravity Cooldown").GetComponent<Image>();
        gravityOverlay = GameObject.Find("Gravity Overlay").GetComponent<Image>();
        oxygenFlash = GameObject.Find("OxygenLevel").transform.Find("Fill Area/OxygenFill").GetComponent<Image>();

        savedhasNormalGun = hasNormalGun;
        savedhasRocketBoost = hasRocketBoost;
        savedhasShotGun = hasShotGun;
        savedhasSniper = hasSniper;
        savedhasGravityBoots = hasGravityBoots;
        hasEnergyCell.CopyTo(savedhasEnergyCell, 0);
        savedCount = _count;

        tel = (AudioClip)Resources.Load("teleport");
        end = (AudioClip) Resources.Load("ending");


        if (!hasRocketBoost){
            RocketFuel.gameObject.SetActive(false);
        }

        if (hasRocketBoost)
        {
            if(currentFuel <= 10)
            {
                currentFuel = 30f;
            }
        }

        if (!hasGravityBoots)
        {
            gravityOverlay.enabled = false;
            gravityTimer.enabled = false;
        }

        if(hasGravityBoots){
            fillValue = 1f;
            infiniteGrav = false;
            _gravityCoolDown = 5.0f;

        }

        _energyText = GameObject.Find("Energy Text").GetComponent<Text>();
        SetEnergyText();

        MaxOxygen = 100f;
        MaxFuel = 100f;

        RocketFuel.value = currentFuel / MaxFuel;

        tutorial = GameObject.Find("Tutorial Text").GetComponent<Text>();

        if (!hasNormalGun)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

       
        if (turnCheatsOn)
        {
            enableCheats();
            infiniteGrav = true;
            _gravityCoolDown = 0f;
        }
        

    }

    public void enableCheats()
    {
        AcquirePowerUp(1);
        AcquirePowerUp(2);
        AcquirePowerUp(3);
        AcquirePowerUp(4);
        AcquirePowerUp(5);
        AcquirePowerUp(6);

        isCheating = true;

        //gameObject.GetComponent<SpriteRenderer>().color = new Color32(123, 233, 151, 255);
    }


    public void UseFuel()
    {
        if (isCheating)
        {
            return;
        }

        if(currentFuel < 5){

            currentFuel = 0;
        }
        else{
            currentFuel = currentFuel - 5f;
        }
        //currentFuel = currentFuel - 5f;

        if(currentFuel <= 0)
        {
            currentFuel = 0;
            RocketFuel.value = currentFuel;
            //hasRocketBoost = false;
        }

        else
        {
            RocketFuel.value = currentFuel / MaxFuel;
        }
    }

    public void UseOxygen()
    {
        if (isCheating)
        {
            return;
        }

        currentOxygen -= Time.deltaTime;

        if(currentOxygen <= 0)
        {
            currentOxygen = 0;
            OxygenTank.value = 0;

            gameOver = true;
            tutorial.enabled = false;
            UIM.SpawnGameOver();
            TogglePause();
            SoundManager.instance.FullHealth();
            playing = false;
            currentOxygen = 100f;
            //ResetStatics();
        }

        else
        {
            if (currentOxygen <= (MaxOxygen / 2f))
            {
                if (!playing)
                {
                    SoundManager.instance.LowHealth();
                    playing = true;
                }
                oxygenFlash.color = Color.Lerp(flashColor, Color.red, Mathf.Sin(Time.time * 4.1f));
            }
            else if (currentOxygen >= (MaxOxygen / 2f))
            {
                SoundManager.instance.FullHealth();
                playing = false;
                oxygenFlash.color = flashColor;
            }
            OxygenTank.value = currentOxygen / MaxOxygen;
        }
    }

    public void gravTimerUpdate()
    {
        if(_lastSwitch > 0)
        {
            if ((Time.time - _lastSwitch) > _gravityCoolDown)
            {
                fillValue = 1f;
            }

            else
            {
                fillValue = (Time.time - _lastSwitch) / _gravityCoolDown;
            }

            gravityTimer.fillAmount = fillValue;
        }

        
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _gun = GetComponent<Gun>();
        UIM = FindObjectOfType<UIManager>();
    }

    IEnumerator RocketWait()
    {
        yield return new WaitForSeconds(.1f);
        checkForRocketGround = true; 
    }


    void FixedUpdate()
    {
        //Physics2D.Linecast(gameObject.transform.position, gameObject.transform.position + 5f*Vector3.down)
        //Physics2D.OverlapCircle(groundCheckL.position, groundRadius, groundLayer)
        grounded = Physics2D.OverlapCircle(groundCheckL.position, groundRadius, groundLayer) ||
            Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer) ||
            Physics2D.OverlapCircle(groundCheckR.position, groundRadius, groundLayer);


        Collider2D plat = null;

        if(grounded){

            if(Physics2D.OverlapCircle(groundCheckL.position, groundRadius, groundLayer)){
                plat = Physics2D.OverlapCircle(groundCheckL.position, groundRadius, groundLayer);
            }

            else if(Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer)){
                plat = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
            }
            else if(Physics2D.OverlapCircle(groundCheckR.position, groundRadius, groundLayer)){
                plat = Physics2D.OverlapCircle(groundCheckR.position, groundRadius, groundLayer);
            }
        }


        if(plat != null){
            ActivatePlatform platActive = plat.gameObject.GetComponent<ActivatePlatform>();

            if (platActive && !platActive.isActive)
            {
                grounded = false;
            }

        }


        teleported = Physics2D.OverlapCircle(teleportCheckL.position, teleportRadius, teleportLayer) ||
                     Physics2D.OverlapCircle(teleportCheck.position, teleportRadius, teleportLayer) ||
                     Physics2D.OverlapCircle(teleportCheckR.position, teleportRadius, teleportLayer) ||
                     Physics2D.OverlapCircle(groundCheckL.position, teleportRadius, teleportLayer) ||
                     Physics2D.OverlapCircle(groundCheck.position, teleportRadius, teleportLayer) ||
                     Physics2D.OverlapCircle(groundCheckR.position, teleportRadius, teleportLayer) ||
                     Physics2D.OverlapCircle(teleportCheckC.position, teleportRadiusCenter, teleportLayer);
        
        if (teleported && proceedTeleport)
        {
            flash.Play("Flash");

            //RocketFuel.value = 1f;
            //currentFuel = MaxFuel;

            //obtained from teleport

            telePlay.MovePlayer(teleportLocation);
            SoundManager.instance.PlaySingle(tel);

            rb2d.velocity = new Vector2(0f, 0f);
            teleported = false;
            wasInverted = false;
            if (rb2d.gravityScale < 0)
            {
                rb2d.gravityScale *= -1;
            }
            grav = 1.0f;
            _lastSwitch = Time.time - _gravityCoolDown;
            if (transform.rotation != originalRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * 100f);
            }

            checkForRocketGround = true;

            proceedTeleport = false;
        }


        if (grounded)
        {
            doubleJump = false;
            //usedRocket = false;
        }


        if (checkForRocketGround)
        {

            /*
            Collider2D leftOverlap = Physics2D.OverlapCircle(groundCheckL.position, groundRadius, groundLayer);
            Collider2D middleOverlap = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
            Collider2D rightOverlap = Physics2D.OverlapCircle(groundCheckR.position, groundRadius, groundLayer);

            if(leftOverlap != null)
            {
                if(leftOverlap.gameObject != null)
                {
                    if(leftOverlap.gameObject.GetComponent<ActivatePlatform>() != null)
                    {

                        if (leftOverlap.gameObject.GetComponent<ActivatePlatform>().isActive)
                        {
                            rocketGrounded = true;
                        }

                        else
                        {
                            rocketGrounded = false;
                        }

                    }

                    else
                    {
                        rocketGrounded = true;
                    }

                }
            }

            else if (middleOverlap != null)
            {
                if (middleOverlap.gameObject != null)
                {
                    if (middleOverlap.gameObject.GetComponent<ActivatePlatform>() != null)
                    {
                        if (middleOverlap.gameObject.GetComponent<ActivatePlatform>().isActive)
                        {
                            rocketGrounded = true;
                        }

                        else
                        {
                            rocketGrounded = false;
                        }
                    }

                    else
                    {
                        rocketGrounded = true;
                    }

                }
            }

            else if (rightOverlap != null)
            {
                if (rightOverlap.gameObject != null)
                {
                    if (rightOverlap.gameObject.GetComponent<ActivatePlatform>() != null)
                    {
                        if(rightOverlap.gameObject.GetComponent<ActivatePlatform>().isActive)
                        {
                            rocketGrounded = true;
                        }

                        else
                        {
                            rocketGrounded = false;
                        }
                    }

                    else
                    {
                        rocketGrounded = true;
                    }

                }
            }

*/


        rocketGrounded = Physics2D.OverlapCircle(groundCheckL.position, groundRadius, groundLayer) ||
        Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer) ||
        Physics2D.OverlapCircle(groundCheckR.position, groundRadius, groundLayer);
        


        }

        if (rocketGrounded)
        {
            checkForRocketGround = false;
            usedRocket = false;
            rocketGrounded = false;
        }


        float move = Input.GetAxis("Horizontal");
    
        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

        
        if (!wasInverted)
        {
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();
        }
        else
        {
            if (move < 0 && !facingRight)
                Flip();
            else if (move > 0 && facingRight)
                Flip();
        }

    }

    void Update()
    {
        


        if (Mathf.Abs(rb2d.velocity.y) > 35f)
        {
            if (wasInverted)
            {
                rb2d.AddForce(4 * Physics.gravity);
            }
            //print("terminal velocity");

            else
            {
                rb2d.AddForce(-4 * Physics.gravity);
            }
        }


        if (hasGravityBoots)
        {
            gravTimerUpdate();
        }

        if (!gameOver)
        {
            UseOxygen();
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && grounded)
        {
            anim.SetInteger("State", 1);
        }
        else if ((anim.GetInteger("State") == 1) && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))) // && (rb2d.velocity.magnitude < 0.2f))
        {
            anim.SetInteger("State", 0);
        }
        else if ((anim.GetInteger("State") == 2) && (grounded && !doubleJump))
        {
            anim.SetInteger("State", 0);
        }

        if ((grounded || !doubleJump) && ((Input.GetKeyDown(KeyCode.W) && wasInverted == false) || (Input.GetKeyDown(KeyCode.S) && wasInverted == true)))
        {
            anim.SetInteger("State", 2);
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(new Vector2(0, grav * jumpForce));

            if (!doubleJump && !grounded)
                doubleJump = true;
        }
        else if(!usedRocket && (grounded || !doubleJump) && hasRocketBoost && Input.GetKeyDown(KeyCode.Space) && currentFuel > 0)
        {
            anim.SetInteger("State", 2);
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(new Vector2(0, grav * rocketForce));
            UseFuel();

            if (!doubleJump && !grounded)
                doubleJump = true;

            usedRocket = true;

            StartCoroutine(RocketWait());

        }


        //if (hasRocketBoost && grounded && Input.GetKeyDown(KeyCode.Space) && currentFuel > 0)
        //{
        //    anim.SetInteger("State", 2);
        //    rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
        //    rb2d.AddForce(new Vector2(0, grav * rocketForce));
        //    UseFuel();
        //    grounded = false;
        //}

        if(wasInverted)
        {
            if (Input.GetKey(KeyCode.J))
            {
                /*
                if(!facingRight){
                    Flip();
                }
                */
                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.J);

            }

            if (Input.GetKey(KeyCode.K))
            {
                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.K);
            }


            if (Input.GetKey(KeyCode.L))
            {
                /*
                if(facingRight){
                    Flip();
                }
                */

                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.L);
            }

            if (Input.GetKey(KeyCode.I))// && !grounded)
            {
                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.I);
            }
        }

        else{
            if (Input.GetKey(KeyCode.J))
            {
                /*
                if (facingRight)
                {
                    Flip();
                }
                */
                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.J);
            }

            if (Input.GetKey(KeyCode.K))// && !grounded)
            {
                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.K);
            }


            if (Input.GetKey(KeyCode.L))
            {
                /*
                if (!facingRight)
                {
                    Flip();
                }
                */
                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.L);
            }

            if (Input.GetKey(KeyCode.I))
            {
                //SoundManager.instance.RandomizeSfx(normal);
                _gun.Fire(KeyCode.I);
            }
        }
        /*
        if (Input.GetKey(KeyCode.J))
            _gun.Fire(KeyCode.J);

        if (Input.GetKey(KeyCode.K) && !grounded)
            _gun.Fire(KeyCode.K);
        

        if (Input.GetKey(KeyCode.L))
            _gun.Fire(KeyCode.L);

        if (Input.GetKey(KeyCode.I))
            _gun.Fire(KeyCode.I);
            */

        //removed &&(grounded || intiniteGrav)
        if ((Input.GetKeyDown(KeyCode.Tab) && hasGravityBoots) ||  (infiniteGrav && Input.GetKeyDown(KeyCode.Tab) && hasGravityBoots))
        {
            float time = Time.time;
            if ((time > _lastSwitch + _gravityCoolDown) || firstUse)
            {
                gravityTimer.fillAmount = 0;

                firstUse = false;
                _lastSwitch = time;

                if (wasInverted)
                {
                    wasInverted = false;
                    rb2d.gravityScale *= -1;
                    grav *= -1;
                    wantedRotation = originalRotation;

                }
                else
                {
                    wasInverted = true;
                    rb2d.gravityScale *= -1;
                    grav *= -1;
                    wantedRotation = Quaternion.Euler(0, 0, 180f);
                }

                //transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * 0.1f);
                transform.rotation = wantedRotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!paused)
            {
                tutorial.enabled = false;
                UIM.SpawnPause();
                TogglePause();
            }
            else
            {
                tutorial.enabled = true;
                if (GameObject.FindObjectOfType<PauseMenu>())
                {
                    FindObjectOfType<PauseMenu>().Die();
                }
                else if (GameObject.FindObjectOfType<ControlsMenu>())
                {
                    FindObjectOfType<ControlsMenu>().Die();
                }
                else
                {
                    FindObjectOfType<InvertedMenu>().Die();
                }
                TogglePause();
            }
        }

        if (!endPlaying && (CountCells() >= 12))
        {
            SoundManager.instance.Ending(end);
            endPlaying = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ResetStatics()
    {
        hasNormalGun = false;
        hasRocketBoost = false;
        hasShotGun = false;
        hasSniper = false;
        gravityOverlay.enabled = false;
        gravityTimer.enabled = false;
        hasGravityBoots = false;
        playing = false;

        for (int i = 0; i < hasEnergyCell.Length; i++)
        {
            hasEnergyCell[i] = false;
        }
    }

    public void AcquirePowerUp(int upgrade)
    {
        switch (upgrade)
        {
            case 1:
                hasNormalGun = true;
                break;
            case 2:
                RocketFuel.gameObject.SetActive(true);
                hasRocketBoost = true;
                currentFuel = MaxFuel;
                RocketFuel.value = currentFuel;
                break;
            case 3:
                hasShotGun = true;
                break;
            case 4:
                hasSniper = true;
                break;
            case 5:
                gravityOverlay.enabled = true;
                gravityTimer.enabled = true;
                gravityTimer.fillAmount = 1f;
                hasGravityBoots = true;
                break;
            case 6:
                currentOxygen = MaxOxygen;
                SoundManager.instance.FullHealth();
                playing = false;
                break;
            default:
                break;
        }
    }

    public int CountCells()
    {
        int cells = 0;
        if (hasEnergyCell[0])
            cells++;
        if (hasEnergyCell[1])
            cells++;
        if (hasEnergyCell[2])
            cells++;
        if (hasEnergyCell[3])
            cells++;
        if (hasEnergyCell[4])
            cells++;
        if (hasEnergyCell[5])
            cells++;
        if (hasEnergyCell[6])
            cells++;
        if (hasEnergyCell[7])
            cells++;
        if (hasEnergyCell[8])
            cells++;
        if (hasEnergyCell[9])
            cells++;
        if (hasEnergyCell[10])
            cells++;
        if (hasEnergyCell[11])
            cells++;
        if (hasEnergyCell[12])
            cells++;
        if (hasEnergyCell[13])
            cells++;
        if (hasEnergyCell[14])
            cells++;

        return cells;
    }


    public void SetEnergyText()
    {
        int cells = 0;
        switch (sceneName)
        {
            case "Area0":
                if (hasEnergyCell[13])
                    cells++;

                _energyText.text = "Energy Cells: " + cells.ToString() + "/1";
                break;
            case "Hub":
                _cells = CountCells();

                _energyText.text = "Energy Cells: " + _cells.ToString() + "/15";
                break;
            case "Area1":
                if (hasEnergyCell[0])
                    cells++;
                if (hasEnergyCell[1])
                    cells++;
                if (hasEnergyCell[2])
                    cells++;
                if (hasEnergyCell[3])
                    cells++;

                _energyText.text = "Energy Cells: " + cells.ToString() + "/4";
                break;
            case "Area2":
                if (hasEnergyCell[4])
                    cells++;
                if (hasEnergyCell[5])
                    cells++;

                _energyText.text = "Energy Cells: " + cells.ToString() + "/2";
                break;
            case "Area3":
                if (hasEnergyCell[6])
                    cells++;
                if (hasEnergyCell[7])
                    cells++;
                if (hasEnergyCell[14])
                    cells++;

                _energyText.text = "Energy Cells: " + cells.ToString() + "/3";
                break;
            case "Area4":
                if (hasEnergyCell[8])
                    cells++;
                if (hasEnergyCell[9])
                    cells++;
                if (hasEnergyCell[10])
                    cells++;
                if (hasEnergyCell[11])
                    cells++;
                if (hasEnergyCell[12])
                    cells++;

                _energyText.text = "Energy Cells: " + cells.ToString() + "/5";
                break;
            default:
                break;
        }
        
    }


    public void TogglePause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
        }
        else
        {
            paused = true;
            Time.timeScale = 0f;
        }
    }

    public void ChangeTele(Vector3 teleLocal)
    {
        teleportLocation = teleLocal;
        proceedTeleport = true;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == CurrentScene)
        {
            hasNormalGun = savedhasNormalGun;
            hasRocketBoost = savedhasRocketBoost;
            hasShotGun = savedhasShotGun;
            hasSniper = savedhasSniper;
            hasGravityBoots = savedhasGravityBoots;
            savedhasEnergyCell.CopyTo(hasEnergyCell, 0);
            _count = savedCount;

        }
        


        if (level == 1)
        {

            if (turnCheatsOn)
            {
                return;
            }

            hasNormalGun = false;
            hasRocketBoost = false;
            hasShotGun = false;
            hasSniper = false;
            hasGravityBoots = false;

            for (int i = 0; i < hasEnergyCell.Length; i++)
            {
                hasEnergyCell[i] = false;
            }

            _count = 0;


            currentOxygen = 100f;//200f;
            currentFuel = 0f;

            infiniteGrav = false;


            isCheating = false;


        }
        
    }
}

