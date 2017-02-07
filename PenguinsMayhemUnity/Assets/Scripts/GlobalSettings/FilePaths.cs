using UnityEngine;
using System.Collections;

public static class FilePaths
{
    public static string canvas = "GameObjects/Canvas/";
    public static string entity = "GameObjects/Entity/";
    public static string sprite = "Sprite/";
    //canvases
    public static string canvasMenu = "GameObjects/Canvas/CanvasMenu";
    public static string canvasGameEnd = "GameObjects/Canvas/CanvasGameEnd";
    public static string canvasOptions = "GameObjects/Canvas/CanvasOptions";
    public static string canvasEditor = "GameObjects/Canvas/CanvasEditor";
    public static string canvasProfileChoice = "GameObjects/Canvas/CanvasProfileChoice";
    public static string canvasScore = "GameObjects/Canvas/CanvasGameScore";
    public static string canvasWinner = "GameObjects/Canvas/CanvasWinner";
    //sprite folders
    public static string spritePlatform = "Sprite/Platform";
    public static string spriteBackground = "Sprite/Background";
    public static string spriteItem = "Sprite/Items";
    public static string spriteHat = "Sprite/Hats";
    public static string spriteWeapon = "Sprite/Weaponss/";
    public static string spriteBullet = "Sprite/Bullets/";
    public static string spriteShotEffect = "Sprite/ShotEffectAnim/";
    public static string spriteBulletAnim = "Sprite/BulletAnim/";
    public static string spriteBlood= "Sprite/Blood/";
    public static string spriteExplosion = "Sprite/Explosion/";
    public static string spriteBackgroundBack = "Sprite/BackgroundBack/";
    //sprite actual sprites
    public static string spriteNohat = "Sprite/Hats/nohat";
    public static string spriteCam = "Sprite/Items/cam";
    public static string spritePlayerSpawn = "Sprite/Items/player_spawn";
    public static string spriteWeaponSpawn = "Sprite/Items/weapon_spawn";
    
    public static string spriteFilenameCam = "cam";
    public static string spriteFilenamePlayerSpawn = "player_spawn";
    public static string spriteFilenameWeaponSpawn = "weapon_spawn";
    //mapEditor
    public static string mapEditor = "GameObjects/HelpObjects/MapEditor";
    //JSON maps file(******IF IN EDITOR = USE dataPath, IF IT IS RELEASED = USE 'maps.json') 
    public static string jsonMaps = Application.dataPath + "/Resources" + "/JSON" + "/maps.json";
    //public static string jsonMaps = "maps.json";
    //objects
    public static string platformTempObj = "GameObjects/Platform/platform";
    public static string characterObj = "GameObjects/Entity/Character";
    public static string objWeaponbox = "GameObjects/Weaponbox/weaponbox";
    public static string objConfettiSpawner = "GameObjects/Confetti/confettiSpawner";
    public static string objBackgroundBack = "GameObjects/BackgroundBack/BackgroundObject";
    //Sounds
    public static string soundFolder = "Sounds/";

}
