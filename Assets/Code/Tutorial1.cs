using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    //how far along in the tutorial are we?
    public enum TutorialState1
    {
        Blank,
        Hint,
        Wait1,
        Rocket,
        RocketMechanics,
        DoneWithTutorial,
        Infinite,
        Wait2,
        Ionizer,
        Mech,
        Done
    };

    public class Tutorial1 : Tutorial
    {
        // should we show the tutorial at all?
        public bool ShowTutorial = true;

        public GameObject p;
        public bool start;


        private Text _tutorialText;
        private TutorialState1 _tutorialState = TutorialState1.Blank;
        private bool rocket;
        private bool gravity = false;
        private float wait, wait_long, last;

        // the different messages to display throughout the tutorial
        private static readonly string[] TutorialStrings =
        {
            "",
            "Continue exploring this area.",
            "",
            "Rocket Boost acquired.",
            "Activate with Spacebar. Replaces one of your jumps.",
            "",
            "Translucent fields allow you to switch gravity freely.",
            "",
            "Ionizer acquired.",
            "Cycle guns with Left Shift. Activate/deactivate blue platforms.",
            ""
        };

        internal void Start()
        {
            _tutorialText = GameObject.Find("Tutorial Text").GetComponent<Text>();
            p = GameObject.Find("Player");
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
            if (Player.hasGravityBoots)
            {
                _tutorialText.text = "";
                gravity = true;
                _tutorialState = TutorialState1.DoneWithTutorial;
            }
            if (!gravity)
            {
                UpdateText();
            }
        }

        /// <summary>
        /// Called (internally or externally) whenever the Player does something that might be of interest to the tutorial.
        /// Changes state appropriately.
        /// </summary>
        /// <param name="action">The type of event that just happened</param>
        public void UserAction(TutorialState1 action)
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
            if (!gravity)
            {
                CheckActions();
            }
            if (gravity && ((p.transform.position.x > 207f) && (p.transform.position.y < -9f)) && (_tutorialState == TutorialState1.DoneWithTutorial) && ShowTutorial)
            {
                UserAction(TutorialState1.DoneWithTutorial);
                last = Time.time;
                //_tutorialText.text = "Translucent fields allow you to switch gravity freely.";
            }
            if (_tutorialState == TutorialState1.Infinite && Time.time > wait_long + last)
            {
                UserAction(TutorialState1.Infinite);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.Wait2 && Player.hasSniper)
            {
                UserAction(TutorialState1.Wait2);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.Ionizer && Time.time > wait + last)
            {
                UserAction(TutorialState1.Ionizer);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.Mech && Time.time > wait_long + last)
            {
                UserAction(TutorialState1.Mech);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.Done) { return; }


        }

        /// <summary>
        /// Checks to see if interesting things are happening and updates the tutorial accordingly
        /// </summary>
        private void CheckActions()
        {
            if (!ShowTutorial) { return; }
            if (_tutorialState == TutorialState1.DoneWithTutorial) { return; } // nothing to see here


            if (_tutorialState == TutorialState1.Blank && Time.time > wait)
            {
                UserAction(TutorialState1.Blank);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.Hint && Time.time > wait_long + last)
            {
                UserAction(TutorialState1.Hint);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.Wait1 && rocket)
            {
                UserAction(TutorialState1.Wait1);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.Rocket && Time.time > wait + last)
            {
                UserAction(TutorialState1.Rocket);
                last = Time.time;
            }

            if (_tutorialState == TutorialState1.RocketMechanics && (Input.GetKey(KeyCode.Space) && Time.time > wait + last))
            {
                UserAction(TutorialState1.RocketMechanics);
                last = Time.time;
            }
        }

        public override void AcquirePowerUp(int upgrade)
        {
            switch (upgrade)
            {
                case 2:
                    rocket = true;
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
    }
}
