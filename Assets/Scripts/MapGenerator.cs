﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int columns;
    private float roomWidth = 50f;
    private float roomHeight = 50f;
    public GameObject[] gridPrefabs;
    private Room[,] grid;

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateGrid()
    {
        // Start with an empty grid
        grid = new Room[columns, rows];

        // For each grid row
        for (int currentRow=0; currentRow < rows; currentRow++)
        {
            for (int currentColumn=0; currentColumn < columns; currentColumn++)
            {
                // Figure out the location
                float xPosition = roomWidth * currentColumn;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);

                // Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set the room's parent
                tempRoomObj.transform.parent = this.transform;

                // Give the room a meaningful name
                tempRoomObj.name = "Room " + currentColumn + ", " + currentRow;


                Room tempRoom = tempRoomObj.GetComponent<Room>();

                if (rows == 1)
                {
                    // Do nothing
                }
                else if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                if (columns == 1)
                {
                    // Do nothing
                } 
                else if(currentColumn == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentColumn == columns - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

                grid[currentColumn, currentRow] = tempRoom;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
