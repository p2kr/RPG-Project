using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                print("Portal Triggered");
                // Only specifying the sceneName or sceneBuildIndex 
                //will load the Scene with the Single mode
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
