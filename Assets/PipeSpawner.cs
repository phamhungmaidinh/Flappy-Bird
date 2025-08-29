using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class PipeSpawner : MonoBehaviour
{
    public GameObject pipe;
    public float spawn_rate;
    public float heightOffsetUp;
    public float heightOffsetDown;
    private float timer;
    LuaEnv luaEnv;
    LuaTable luaTable;
    Action<LuaTable> luaUpdate;
    Action<LuaTable> luaStart;
    Action<GameObject, Vector3, Quaternion> instanFunc;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        instanFunc = (go,pos,rot) => Instantiate(go,pos,rot);
        string path = Path.Combine(Application.streamingAssetsPath, "Lua/pipe_spawner.lua");
        string script = File.ReadAllText(path);

        luaTable = luaEnv.DoString(script)[0] as LuaTable;
        luaTable.Set("pipe", pipe);
        luaTable.Set("transform", transform);
        luaTable.Set("Instantiate",instanFunc);
        luaTable.Set("rate", spawn_rate);
        luaTable.Set("timer", timer);
        luaTable.Set("deltaHeightUp", heightOffsetUp);
        luaTable.Set("deltaHeightDown", heightOffsetDown);
        luaStart = luaTable.Get<Action<LuaTable>>("Start");
        luaUpdate = luaTable.Get<Action<LuaTable>>("Update");
        luaStart(luaTable);
    }

    // Update is called once per frame
    void Update()
    {
        luaUpdate(luaTable);
    }
}
