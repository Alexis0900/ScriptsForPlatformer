using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{

    public Transform spawnPosition;
    private Vector3 mouse_pos;
    private Vector3 object_pos;
    
    public void ShootWeapon(int damage, int speed, GameObject bulletType)
    {
        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // dicrection from A to B = B.position - A.postion
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;

       
        GameObject projectile = Instantiate(bulletType, spawnPosition.position, Quaternion.identity) as GameObject;     
        projectile.transform.forward = mouse_pos.normalized;

        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
        projectile.GetComponent<RangedPlayerBullet>().SetDamage(damage);
    }

}
