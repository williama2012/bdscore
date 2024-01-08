using System;
using System.IO;
using System.Collections;

namespace bds.file {
    public static class listFiles {

        /*  */
        public static ArrayList Files(String[] directory, string extension) {
            ArrayList al = new ArrayList();

            foreach (string i in directory)            {
                al.AddRange(DirSearch(i, extension));

            }
            return al;
        }

        /*  */
        public static ArrayList Files(string directory, string extension) {
            return DirSearch(directory, extension);
        }


        /*  */
        private static ArrayList DirSearch(string sDir, string ext) {
            ArrayList al = new ArrayList();

            try {
                foreach (String f in Directory.GetFiles(sDir, ext)) {
                    al.Add(f);
                }
                foreach (String d in Directory.GetDirectories(sDir)) {
                    foreach (String f in Directory.GetFiles(d, ext)) {
                        al.Add(f);
                    }
                    al.AddRange(DirSearch(d, ext));
                }
                return al;
            } catch (Exception e) {
                return null;
            }
        }


    }
}
