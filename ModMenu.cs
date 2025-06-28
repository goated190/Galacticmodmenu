
using BepInEx;
using UnityEngine;
using System.Collections.Generic;

[BepInPlugin("com.galactic.modmenu", "Galactic Mod Menu", "1.0.0")]
public class ModMenu : BaseUnityPlugin
{
    private bool showMenu = true;
    private Rect menuRect = new Rect(20, 20, 300, 600);
    private Vector2 scrollPosition;

    // Categories and mods dictionary
    private Dictionary<string, List<Mod>> modCategories = new Dictionary<string, List<Mod>>();

    public ModMenu()
    {
        // Fill categories with 20 mods each (total 200 mods, 10 categories)
        modCategories = new Dictionary<string, List<Mod>>()
        {
            {"Movement Mods", new List<Mod>()},
            {"Troll Mods", new List<Mod>()},
            {"Visual Mods", new List<Mod>()},
            {"World Mods", new List<Mod>()},
            {"Player Mods", new List<Mod>()},
            {"Weapon Mods", new List<Mod>()},
            {"Social Mods", new List<Mod>()},
            {"Camera Mods", new List<Mod>()},
            {"Misc Mods", new List<Mod>()},
            {"Dev Tools", new List<Mod>()},
        };

        int modCount = 1;
        foreach (var cat in modCategories.Keys)
        {
            for (int i = 1; i <= 20; i++)
            {
                modCategories[cat].Add(new Mod() { Name = cat + " Mod " + i, Enabled = false });
                modCount++;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            showMenu = !showMenu;

        // Example: Implement speed mod if enabled (placeholder logic)
        var movementMods = modCategories["Movement Mods"];
        if (movementMods[0].Enabled) // Movement Mod 1 as Speed Boost example
        {
            var player = GorillaLocomotion.Player.Instance;
            if (player != null)
                player.maxJumpSpeed = 20f;
        }
        else
        {
            var player = GorillaLocomotion.Player.Instance;
            if (player != null)
                player.maxJumpSpeed = 7f;
        }

        // Other mods placeholder logic goes here...
    }

    void OnGUI()
    {
        if (!showMenu) return;

        menuRect = GUI.Window(0, menuRect, DrawMenu, "Galactic Mod Menu");
    }

    private void DrawMenu(int windowID)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(280), GUILayout.Height(580));
        foreach (var category in modCategories)
        {
            GUILayout.Label("===== " + category.Key + " =====");
            foreach (var mod in category.Value)
            {
                mod.Enabled = GUILayout.Toggle(mod.Enabled, mod.Name);
            }
            GUILayout.Space(10);
        }
        GUILayout.EndScrollView();
        GUI.DragWindow();
    }

    class Mod
    {
        public string Name;
        public bool Enabled;
    }
}
