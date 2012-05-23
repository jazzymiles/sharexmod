using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IndexersLib
{
    public static class QuickIndexer
    {
        public static string Index(string dir, bool html, IndexerConfig config = null)
        {
            var settings = new IndexerAdapter();

            if (config == null)
                config = new IndexerConfig();

            config.CollapseFolders = false;
            config.FolderExpandLevel = 2;
            settings.LoadConfig(config);

            config.FolderList.Clear();

            settings.GetConfig().SetSingleIndexPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                html ? "temp.html" : "temp.txt"));

            settings.GetConfig().FolderList.Add(dir);

            Indexer indexer = null;
            if (html)
            {
                indexer = new TreeNetIndexer(settings);
            }
            else
            {
                indexer = new TreeWalkIndexer(settings);
            }

            return indexer.IndexNow(IndexingMode.IN_ONE_FOLDER_MERGED, false);
        }
    }
}