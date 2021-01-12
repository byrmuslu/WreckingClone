using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.GameObject.Environment
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
    }
}