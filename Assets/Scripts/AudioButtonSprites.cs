    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioButtonSprites : MonoBehaviour
    {
        [SerializeField]
        private Sprite OnState;
        
        [SerializeField]
        private Sprite OffState;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public Sprite GetOffState() {
            return OffState;
        }
        
        public Sprite GetOnState() {
            return OnState;
        }
    }
