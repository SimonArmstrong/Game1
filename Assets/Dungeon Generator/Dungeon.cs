using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DunGen
{
    public class Dungeon : MonoBehaviour
    {
        public int floorCount = 10;
        public Floor currentFloor;
        public List<Floor> floors = new List<Floor>();
        public GameObject floorObject;

        [Header("Stats")]
        public int numberOfFloors = 10;


        private void Start()
        {
            for (int i = 0; i < numberOfFloors; i++)
            {
                floors.Add(Instantiate(floorObject).GetComponent<Floor>());
            }
        }
    }
}