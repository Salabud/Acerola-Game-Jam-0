using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    public float kick_power;
    public SphereCollider kick_hitbox;
    
    private Vector3 kick_dir;
    public bool can_kick = true;
    //private bool object_in_hitbox = false;

    void Start()
    {
        kick_dir = new Vector3(0,1,1); //45 degrees
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && can_kick)
        {
            StartCoroutine(KickAction());
        }
    }

    IEnumerator KickAction()
    {
        can_kick = false;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, kick_hitbox.radius, transform.forward, out hit, 2))
        {
            Debug.Log("hit");
            if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce((transform.forward+transform.up) * kick_power, ForceMode.VelocityChange);
            }
        }
        yield return new WaitForSeconds(0.5f);
        can_kick = true;
    }
}
