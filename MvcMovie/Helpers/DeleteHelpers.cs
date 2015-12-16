using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using File = System.IO.File;

namespace MvcMovie.Helpers
{
    public static class DeleteHelpers
    {
        public static void DeleteDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            string[] dirs = Directory.GetDirectories(directoryPath);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(directoryPath, false);
        }


        //public static void deleteChildCollectionFromEntity(ICollection<object> childCollection, Object parentEntity)
        //{
        //    if (childCollection != null && childCollection.Any())
        //    {
        //        var reviews = childCollection.ToList();
        //        foreach (Object entity in childCollection)
        //        {
        //        }

        //    }
        //}
    }
}