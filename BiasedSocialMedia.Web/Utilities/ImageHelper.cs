﻿using BiasedSocialMedia.Web.Models;
using BiasedSocialMedia.Web.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BiasedSocialMedia.Web.Utilities
{
    public class ImageHelper:IImageHelper
    {
        //TODO:
        // One method for parsing the byte from DB and forwading the parsed data to URL
        // one method for generating the required URL
        private DataRepository dataRepository;
        public ImageHelper(DataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public byte[] GetImageFromDB(int id)
        {
            var model = dataRepository.MediaInfo.FirstOrDefault(x => x.MediaID == id);
            if (model != null)
                return model.Data;
            return null;
        }

        public int InsertImageToDB(HttpPostedFileBase file)
        {
            ImageUploadModel model = new ImageUploadModel();
            if (file != null)
            {
                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                byte[] imgData = ms.ToArray();
                var InputFileName = Path.GetFileName(file.FileName);
                model.Data = imgData;
                model.FileName = InputFileName;
                dataRepository.MediaInfo.Add(model);
                dataRepository.SaveChanges();
                return model.MediaID;
            }
            return 0;
        }
    }
}