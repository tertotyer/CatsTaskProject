﻿using CatsTaskProject.Models;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;

namespace CatsTaskProject.Managers
{
    internal class ImageManager
    {
        private string _imageDirectory;

        public ImageManager()
        {
            _imageDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Images");

            CreateBaseImageFiles();
        }

        public string ImageDirectory
        {
            get => _imageDirectory;
        }

        public async Task<CatImage> GetImageById(string imageId)
        {
            CatAPIManager apiManager = CatAPIManager.Instance;
            string jsonImage = await apiManager.GetImageById(imageId);

            return JsonSerializer.Deserialize<CatImage>(jsonImage);
        }

        public async Task<IList<CatImage>> GetBreedImages(string breedId, int quantity)
        {
            CatAPIManager apiManager = CatAPIManager.Instance;
            string jsonImage = await apiManager.GetBreedImages(breedId, quantity);

            return JsonSerializer.Deserialize<IList<CatImage>>(jsonImage);
        }

        public async Task LoadImage(string imageUrl)
        {
            string imagePath = GetImagePath(imageUrl);

            var res = await new HttpClient().GetAsync(imageUrl);

            byte[] bytes = await res.Content.ReadAsByteArrayAsync();

            using Image image = Image.FromStream(new MemoryStream(bytes));
            image.Save(imagePath);
        }

        public bool ImageAlreadyLoadedById(string imageId)
        {
            DirectoryInfo root = new(_imageDirectory);
            string searchPattern = string.Concat(imageId, ".*");

            return root.GetFiles(searchPattern).Length > 0;
        }

        public bool ImageAlreadyLoadedByUrl(string imageUrl)
        {
            return File.Exists(GetImagePath(imageUrl));
        }

        internal string GetImagePath(string imageUrl)
        {
            return Path.Combine(_imageDirectory, Path.GetFileName(imageUrl));
        }

        private void CreateBaseImageFiles()
        {
            Directory.CreateDirectory(_imageDirectory);
            string noneCatImageFullPath = Path.Combine(Directory.GetParent(Assembly.GetEntryAssembly().FullName).Parent.Parent.Parent.FullName,
                "Resources\\Images\\NoneCat.jpeg");
            if (!File.Exists(Path.Combine(_imageDirectory, "NoneCat.jpeg")))
                File.Copy(noneCatImageFullPath, Path.Combine(_imageDirectory, "NoneCat.jpeg"));
        }
    }
}
