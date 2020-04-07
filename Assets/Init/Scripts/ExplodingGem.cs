using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EGaDSTutorial
{
    
    [RequireComponent(typeof(Collider2D))]
    public class ExplodingGem : MonoBehaviour, IGem
    {
        [SerializeField] private float _startValue = 100f;
        [SerializeField] private float _explodeTime = 10f;
        [SerializeField] private AnimationCurve valueFalloffCurve;
        private float _value;
        public float Value => _value;


        private void Awake()
        {
            _value = _startValue;
            StartCoroutine(TickGem());
        }

        IEnumerator TickGem()    
        {
            float timer = 0;

            Vector2 startScale = transform.localScale;
            
            while (timer < _explodeTime)
            {
                float t = valueFalloffCurve.Evaluate(timer / _explodeTime);
                transform.localScale = t * startScale;
                _value = _startValue * t;
                yield return null;
                timer += Time.deltaTime;
            }
            
            Destroy(gameObject);
        }
        
        public void OnPickup()
        {
            Player.Instance.Money += _value;
            Destroy(gameObject);
        }
    }
}