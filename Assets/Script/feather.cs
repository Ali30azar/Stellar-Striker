using UnityEngine;

namespace Script
{
    public class feather : MonoBehaviour
    {
        [SerializeField] private float speed;
        public Rigidbody2D Rigidbody2D;
        
        void Update()
        {
            Rigidbody2D.AddForce(transform.up * speed );
            Destroy(this.gameObject ,4f);
        }
    }
}