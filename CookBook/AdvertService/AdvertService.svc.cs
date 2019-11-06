using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AdvertService.Models;
using AdvertService.Contracts;
using System.ServiceModel;

namespace AdvertService
{
   public class AdvertService : IAdvertContract
    {        
        public List<AdvertType> GetAdvertImages(string directoryPath)
        {
            List<AdvertType> fullImageList = new List<AdvertType>();

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            if(directoryInfo==null)
            {
                throw new FaultException($"The directory \"{directoryPath}\" is not exists");
            }
            FileInfo[] files = directoryInfo.GetFiles("*.png");
            if (files == null || files.Length==0)
            {
                throw new FaultException($"There is no files in \"{directoryPath}\" with png format");
            }
            foreach (FileInfo item in files)
            {
                char[] sep = new char[] { '.' };
                Image image = Image.FromFile(item.FullName);
                AdvertType imgAdv = new AdvertType();
                imgAdv.ImageId = int.Parse(item.Name.Split(sep)[0]);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Png);
                imgAdv.ImageByteArray = memoryStream.ToArray();
                fullImageList.Add(imgAdv);
            }

            return fullImageList;
        }

        public int[] GetRandomImageIds(string directoryPath, int numberOfImages)
        {
            if(numberOfImages<1)
            {
                throw new FaultException("Number of images should be more than 0");
            }
            List<AdvertType> fullImageList = GetAdvertImages(directoryPath);
            int[] returnedArray = new int[numberOfImages];
            Random rnd = new Random();
            bool flag = true;
            int k = 0;

            while (flag)
            {
                int randomId = rnd.Next(1, fullImageList.Count);
                if (!returnedArray.Contains(randomId))
                {
                    returnedArray[k] = randomId;
                    k++;
                }
                if (k == numberOfImages)
                {
                    flag = false;
                }
            }

            return returnedArray;
        }
    }
}
