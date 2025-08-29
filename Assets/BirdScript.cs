using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    public float flapStrength;
    LuaEnv luaEnv;
    LuaTable luaTable;
    Action<LuaTable> luaUpdate;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        string path = Path.Combine(Application.streamingAssetsPath, "Lua/bird.lua");
        string script = File.ReadAllText(path);

        luaTable = luaEnv.DoString(script)[0] as LuaTable;
        luaTable.Set("myrigidbody", myrigidbody );
        luaTable.Set("multi", flapStrength);
        luaUpdate = luaTable.Get<Action<LuaTable>>("Update");
    }

    // Update is called once per frame
    void Update()
    {
        luaUpdate(luaTable);
    }
}
