using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public enum TutorialState2
    {
        Blank,
        Hint,
        Wait1,
        PhaseGun,
        GunMechanics,
        DoneWithTutorial
    };

    public class Tutorial2 : Tutorial
    {
        // should we show the tutorial at all?
        public bool ShowTutorial = true;

        public GameObject p;
        public bool start;


        private Text _tutorialText;
        private TutorialState2 _tutorialState = TutorialState2.Blank;
        private bool phase;
        private bool rocket = true;
        private bool gravity = false;
        private float wait, wait_long, last;

        // the different messages to display throughout the tutorial
        private static readonly string[] TutorialStrings =
        {
            "",
            "Conserve rocket fuel.",
            "",
            "Phaser acquired.",
            "Cycle guns with Left Shift. Weapon phases through objects.",
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
            if (!Player.hasRocketBoost)
            {
                _tutorialText.text = "That jump looks awfully high...";
                rocket = false;
            }
            if (Player.hasGravityBoots)
            {
                _tutorialText.text = "";
                gravity = true;
            }
            if (rocket && !gravity)
            {
                UpdateText();
            }
        }

        /// <summary>
        /// Called (internally or externally) whenever the Player does something that might be of interest to the tutorial.
        /// Changes state appropriately.
        /// </summary>
        /// <param name="action">The type of event that just happened</param>
        public void UserAction(TutorialState2 action)
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
            if (rocket && !gravity)
            {
                CheckActions();
            }
        }

        /// <summary>
        /// Checks to see if interesting things are happening and updates the tutorial accordingly
        /// </summary>
        private void CheckActions()
        {
            if (!ShowTutorial) { return; }
            if (_tutorialState == TutorialState2.DoneWithTutorial) { return; } // nothing to see here


            if (_tutorialState == TutorialState2.Blank && Time.time > wait)
            {
                UserAction(TutorialState2.Blank);
                last = Time.time;
            }

            if (_tutorialState == TutorialState2.Hint && Time.time > wait_long + last)
            {
                UserAction(TutorialState2.Hint);
                last = Time.time;
            }

            if (_tutorialState == TutorialState2.Wait1 && phase)
            {
                UserAction(TutorialState2.Wait1);
                last = Time.time;
            }

            if (_tutorialState == TutorialState2.PhaseGun && Time.time > wait + last)
            {
                UserAction(TutorialState2.PhaseGun);
                last = Time.time;
            }

            if (_tutorialState == TutorialState2.GunMechanics && (Input.GetKey(KeyCode.Space) && Time.time > wait + last))
            {
                UserAction(TutorialState2.GunMechanics);
                last = Time.time;
            }
        }

        public override void AcquirePowerUp(int upgrade)
        {
            switch (upgrade)
            {
                case 3:
                    phase = true;
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
    }
}

