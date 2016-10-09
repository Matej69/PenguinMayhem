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

    private GameObject saveButtonObj;
    private GameObject mapChoiceDropdownObj;
    private GameObject mapNameInputObj;
    private Button saveButton;
    private Dropdown mapChoiceDropdown;
    private InputField mapNameInput;

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

        //gui on top of shown tile GUI
        saveButtonObj = transform.FindChild("ButtonSave").gameObject;
        mapChoiceDropdownObj = transform.FindChild("DropdownMapChoice").gameObject;
        mapNameInputObj = transform.FindChild("InputMapName").gameObject;

        saveButton = saveButtonObj.GetComponent<Button>();
        mapChoiceDropdown = mapChoiceDropdownObj.GetComponent<Dropdown>();
        mapNameInput = mapNameInputObj.GetComponent<InputField>();

        SaveButtonListener();
        InitMapChoiceItems();
        MapChoiceDropdownListener();

        //InitPlatformToolSprites();
        CreatePanelItems(ResourceReader.platformSpriteMap, ref platformsGrid);
        CreatePanelItems(ResourceReader.backgroundSpriteMap, ref backgroundsGrid);
        CreatePanelItems(ResourceReader.itemSpriteMap, ref itemsGrid);
   
    }
	
	// Update is called once per frame
	void Update () {
        if (InputManager.keyPressed[KeyCode.C])
            LevelMap.ClearMapObjects(ref mapEditor.map);

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
    

    //TOP GUI************************************
    void InitMapChoiceItems()
    {
        List<string> mapNames = MapParser.GetMapNamesFromJSON();
        mapChoiceDropdown.options.Clear();
        foreach (string name in mapNames) {
           mapChoiceDropdown.options.Add(new Dropdown.OptionData() { text = name.ToString() });
        }
        mapChoiceDropdown.value = 1;
        mapChoiceDropdown.value = 0;
    }
    //top GUI listeners
    void SaveButtonListener() {
        saveButton.onClick.AddListener(
            delegate{
                string inputText = mapNameInput.text.ToString();
                if (inputText == "" || inputText == null)
                    inputText = mapChoiceDropdown.value.ToString();
                //get maps from json, remove duplicates,save new map, save to json
                List<LevelMap> allMaps = MapParser.GetAllLevelMapsFromJSON();                
                LevelMap.RemoveCopiesWithSameName(inputText, ref allMaps);
                mapEditor.map.name = inputText;
                allMaps.Add(mapEditor.map);
                MapParser.SaveLevelMapsToJSON(allMaps);
                //destroy allMaps except active one because they dont need to be on screen(bad solution but works)
                LevelMap.ClearAllMaps(ref allMaps);
                //refresh dropdown to display saved map  
                InitMapChoiceItems();

            });
    }

    void MapChoiceDropdownListener()
    {
        mapChoiceDropdown.onValueChanged.AddListener(
            delegate {
                string dropdownText = mapChoiceDropdown.captionText.text.ToString();
                mapNameInput.text = dropdownText;
                LevelMap.ClearMapObjects(ref mapEditor.map);
                mapEditor.map = MapParser.GetLevelMapFromJSON(dropdownText);                   
            });
    }








}
