using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class SaveScreen1 : MonoBehaviour {
	
	public Camera mainCam;
	public bool showText = false;
	string path;
	public string directoryName = "Screens";
	string mainPath = "";
	string cName;
	int i = 0;
	
	void Start() {
		path = Application.dataPath + "/";
		mainPath = path + directoryName + "/";

		if(!Directory.Exists(mainPath)) {           
			Directory.CreateDirectory(mainPath);            
		}
	}
	
	IEnumerator SaveScreenshot(string name) {
		int sw = 300;
		int sh = 600;
		
		RenderTexture rt = new RenderTexture(sw,sh,0);
		mainCam.targetTexture = rt;
		Texture2D sc = new Texture2D(sw,sh,TextureFormat.RGB24, false);
		mainCam.Render();
		
		yield return new WaitForSeconds(0.3f);
		showText = true;
		
		
		RenderTexture.active = rt;
		sc.ReadPixels(new Rect(0,0,sw,sh), 0,0);
		mainCam.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);
		
		byte[] bytes = sc.EncodeToPNG();
		string finalPath = mainPath + name + ".png";
		File.WriteAllBytes(finalPath, bytes);
		
		if (bytes != null) {
			WWWForm form = new WWWForm ();
			form.AddBinaryData("file", bytes, "screenShot1.png", "images/"); 

			byte[] rawData = form.data;
			string url = "http://128.71.46.246:3334/unity/upload.php";
			var headers = form.headers;

			WWW www = new WWW(url, rawData, headers);
			StartCoroutine (WaitForRequest (www));
		}
		yield return new WaitForSeconds(2.3f);
		showText = false;
	}
	
	void OnGUI() {
		if(showText==true) {
			GUI.Label(new Rect(100,200,200,50), "Сохранение снимка");
		}
	}
	
	
	
	IEnumerator WaitForRequest(WWW web) {
		yield return web;
		if (web.error != null) {
			Debug.Log("Ошибка: " + web.error);
		} else {
			Debug.Log(web.text);
		}
	}
	
	void Update() {
		
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			i++;
			cName = "Screen_"+i;
			StartCoroutine(SaveScreenshot(cName));
			showText = true;
		}
		
	}
	
	
}
