﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapEditor : MonoBehaviour {
    
    [HideInInspector]
    public LevelMap map = new LevelMap(); 
    
    public int cellSize = 32;
    public GameObject highlightedCell;
    private float cellWorldSize;

    // Use this for initialization
    void Start () {
        highlightedCell = (GameObject)Instantiate(highlightedCell);
        cellWorldSize = highlightedCell.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                

    }
	
	// Update is called once per frame
	void Update () {
        SetSelectedTilePos();
        if (InputManager.keyHold[KeyCode.Mouse0] && map != null)
            PlaceTile();
        if (InputManager.keyHold[KeyCode.Mouse1] && map != null)        
            RemoveTile();
        //moving camera
        MoveCamOnInput();



    }

    //moving camera
    void MoveCamOnInput()
    {
        float camSpeed = 0.15f;
        Vector2 camPos = Camera.main.transform.position;
        if (InputManager.keyHold[KeyCode.UpArrow])
            camPos.y += camSpeed;
        if (InputManager.keyHold[KeyCode.DownArrow])
            camPos.y -= camSpeed;
        if (InputManager.keyHold[KeyCode.LeftArrow])
            camPos.x -= camSpeed;
        if (InputManager.keyHold[KeyCode.RightArrow])
            camPos.x += camSpeed;
        Camera.main.transform.position = new Vector3(camPos.x,camPos.y,-10);
    }

    void SetSelectedTilePos()
    {
        Vector2 mousePos = new Vector2(InputManager.mousePos.x, InputManager.mousePos.y);
        Vector2 finalCellPos = new Vector2();

        float minX, maxX;
        //..X..
        if (mousePos.x > 0) {
            minX = (int)(mousePos.x / cellWorldSize) * cellWorldSize;
            maxX = (int)(mousePos.x / cellWorldSize) * cellWorldSize + cellWorldSize / 2;
        } else{
            minX = (int)(mousePos.x / cellWorldSize) * cellWorldSize - cellWorldSize / 2;
            maxX = (int)(mousePos.x / cellWorldSize) * cellWorldSize;
        }
        finalCellPos.x = ( Mathf.Abs(mousePos.x/minX) < Mathf.Abs(mousePos.x/maxX)) ? (minX) : (maxX);

        //..Y..
        float minY, maxY;
        if (mousePos.y > 0) {
            minY = (int)(mousePos.y / cellWorldSize) * cellWorldSize + cellWorldSize / 2;
            maxY = (int)(mousePos.y / cellWorldSize) * cellWorldSize;
        } else{
            minY = (int)(mousePos.y / cellWorldSize) * cellWorldSize - cellWorldSize / 2;
            maxY = (int)(mousePos.y / cellWorldSize) * cellWorldSize;
        }
        finalCellPos.y = (Mathf.Abs(mousePos.y / minY) < Mathf.Abs(mousePos.y / maxY)) ? (minY) : (maxY);        

        highlightedCell.transform.position = finalCellPos;             
    }

    //PLATFORM PLACEMENT/REMOVAL::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::    
    void PlaceTile()
    {
        if (GUIEditor.selectedSprite == null)
            return;

        //check if platform on targeted position already exists
        Vector2 posiblePos = highlightedCell.transform.position;
        foreach (GameObject tileObj in map.mapObjects)
        {
            Vector2 targetPos = tileObj.transform.position;
            if (targetPos.x == posiblePos.x && targetPos.y == posiblePos.y)
            {
                tileObj.GetComponent<SpriteRenderer>().sprite = GUIEditor.selectedSprite;
                tileObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                return;
            }
        }        
        //create object and set sprite
        GameObject obj = Instantiate(highlightedCell);
        if (GUIEditor.selectedSprite != null) {
            obj.GetComponent<SpriteRenderer>().sprite = GUIEditor.selectedSprite;
            obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }        
        map.mapObjects.Add(obj);
        EntityInfo.PrintObjectInfo(obj);
    }
    void RemoveTile()
    {
        Vector2 posiblePos = highlightedCell.transform.position;
        foreach(GameObject tileObj in map.mapObjects)
        {
            Vector2 targetPos = tileObj.transform.position;
            if (targetPos.x == posiblePos.x && targetPos.y == posiblePos.y)
            {
                Destroy(tileObj);
                map.mapObjects.Remove(tileObj);
                return;
            }
        }       
    }
    



}
