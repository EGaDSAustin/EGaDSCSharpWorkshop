using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGaDSTutorial
{
    class Player : MonoBehaviour
    {
        public float Money = 0;
        [SerializeField] private float _speed = 2;
        
        #region Singleton

        private static Player _instance;
        public static Player Instance => _instance ? _instance : (_instance = FindObjectOfType<Player>());

        #endregion

        #region Movement

        public void Update()
        {
            transform.position = 
                transform.position + _speed * Time.deltaTime * 
                new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        }
    
        #endregion

        #region Colission

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Gem>() != null) other.gameObject.GetComponent<Gem>().OnPickup();
        }

        #endregion
    }
}