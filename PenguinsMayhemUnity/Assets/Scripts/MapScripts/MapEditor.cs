﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapEditor : MonoBehaviour {

    private List<GameObject> platforms = new List<GameObject>();
    
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
        if (InputManager.keyHold[KeyCode.Mouse0])
            PlacePlatform();
        if (InputManager.keyHold[KeyCode.Mouse1])
            RemovePlatform();

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
    void PlacePlatform()
    {
        //check if platform on targeted position already exists
        Vector2 posiblePos = highlightedCell.transform.position;
        foreach (GameObject plat in platforms)
        {
            Vector2 targetPos = plat.transform.position;
            if (targetPos.x == posiblePos.x && targetPos.y == posiblePos.y)
            {
                plat.GetComponent<SpriteRenderer>().sprite = GUIEditor.selectedSprite;
                plat.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                return;
            }
        }        
        //create object and set sprite
        GameObject obj = Instantiate(highlightedCell);
        if (GUIEditor.selectedSprite != null)
        {
            obj.GetComponent<SpriteRenderer>().sprite = GUIEditor.selectedSprite;
            obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        platforms.Add(obj);        
    }
    void RemovePlatform()
    {
        Vector2 posiblePos = highlightedCell.transform.position;
        foreach(GameObject plat in platforms)
        {
            Vector2 targetPos = plat.transform.position;
            if (targetPos.x == posiblePos.x && targetPos.y == posiblePos.y)
            {
                Destroy(plat);
                platforms.Remove(plat);
                return;
            }
        }


       
    }


}
