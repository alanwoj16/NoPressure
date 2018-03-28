using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{

    public enum TutorialState4
    {
        Blank,
        Move,
        DoubleJump,
        HUDOxygen,
        Warning,
        Wait1,
        OxygenTank,
        Wait2,
        Pause,
        Wait3,
        Gun,
        GunMechanics,
        DoneWithTutorial
    };

    public class Tutorial4 : Tutorial
    {
        // should we show the tutorial at all?
        public bool ShowTutorial = true;

        public GameObject p;
        public bool start;


        private Text _tutorialText;
        private TutorialState4 _tutorialState = TutorialState4.Blank;
        private bool normalgun;
        private float wait, wait_long, last;

        // the different messages to display throughout the tutorial
        private static readonly string[] TutorialStrings =
        {
            "",
            "Move horizontally with A and D.",
            "Jump with W. Double jump gives an extra boost.",
            "Your oxygen levels are the upper left blue bar.",
            "If your oxygen runs out, you die",
            "",
            "Acquire blue oxygen tanks to restore your reserves.",
            "",
            "Pause the game with ENTER. Press ENTER again to unpause.",
            "",
            "Normal Gun acquired.",
            "Fire your gun with I/J/K/L (Up, Left, Down, Below)",
            ""
        };

        internal void Start()
        {
            _tutorialText = GameObject.Find("Tutorial Text").GetComponent<Text>();
            //p = GameObject.Find("Player");
            //start = GameObject.Find("Player").start

            Cheat CheatObj = GameObject.FindObjectOfType<Cheat>();

            if (CheatObj.cheating)
            {
                ShowTutorial = false;
            }

            else
            {
                ShowTutorial = true;
            }


            wait = 3.0f;
            wait_long = 6.0f;
            UpdateText();
        }

        /// <summary>
        /// Called (internally or externally) whenever the Player does something that might be of interest to the tutorial.
        /// Changes state appropriately.
        /// </summary>
        /// <param name="action">The type of event that just happened</param>
        public void UserAction(TutorialState4 action)
        {
            if (action != _tutorialState) return; // this action wasn't relevant at this particular time.

            _tutorialState++;
            UpdateText();

        }

        // Fill this function in

        private void UpdateText()
        {
            _tutorialText.text = TutorialStrings[(int)_tutorialState];

        }


        internal void Update()
        {
            _tutorialText.text = "";
            //CheckActions();
        }

        /// <summary>
        /// Checks to see if interesting things are happening and updates the tutorial accordingly
        /// </summary>
        private void CheckActions()
        {
            if (!ShowTutorial)
            {
                return;
            }
            if (_tutorialState == TutorialState4.DoneWithTutorial)
            {
                return;
            } // nothing to see here


            if (_tutorialState == TutorialState4.Blank && Time.time > 5f)
            {
                UserAction(TutorialState4.Blank);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.Move && Time.time > wait + last)
            {
                UserAction(TutorialState4.Move);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.DoubleJump && Input.GetKeyDown(KeyCode.W) && Time.time > wait + last)
            {
                UserAction(TutorialState4.DoubleJump);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.HUDOxygen && Time.time > wait_long + last)
            {
                UserAction(TutorialState4.HUDOxygen);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.Warning && Time.time > wait_long + last)
            {
                UserAction(TutorialState4.Warning);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.Wait1 && Time.time > wait + last)
            {
                UserAction(TutorialState4.Wait1);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.OxygenTank && Time.time > wait + last)
            {
                UserAction(TutorialState4.OxygenTank);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.Wait2 && Time.time > wait + last)
            {
                UserAction(TutorialState4.Wait2);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.Pause && Time.time > wait + last)
            {
                UserAction(TutorialState4.Pause);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.Wait3 && normalgun)
            {
                UserAction(TutorialState4.Wait3);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.Gun && Time.time > wait + last)
            {
                UserAction(TutorialState4.Gun);
                last = Time.time;
            }

            if (_tutorialState == TutorialState4.GunMechanics &&
                (Input.GetKey(KeyCode.J) || (Input.GetKey(KeyCode.K)) || (Input.GetKey(KeyCode.L)) ||
                 (Input.GetKey(KeyCode.I))) && Time.time > wait + last)
            {
                UserAction(TutorialState4.GunMechanics);
                last = Time.time;
            }
        }

        public override void AcquirePowerUp(int upgrade)
        {
            switch (upgrade)
            {
                case 1:
                    normalgun = true;
                    break;
                case 6:
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
    }
}
