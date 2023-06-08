using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private bool facingLeft = true;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        transform.localScale = new Vector3(-1f, -1f, 1f);

    }
    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        rotation.z = 0;
        transform.right = rotation;

        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (mousePos.x > transform.position.x && facingLeft )
        {

            FlipX();
            

        }
        if (mousePos.x < transform.position.x && !facingLeft )
        {

            FlipX();
            

        }
    }



    void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        scale.y *= -1;
        transform.localScale = scale;

        facingLeft = !facingLeft;
    }

    

}