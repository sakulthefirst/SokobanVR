using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Logic
{
    public class BoxManager : MonoBehaviour
    {
        // parameter for material
        public Material selectMaterial;
        public GameObject moveAudioSource;
        public GameObject inGoalAudioSource;

        public bool IsInGoal { get => isInGoal; private set => isInGoal = value; }

        private bool isInGoal = false;
        private Material defaultMaterial;
        private GameObject colliderGameObject;
        

        void Start()
        {
            defaultMaterial = GetComponent<Renderer>().material;
        }

        // Update is called once per frame
        void Update()
        {

            if (GameState.SelectedBoxInstance == gameObject)
            {
                GetComponent<Renderer>().material = selectMaterial;

                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    // Find where the player is from the cube
                    Vector3 dir = transform.position - colliderGameObject.gameObject.transform.position;
                    // Do not consider the y direction
                    dir.y = 0;
                    // Are we going to push in the x or z axis?
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
                    {
                        dir.z = 0;
                    }
                    else
                    {
                        dir.x = 0;
                    }
                    // Normalize then apply a constant to be sure that the distance from which the player push won't affect the speed of the movement
                    dir.Normalize();
                    //transform.position = transform.position+dir;

                    var newposition = transform.position + dir;

                    var canMove = true;
                    var _isInGoal = true;
                    if (Physics.CheckSphere(newposition, 0.1f))
                    {
                        var colliders = Physics.OverlapSphere(newposition, 0.1f);
                        foreach (var collider in colliders)
                        {
                            if (collider.gameObject.tag == "Goal")
                            {
                            }
                            else
                            {
                                canMove = false;
                                _isInGoal = false;
                            }
                        }
                    }
                    else
                    {
                        _isInGoal = false;
                    }

                    if (canMove)
                    {
                        transform.position = newposition;
                        this.isInGoal = _isInGoal;

                        if (this.IsInGoal)
                        {
                            inGoalAudioSource.GetComponent<AudioSource>().Play();
                        }
                        else
                        {
                            moveAudioSource.GetComponent<AudioSource>().Play();
                        }

                    }
                }



            }
            else
            {
                GetComponent<Renderer>().material = defaultMaterial;
            }



            // move
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Selector")
            {
                GameState.SelectedBoxInstance = gameObject;
                colliderGameObject = other.gameObject;
            }

        }

        void OnTriggerExit(Collider other)
        {
            if (GameState.SelectedBoxInstance == gameObject)
            {
                GameState.SelectedBoxInstance = null;
            }
        }

    }
}
