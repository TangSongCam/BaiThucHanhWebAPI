﻿using BaiThucHanhWeb.Model.Domain;

namespace BaiThucHanhWeb.Repositories
{
    public interface IImageRepository
    {
        Image Upload(Image image);
        List<Image> GetAllInfoImages();
        (byte[], string, string) DownloadFile(int Id);
    }
}
