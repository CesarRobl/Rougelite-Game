using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
   public Vector3 dir;
   [SerializeField]private bool stop, wallinfront;
   [SerializeField] private GameObject[] walls;

   private void Update()
   {
      if(!stop) CheckDoorAndWalls();
   }

   private void SpawnWall()
   {
      walls[0].SetActive(true);
   }
   private void CheckDoorAndWalls()
   {
      Vector3 dir2 = new Vector3(dir.x / 10, dir.y / 10,0);
      Vector3 offset = new Vector3(transform.position.x + (dir.x / 60), transform.position.y + (dir.y / 60), 0);
      RaycastHit2D hit = Physics2D.Raycast(offset, dir2, 1);
      // if ( hit.collider.gameObject.CompareTag("Walls"))
      // {
      //   SpawnWall();
      //   
      // }
      
     stop = true;
   }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawRay(new Vector3(transform.position.x +(dir.x / 60), transform.position.y + (dir.y / 60), 0), new Vector3(dir.x / 10,dir.y / 10,0).normalized * 1);
   }
}
