using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class VideoManager {

    [HideInInspector]
    public static WindowResolution resolution = new WindowResolution(0,0);
    [HideInInspector]
    public static List<WindowResolution> resolutions = new List<WindowResolution>();

    [HideInInspector]
    public enum WindowMode { WINDOWED, FULLSCREEN };
    [HideInInspector]
    public static WindowMode windowMode = WindowMode.WINDOWED;
    
    static VideoManager()
    {
        ResolutionsInit();
    } 
    
    public static void ResolutionsInit()
    {
        resolutions.Add(new WindowResolution(800, 450));
        resolutions.Add(new WindowResolution(800, 600));
        resolutions.Add(new WindowResolution(800,600));
        resolutions.Add(new WindowResolution(1024, 768));
        resolutions.Add(new WindowResolution(1280, 800));
        resolutions.Add(new WindowResolution(1280, 1024));
        resolutions.Add(new WindowResolution(1366, 768));
        resolutions.Add(new WindowResolution(1920, 1080));
        resolutions.Add(new WindowResolution(1440, 900));
        resolutions.Add(new WindowResolution(1600, 900));
    }


    public static bool isFullScreen()
    {
        return (VideoManager.windowMode == VideoManager.WindowMode.FULLSCREEN) ? true : false;
    }
    public static void SetResolution(int _w, int _h)
    {
        VideoManager.resolution.width = _w;
        VideoManager.resolution.height = _h;
        UnityEngine.Screen.SetResolution(_w, _h, isFullScreen());
    }

}



//holds screen resolution
public class WindowResolution
{
    public int width;
    public int height;

    public WindowResolution(int _w, int _h)
    {
        width = _w;
        height = _h;
    }
     
}


