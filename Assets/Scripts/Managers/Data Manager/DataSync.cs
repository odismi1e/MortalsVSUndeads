using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.SimpleLocalization
{ 
	[ExecuteInEditMode]
	public class DataSync : MonoBehaviour
	{
		public string TableId;
		public Sheet[] Sheets;
		public UnityEngine.Object SaveFolder;

		private const string UrlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";

#if UNITY_EDITOR
		public void Sync()
		{
			StopAllCoroutines();
			StartCoroutine(SyncCoroutine());
		}

		private IEnumerator SyncCoroutine()
		{
			var folder = AssetDatabase.GetAssetPath(SaveFolder);

			Debug.Log("<color=yellow>Sync started, please wait for confirmation message...</color>");

			var dict = new Dictionary<string, UnityWebRequest>();

			foreach (var sheet in Sheets)
			{
				var url = string.Format(UrlPattern, TableId, sheet.Id);

				Debug.Log($"Downloading: {url}...");

				dict.Add(url, UnityWebRequest.Get(url));
			}

			foreach (var entry in dict)
			{
				var url = entry.Key;
				var request = entry.Value;

				if (!request.isDone)
				{
					yield return request.SendWebRequest();
				}

				if (request.error == null)
				{
					var sheet = Sheets.Single(i => url == string.Format(UrlPattern, TableId, i.Id));
					var path = System.IO.Path.Combine(folder, sheet.Name + ".csv");

					System.IO.File.WriteAllBytes(path, request.downloadHandler.data);
					Debug.LogFormat("Sheet {0} downloaded to {1}", sheet.Id, path);
				}
				else
				{
					throw new Exception(request.error);
				}
			}

			Debug.Log("<color=green>Data successfully synced!</color>");
		}

#endif
	}
}
