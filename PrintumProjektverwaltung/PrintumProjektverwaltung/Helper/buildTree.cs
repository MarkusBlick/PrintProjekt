using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrintumProjektverwaltung.Models;

namespace PrintumProjektverwaltung.Helper
{
    class buildTree
    {
        internal static void addProjekte(TreeView treeView1, List<printumProjekt> alleProjekte)
        {
            treeView1.Nodes.Clear();
            foreach (var item in alleProjekte)
            {

                TreeNode parent = new TreeNode();
                parent.ImageKey = "Folder.gif";
                parent.Tag = item;
                parent.Text = item.Projektnummer.ToString() + " - " + item.Projektname;
                treeView1.Nodes.Add(parent);



            }
        }

        internal static void filter(TreeView treeView1, List<printumProjekt> alleProjekte, string suchtxt)
        {
            treeView1.Nodes.Clear();
            foreach (var item in alleProjekte)
            {
                string txt = item.Projektnummer.ToString() + " - " + item.Projektname;

                if (txt.ToLower().Contains(suchtxt.ToLower()))
                {

                TreeNode parent = new TreeNode();
                parent.ImageKey = "Folder.gif";
                parent.Tag = item;
                parent.Text = txt;
                treeView1.Nodes.Add(parent);


                }

            }
        }
    }
}
