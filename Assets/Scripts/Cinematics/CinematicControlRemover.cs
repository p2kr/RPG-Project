using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Control;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            //GetComponent<FakePlayableDirector>().onFinish += EnableControl;
            //GetComponent<FakePlayableDirector>().onFinish += DisableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector director)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            print("Disable Control");
        }

        void EnableControl(PlayableDirector director)
        {
            print("Enable Control");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}