#region

using System.Text;
using UnityEngine;

#endregion

public class GameConsoleWithScrollView : MonoBehaviour {

    public bool show_output = true;
    public bool show_stack = true;
    public static GameConsoleWithScrollView I;
    public Rect pos_rect = new Rect(50, 75, 400, 400);
    public Rect view_rect = new Rect(0, 0, Screen.width - 100f, 60000);
    private Vector2 scroll_pos;
    public bool show = false;

    private void Awake() {
        I = this;
        strb.AppendLine("CONSOLE:");
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            show = !show;
            Debug.Log("~");
        }
    }

    //error это пользовательский, а exception - это системный "ошипка"?
    private int error_count;

    private StringBuilder strb = new StringBuilder();

    private void OnEnable() {
        Application.RegisterLogCallback(HandleLog);
    }

    private void OnDisable() {
        Application.RegisterLogCallback(null);
    }

    //чтобы сделать цветные строки нужен массив строк здесь)
    private void HandleLog(string logString, string stackTrace, LogType type) {
        if (type == LogType.Exception || type == LogType.Error) {
            error_count++;
        }

        if (show_output || show_stack) {
            //strb.Append("\n");
            if (show_output) {
                strb.AppendLine(logString);
            }
            //вписываем стек всегда если есть ошибка
            if (show_stack || type == LogType.Exception || type == LogType.Error) {
                strb.AppendLine(stackTrace);
            }
        }
    }

    public void OnGUI() {
        if (!show) {
            return;
        }

        if (GUILayout.Button("Console")) {
            show = !show;
        }
        if (GUILayout.Button("Clean console")) {
            strb = new StringBuilder();
            strb.AppendLine("/clean");
        }
        if (show) {
            pos_rect = new Rect(50f, 75f, Screen.width - 100f, Screen.height - 150f);
            GUI.Label(new Rect(pos_rect.x, pos_rect.y - 20f, 200f, 50f),
                "[errors " + error_count + "] length: " + strb.Length, "box");

            scroll_pos = GUI.BeginScrollView(pos_rect, scroll_pos, view_rect);
            GUI.TextArea(new Rect(0, 0, Screen.width - 100f, view_rect.height), strb.ToString());
            GUI.EndScrollView();
        }
    }

}