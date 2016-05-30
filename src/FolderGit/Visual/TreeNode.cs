using Folder.FS;
using Folder.Visual;
using MultiSelect;
using System;
using System.IO;
using System.Windows;

namespace FolderGit.Visual
{
    class TreeNode
    {
        public static void OnExpand(Window w, MultiSelectTreeViewItem item)
        {
            item.IsExpanded = true;
            var items = item.Items;
            var node = item.DataContext as IconItem;

            if (node != null && Directory.Exists(node.Path))
            {
                (w as FolderWindowGit).txtFind.Text = node.Path;
                FolderTree.LoadSubDir(w, item, node.Path);
            }
        } 
        
    }
}
