using System.Collections;

using UnityEngine;

using Photon.Pun.UtilityScripts;

namespace Photon.Pun.Demo.Asteroids
{
    public class Spaceship : MonoBehaviour
    {

        private PhotonView photonView;

        private new Rigidbody rigidbody;
        private new Collider collider;
        private new Renderer renderer;

        private bool controllable = true;

        public void Awake()
        {
            photonView = GetComponent<PhotonView>();

            rigidbody = GetComponent<Rigidbody>();
            renderer = GetComponent<Renderer>();
        }

        public void Start()
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                r.material.color = AsteroidsGame.GetColor(photonView.Owner.GetPlayerNumber());
            }
        }

        public void FixedUpdate()
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!controllable)
            {
                return;
            }

            if (Input.GetKey(KeyCode.W))
            {
                Vector3 force = new Vector3(0f, 0f, 7f);
                rigidbody.AddForce(force);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Vector3 force = new Vector3(0f, 0f, -7f);
                rigidbody.AddForce(force);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Vector3 force = new Vector3(7f, 0f, 0f);
                rigidbody.AddForce(force);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Vector3 force = new Vector3(-7f, 0f, 0f);
                rigidbody.AddForce(force);
            }

            CheckExitScreen();
        }

        private void CheckExitScreen()
        {
            if (Camera.main == null)
            {
                return;
            }
            if (Mathf.Abs(rigidbody.position.x) > (Camera.main.orthographicSize * Camera.main.aspect))
            {
                rigidbody.position = new Vector3(-Mathf.Sign(rigidbody.position.x) * Camera.main.orthographicSize * Camera.main.aspect, 0, rigidbody.position.z);
                rigidbody.position -= rigidbody.position.normalized * 0.1f; // offset a little bit to avoid looping back & forth between the 2 edges 
            }

            if (Mathf.Abs(rigidbody.position.z) > Camera.main.orthographicSize)
            {
                rigidbody.position = new Vector3(rigidbody.position.x, rigidbody.position.y, -Mathf.Sign(rigidbody.position.z) * Camera.main.orthographicSize);
                rigidbody.position -= rigidbody.position.normalized * 0.1f; // offset a little bit to avoid looping back & forth between the 2 edges 
            }
        }
    }
}