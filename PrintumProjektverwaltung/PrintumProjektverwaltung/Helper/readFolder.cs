using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PrintumProjektverwaltung.Models;
using Newtonsoft.Json;

namespace PrintumProjektverwaltung.Helper
{
    class readFolder
    {

        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();


        internal static printumProjekt selectFolderAndScanIt(string ordnerpfad)
        {
            printumProjekt dasProjekt = new printumProjekt();
            dasProjekt.Unterordner = new List<printumOrdner>();
            dasProjekt.Dateien = new List<printumDatei>();

            try
            {
                DirectoryInfo rootDir = new DirectoryInfo(ordnerpfad);
                if (rootDir.Name.Contains("-"))
                {
                    var projektnameArray = rootDir.Name.Split('-');
                    string projektNr = projektnameArray[0].Trim();
                    string projektName = projektnameArray[1].Trim();

                    int nr;
                    var ergebniss = Int32.TryParse(projektNr, out nr);


                    dasProjekt.Projektnummer = nr;
                    dasProjekt.Projektname = projektName;

                }

                WalkDirectoryTree(rootDir, dasProjekt);
            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteDebugLog(ex.ToString());
            }

            addJsonsTo(dasProjekt);

            return dasProjekt;


        }



        static void WalkDirectoryTree(DirectoryInfo root, printumProjekt dasProjekt)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;


            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }


            // Die DAteien
            if (files != null)
            {


                foreach (System.IO.FileInfo fi in files)
                {
                    // Hier wird gearbeitet
                    //  
                    try
                    {
                        printumDatei pd = new printumDatei();
                        pd.Name = fi.Name;
                        pd.Pfad = fi.DirectoryName;
                        pd.Projektnummer = dasProjekt.Projektnummer;
                        pd.Typ = fi.Extension;
                        pd.erstellt = fi.CreationTime;
                        pd.geaendert = fi.LastWriteTime;

                        dasProjekt.Dateien.Add(pd);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Unterordner merken
                    printumOrdner po = new printumOrdner();
                    po.Name = dirInfo.Name;
                    po.Pfad = dirInfo.FullName;
                    po.Projektnummer = dasProjekt.Projektnummer;
                    po.Rootname = dirInfo.Parent.Name;

                    dasProjekt.Unterordner.Add(po);

                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, dasProjekt);
                }
            }
        }


        private static void addJsonsTo(printumProjekt dasProjekt)
        {
            StringBuilder sbFile = new StringBuilder();
            StringWriter swFile = new StringWriter(sbFile);

            using (JsonWriter schreiber = new JsonTextWriter(swFile))
            {
                schreiber.WriteStartObject();
                foreach (var datei in dasProjekt.Dateien)
                {
                    if (datei.Typ.Contains("msg"))
                    {
                        schreiber.WritePropertyName("Mail");
                        schreiber.WriteValue(datei.Pfad + @"\" + datei.Name);
                    }
                    else
                    {
                        schreiber.WritePropertyName("File");
                        schreiber.WriteValue(datei.Pfad + @"\" + datei.Name);

                    }

                }


                schreiber.WriteEnd();
                //schreiber.WriteEndObject();

                dasProjekt.MailsJSON = sbFile.ToString();
            }
        }
    }
}
