using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset;
        void Start()
        {
            offset = transform.position - player.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = player.position + offset;

        }
    }
}
