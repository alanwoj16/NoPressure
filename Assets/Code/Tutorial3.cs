using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public enum TutorialState3
    {
        Blank,
        Hint,
        Wait1,
        GravityBoots,
        GravityMechanics,
        Jump,
        DoneWithTutorial
    };

    public class Tutorial3 : Tutorial
    {
        // should we show the tutorial at all?
        public bool ShowTutorial = true;

        public GameObject p;
        public bool start;


        private Text _tutorialText;
        private TutorialState3 _tutorialState = TutorialState3.Blank;
        private bool boots;
        private float wait, wait_long, last;

        // the different messages to display throughout the tutorial
        private static readonly string[] TutorialStrings =
        {
            "",
            "Purple barriers absorb phase bullets.",
            "",
            "Gravity Disruptor acquired.",
            "Invert gravity with Tab. Requires a brief recharge.",
            "Jump down with S once inverted.",
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
        }

        /// <summary>
        /// Called (internally or externally) whenever the Player does something that might be of interest to the tutorial.
        /// Changes state appropriately.
        /// </summary>
        /// <param name="action">The type of event that just happened</param>
        public void UserAction(TutorialState3 action)
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
            CheckActions();
        }

        /// <summary>
        /// Checks to see if interesting things are happening and updates the tutorial accordingly
        /// </summary>
        private void CheckActions()
        {
            if (!ShowTutorial) { return; }
            if (_tutorialState == TutorialState3.DoneWithTutorial) { return; } // nothing to see here


            if (_tutorialState == TutorialState3.Blank && Time.time > wait)
            {
                UserAction(TutorialState3.Blank);
                last = Time.time;
            }

            if (_tutorialState == TutorialState3.Hint && Time.time > wait_long + last)
            {
                UserAction(TutorialState3.Hint);
                last = Time.time;
            }

            if (_tutorialState == TutorialState3.Wait1 && boots)
            {
                UserAction(TutorialState3.Wait1);
                last = Time.time;
            }

            if (_tutorialState == TutorialState3.GravityBoots && Time.time > wait + last)
            {
                UserAction(TutorialState3.GravityBoots);
                last = Time.time;
            }

            if (_tutorialState == TutorialState3.GravityMechanics && (Input.GetKey(KeyCode.Tab) && Time.time > wait + last))
            {
                UserAction(TutorialState3.GravityMechanics);
                last = Time.time;
            }

            if (_tutorialState == TutorialState3.Jump && (Input.GetKey(KeyCode.S) && Time.time > wait + last))
            {
                UserAction(TutorialState3.Jump);
                last = Time.time;
            }
        }

        public override void AcquirePowerUp(int upgrade)
        {
            switch (upgrade)
            {
                case 5:
                    boots = true;
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
    }
}


