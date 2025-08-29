using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class MovingPipe : MonoBehaviour
{
    public float moveSpeed;
    public float deadZone;
    LuaEnv luaEnv;
    LuaTable luaTable;
    Action<LuaTable> luaUpdate;
    Action<GameObject> destroy;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        destroy=(self) => Destroy(self);
        string path = Path.Combine(Application.streamingAssetsPath, "Lua/moving_pipe.lua");
        string script = File.ReadAllText(path);

        luaTable = luaEnv.DoString(script)[0] as LuaTable;
        luaTable.Set("xBorder",deadZone);
        luaTable.Set("Destroy", destroy);
        luaTable.Set("multi", moveSpeed);
        luaTable.Set("transform", transform);
        luaTable.Set("gameObject", gameObject);
        luaUpdate = luaTable.Get<Action<LuaTable>>("Update");
    }

    // Update is called once per frame
    void Update()
    {
        luaUpdate(luaTable);
    }
}
