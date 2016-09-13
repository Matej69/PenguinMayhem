using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIEditor : MonoBehaviour {

    private GameObject platformsPannel;
    private GameObject backgroundsPannel;
    private GameObject itemsPannel;
    private GridLayoutGroup platformsGrid;
    private GridLayoutGroup backgroundsGrid;
    private GridLayoutGroup itemsGrid;

    public GameObject gridElement;
    
    [HideInInspector]
    public static Sprite selectedSprite;

    private MapEditor mapEditor;
    
	// Use this for initialization
	void Start () {
        mapEditor = ((ScreenEditor)Screen.screen).mapEditor.GetComponent<MapEditor>();
        platformsPannel = transform.FindChild("PanelPlatforms").gameObject;
        backgroundsPannel = transform.FindChild("PanelBackgrounds").gameObject;
        itemsPannel = transform.FindChild("PanelItems").gameObject;

        platformsGrid = platformsPannel.GetComponent<GridLayoutGroup>();
        backgroundsGrid = backgroundsPannel.GetComponent<GridLayoutGroup>();
        itemsGrid = itemsPannel.GetComponent<GridLayoutGroup>();


        platformsGrid.cellSize = new Vector2(mapEditor.cellSize, mapEditor.cellSize);
        backgroundsGrid.cellSize = new Vector2(mapEditor.cellSize, mapEditor.cellSize);
        itemsGrid.cellSize = new Vector2(mapEditor.cellSize, mapEditor.cellSize);

        //InitPlatformToolSprites();
        CreatePanelItems(ResourceReader.platformSpriteMap, ref platformsGrid);
        CreatePanelItems(ResourceReader.backgroundSpriteMap, ref backgroundsGrid);
        CreatePanelItems(ResourceReader.itemSpriteMap, ref itemsGrid);
   

    }
	
	// Update is called once per frame
	void Update () {

    }
    

    void CreatePanelItems(Dictionary<Sprite, int> map, ref GridLayoutGroup grid)
    {
        //set texture
        foreach (KeyValuePair<Sprite, int> pair in map)
        {
            //create and set sprite for buttons that are part of toolbar
            GameObject element = Instantiate(gridElement) as GameObject;
            element.GetComponent<Image>().sprite = pair.Key;
            element.name = "GridElement";
            element.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "";
            element.transform.SetParent(grid.transform);
            //set up listener for button
            Button elementButton = element.GetComponent<Button>();
            elementButton.onClick.AddListener(
                delegate
                {
                    GUIEditor.selectedSprite = elementButton.image.sprite;
                });
        }
    }

    public bool isMouseOnGUIPannel()
    {
        return false;
    }



}
