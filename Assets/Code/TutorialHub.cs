using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public enum TutorialStateHub
    {
        Blank,
        Hint,
        DoneWithTutorial
    };

    public class TutorialHub : Tutorial
    {
        // should we show the tutorial at all?
        public bool ShowTutorial = true;

        public Player p;
        public bool start;


        private Text _tutorialText;
        private TutorialStateHub _tutorialState = TutorialStateHub.Blank;
        private float wait, wait_long, last, lastnew;
        public int cells;

        // the different messages to display throughout the tutorial
        private static readonly string[] TutorialStrings =
        {
            "",
            "Find and collect at least 12 energy cells to power the escape pod.",
            ""
        };

        internal void Start()
        {
            _tutorialText = GameObject.Find("Tutorial Text").GetComponent<Text>();
            p = GameObject.FindObjectOfType<Player>();
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
        public void UserAction(TutorialStateHub action)
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
            cells = p.CountCells();
            if (Player.hasGravityBoots && !Player.hasSniper && (cells < 12))
            {
                lastnew = Time.time;
                if (Time.time < lastnew + wait_long)
                {

                    if (ShowTutorial)
                    {
                        _tutorialText.text = "Try exploring previous areas with your Gravity Disruptor...";
                    }

                    //_tutorialText.text = "Try exploring previous areas with your Gravity Disruptor...";
                }
                else
                {
                    _tutorialText.text = "";
                }
            }
            else if (Player.hasSniper && (cells < 12))
            {
                lastnew = Time.time;
                if (Time.time < lastnew + wait_long)
                {

                    if (ShowTutorial)
                    {
                        _tutorialText.text = "Try exploring previous areas with your Ionizer...";
                    }

                    //_tutorialText.text = "Try exploring previous areas with your Ionizer...";
                }
            }
            else if (cells >= 12)
            {
                lastnew = Time.time;
                if (Time.time < lastnew + wait_long)
                {
                    _tutorialText.text = "Activate the escape pod.";
                }
            }
            else
            {
                CheckActions();
            }
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
            if (_tutorialState == TutorialStateHub.DoneWithTutorial)
            {
                return;
            } // nothing to see here


            if (_tutorialState == TutorialStateHub.Blank && Time.time > wait)
            {
                UserAction(TutorialStateHub.Blank);
                last = Time.time;
            }

            if (_tutorialState == TutorialStateHub.Hint && Time.time > wait_long + last)
            {
                UserAction(TutorialStateHub.Hint);
                last = Time.time;
            }

        }

        public override void AcquirePowerUp(int upgrade)
        {
            switch (upgrade)
            {
                case 3:
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
    }
}

