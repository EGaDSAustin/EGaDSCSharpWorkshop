using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGaDSTutorial
{
    public interface IGem
    {
        float Value { get; }
        void OnPickup();
    }
    
    
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Gem/BaseGem")]
    public class Gem : MonoBehaviour, IGem
    {
        [SerializeField] private float _value;
        public float Value => _value;
        
        public void OnPickup()
        {
            Player.Instance.Money += _value;
            Destroy(gameObject);
        }
    }
}
