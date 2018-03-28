using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class CameraFollow : MonoBehaviour
    {

        public GameObject Player;
        public Vector3 Offset1, Offset2;

        public GameObject star;
        //public Quaternion rot = Quaternion.Euler(0, 180f, 0);

        void Start()
        {
            star = GameObject.Find("Starfield");
            Offset1 = new Vector3(7, 4, -500);
            Offset2 = new Vector3(7, 4, -400);
        }


        void LateUpdate()
        {
            Player = GameObject.Find("Player");

            if (Player != null)
            {
                gameObject.transform.position = Player.transform.position + Offset1;
                //star.transform.position = Player.transform.position + Offset2;
            }

            /*
            if (Player.GetComponent<Player>().invert)
            {
                float x = Player.transform.position.x + Offset.x;
                float y = Player.transform.position.y + Offset.y;
                float z = Player.transform.position.z + Offset.z;
                Vector3 rotateValue = new Vector3(x, y * -1, z);
                gameObject.transform.eulerAngles = rotateValue;
            }
            */
        }
    }
}

