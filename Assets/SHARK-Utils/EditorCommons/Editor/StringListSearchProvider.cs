using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace NYX.Experimental {
    public class StringListSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        string title;
        string[] items;

        Action<string> onSetIndexCallback;
        string iconResourceDirectory;

        void Contructor(string title, string[] items, Action<string> callback, string iconResourceDirectory)
        {
            this.title = title;
            this.items = items;
            this.onSetIndexCallback = callback;
            this.iconResourceDirectory = iconResourceDirectory;
        }

        public static StringListSearchProvider CreateInstance(string title, string[] items, Action<string> callback, string iconResourceDirectory = "£$_noicon_$£")
        {
            StringListSearchProvider data = CreateInstance<StringListSearchProvider>();
            data.Contructor(title, items, callback, iconResourceDirectory);
            return data;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> search = new List<SearchTreeEntry>();
            search.Add(new SearchTreeGroupEntry(new GUIContent(title), 0));

            List<string> groups = new List<string>();
            foreach (string item in items)
            {
                string[] title = item.Split('/');
                string group = "";
                for (int i = 0; i < title.Length - 1; i++)
                {
                    group += title[i];
                    if (!groups.Contains(group))
                    {
                        Texture2D groupIcon = (Texture2D)Resources.Load($"Icons/Devices/{iconResourceDirectory}/{title[i]}", typeof(Texture2D));
                        search.Add(new SearchTreeGroupEntry(new GUIContent(title[i].Replace("_", " "), groupIcon), i + 1));
                        groups.Add(group);
                    }
                    group += "/";
                }
                Texture2D entryIcon = (Texture2D)Resources.Load($"Icons/Devices/{iconResourceDirectory}/{title.Last()}", typeof(Texture2D));
                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent("  " + title.Last().Replace("_", " "), entryIcon));
                entry.level = title.Length;
                entry.userData = title.Last();
                search.Add(entry);
            }

            return search;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            onSetIndexCallback?.Invoke((string)SearchTreeEntry.userData);
            return true;
        }
    }
}