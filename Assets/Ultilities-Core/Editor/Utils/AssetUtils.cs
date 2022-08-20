using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Ultilities.Core.Editor.Utils
{
    public static class AssetUtils
    {
        public static List<T> GetAtPath<T>(string path, bool isRecusive = true)
        {
            ArrayList al = new ArrayList();
            string absolutePath = Application.dataPath + "/" + path;
            SearchOption searchOption = isRecusive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] fileEntries = Directory.GetFiles(absolutePath, "*", searchOption);

            int j = 0;
            foreach (string fileName in fileEntries)
            {
                j++;
                int assetPathIndex = fileName.IndexOf("Assets");
                string localPath = fileName.Substring(assetPathIndex);

                UnityEngine.Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

                if (t != null)
                {
                    al.Add(t);
                }

                //EditorUtility.DisplayProgressBar("Get Assets", fileName, (float)(j + 1) / fileEntries.Length);
            }

            //EditorUtility.ClearProgressBar();

            List<T> result = new List<T>(al.Count);
            for (int i = 0; i < al.Count; i++)
            {
                result.Add((T) al[i]);
            }

            return result;
        }

        public static T GetSingleAtPath<T>(string filepath, bool isRecusive = true) where T : UnityEngine.Object
        {
            ArrayList al = new ArrayList();
            string absolutePath = Application.dataPath + "/" + filepath;
            SearchOption searchOption = isRecusive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] fileEntries = Directory.GetFiles(absolutePath, "*", searchOption);

            int j = 0;
            foreach (string fileName in fileEntries)
            {
                j++;
                int assetPathIndex = fileName.IndexOf("Assets");
                string localPath = fileName.Substring(assetPathIndex);

                UnityEngine.Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

                if (t != null)
                {
                    return (T) t;
                }
            }

            return default(T);
        }
    }
}