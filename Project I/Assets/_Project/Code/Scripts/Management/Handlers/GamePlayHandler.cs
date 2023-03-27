using UnityEngine;

namespace Wonderland.Management
{
    public class GamePlayHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            MainManager.GamePlayHandler = this;
        }
        
        private void OnDisable()
        {
            MainManager.GamePlayHandler = null;
        }
    }
}