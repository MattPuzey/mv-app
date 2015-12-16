using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MvcMovie.Models;
using File = System.IO.File;

namespace MvcMovie.Helpers
{
    public static class DeleteHelpers
    {
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }


        //public static void deleteChildCollectionFromEntity(ICollection<object> collection, Object parentEntity)
        //{
        //    if(collection != null && collection.Any())
        //    {
        //        var reviews = collection.ToList();
        //        foreach (Object entity in collection)
        //        {
        //        }

        //    }
        //}
    }
}