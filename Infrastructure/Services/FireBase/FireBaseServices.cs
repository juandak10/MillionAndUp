using Application.Contracts.Infrastructure;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services.FireBase
{
    public class FireBaseServices : IFireBaseServices
    {

        //Method to save an image in firebase storage
        public async Task<string> UpLoadImage(Stream stream, string fileName, IConfiguration config)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(config.GetSection("Storage")["ApiKey"]));
            var a = await auth.SignInWithEmailAndPasswordAsync(config.GetSection("Storage")["AuthEmail"], config.GetSection("Storage")["AuthPassword"]);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                 config.GetSection("Storage")["Bucket"],
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("Property")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        //Method to validate image
        public bool IsImage(string image)
        {
            if (Regex.IsMatch(image.ToLower(), @"^.*\.(jpg|gif|png|jpeg)$")) return true;
            return false;
        }

    }
}
