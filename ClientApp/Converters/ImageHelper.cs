using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.IO;

namespace ClientApp.Converters
{

    public static class ImageHelper
    {
        public static async Task<byte[]> ImageSourceToByteArrayAsync(ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            Stream stream = null;

            if (imageSource is StreamImageSource streamImageSource)
            {
                stream = await streamImageSource.Stream(CancellationToken.None);
            }
            else if (imageSource is FileImageSource fileImageSource)
            {
                stream = File.OpenRead(fileImageSource.File);
            }
            else if (imageSource is UriImageSource uriImageSource)
            {
                using var httpClient = new HttpClient();
                stream = await httpClient.GetStreamAsync(uriImageSource.Uri);
            }
            else
            {
                // Other types (e.g., FontImageSource) don’t usually have raw image bytes
                return null;
            }

            if (stream == null)
                return null;

            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return ms.ToArray();
        }
    }

}
