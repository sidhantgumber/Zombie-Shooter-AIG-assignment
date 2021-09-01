using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit2
{
    public class Enemy : MonoBehaviour
    {

      //  public Transform currentObject;
        public Transform goalObject;
        Rigidbody rb;
        public Animator anim;
        public float enemySpeed = 0.5f;
        private float closeEnoughDistance = 0.5f;

        private List<Enemy> otherEnemies = new List<Enemy>();

        void Start()
        {
            foreach (var enemy in otherEnemies)
            {
                if (enemy != this)
                {
                    otherEnemies.Add(enemy);
                }
            }
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();


        }


        void Update()
        {
            // currentObject.position += Vector3.forward * speed;


            float distanceToGoal = Vector3.Distance(transform.position, goalObject.position);
            Vector3 goalDirection = (goalObject.position - transform.position).normalized;

            Vector3 lookAtGoal = new Vector3 (goalObject.position.x, transform.position.y, goalObject.position.z);
            transform.LookAt(lookAtGoal);

            if (Vector3.Distance(transform.position,lookAtGoal)>closeEnoughDistance)
            {
               
                transform.Translate(0,0, enemySpeed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else anim.SetBool("isRunning", false);
            Debug.Log(rb.velocity);
          //  float currentEnemySpeed = enemySpeed
         //   anim.SetFloat("Speed", );


            foreach (var enemy in otherEnemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < 2f)
                {
                    Vector3 oppEnemyDirection = (transform.position - enemy.transform.position).normalized;
                    transform.Translate(oppEnemyDirection * enemySpeed * Time.deltaTime);

                }


            }
        }


    }
}
