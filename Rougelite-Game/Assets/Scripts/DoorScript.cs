using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
   
   public Vector3 dir;
    public bool bossdoor;
   public bool stop, wallinfront, doorinfront;
   [SerializeField] private GameObject[] walls;
   public Transform startingpoint;
   
   private void Update()
   {
      if(!wallinfront || !doorinfront ) CheckDoorAndWalls();
   }

   public void Open()
   {
      
   }

   public void Close()
   {
      
   }
   private void CheckDoorAndWalls()
   {
      Vector3 dir2 = new Vector3(dir.x / 10, dir.y / 10,0);
      Vector3 offset = new Vector3(transform.position.x + (dir.x / 60), transform.position.y + (dir.y / 60), 0);
      RaycastHit2D hit = Physics2D.Raycast(offset, dir2, 5);
      if ( hit.collider.gameObject.CompareTag("Walls"))
      {
         wallinfront = true;

      }
      if ( hit.collider.gameObject.CompareTag("Door"))
      {
         doorinfront = true;

      }
      Debug.Log( gameObject.name + " " + GetComponentInParent<RoomController>().gameObject.name + " Hit" + " " + hit.collider.gameObject.name + " " + hit.collider.GetComponentInParent<RoomController>().gameObject.name);
     
   }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawRay(new Vector3(transform.position.x +(dir.x / 60), transform.position.y + (dir.y / 60), 0), new Vector3(dir.x / 10,dir.y / 10,0).normalized * 5);
   }
}
