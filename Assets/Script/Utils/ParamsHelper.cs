#region

using System.Collections.Generic;
using Assets.Script.Utils.Json;
using UnityEngine;

#endregion

public class ParamsHelper {

    // тут все имена параметров
    public static string PARAMS_PATH = "ParamsPath";
    public static string SERVER_URL = "ServerURL";
    public static string SERVER_PORT = "ServerPort";
    public static string VERSION = "Version";
    public static string IS_VK_APPLICATION = "IsVkApplication";

    private static string RELATIVE_PARAMS_PATH = "Build/params";

    private static IDictionary<string, object> Params = null;

    public static string GetParams(string paramName) {
        if (Params == null) {
            TextAsset config = Resources.Load<TextAsset>(RELATIVE_PARAMS_PATH);
            Params = SimpleJson.DeserializeObject<IDictionary<string, object>>(config.text);
        }
        object res = new object();
        if (!Params.TryGetValue(paramName, out res)) {
            Debug.Log("Params does not contain key: " + paramName);
        }
        return res.ToString();
    }
}
