using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1;
using Google.Cloud.Storage.V1;
using OutSystems.ExternalLibraries.GoogleCloudStorage_Connector.Structures;
using System.Net;
using File = OutSystems.ExternalLibraries.GoogleCloudStorage_Connector.Structures.File;
using Object = OutSystems.ExternalLibraries.GoogleCloudStorage_Connector.Structures.Object;

namespace OutSystems.ExternalLibraries.GoogleCloudStorage_Connector;

public class GoogleCloudStorage : IGoogleCloudStorage
{
    private ServiceAccountCredential GetServiceAccountCredential(Authentication authentication)
    {
        var initializer = new ServiceAccountCredential.Initializer(authentication.ClientEmail)
        {
            Scopes = new[] { StorageService.Scope.CloudPlatform }
        }.FromPrivateKey(authentication.PrivateKey.Replace("\\n", "\n"));

        return new ServiceAccountCredential(initializer);
    }

    private StorageClient GetStorageClient(Authentication authentication)
    {
        var credential = GetServiceAccountCredential(authentication);
        return StorageClient.Create(credential.ToGoogleCredential());
    }

    public void Object_Upload(Authentication authentication, string bucketName, string objectName, File file)
    {
        var storageClient = GetStorageClient(authentication);
        using var stream = new MemoryStream(file.Content);
        storageClient.UploadObject(bucketName, objectName, file.ContentType, stream);
    }

    public void Object_Download(Authentication authentication, string bucketName, string objectName, out File file)
    {
        var storageClient = GetStorageClient(authentication);
        using var stream = new MemoryStream();
        var obj = storageClient.DownloadObject(bucketName, objectName, stream);
        
        file = new File
        {
            Content = stream.ToArray(),
            ContentType = obj.ContentType
        };
    }

    public void Object_List(Authentication authentication, string bucketName, string prefix, out IEnumerable<Object> objects)
    {
        var storageClient = GetStorageClient(authentication);
        var gcsObjects = storageClient.ListObjects(bucketName, prefix);

        objects = gcsObjects.Select(obj => new Object
        {
            Name = obj.Name,
            Size = (long)(obj.Size ?? 0),
            ContentType = obj.ContentType,
            Updated = obj.UpdatedDateTimeOffset?.UtcDateTime ?? new DateTime(1900, 1, 1)
        }).ToList();
    }

    public void Object_Exists(Authentication authentication, string bucketName, string objectName, out bool exists)
    {
        var storageClient = GetStorageClient(authentication);
        try
        {
            storageClient.GetObject(bucketName, objectName);
            exists = true;
        }
        catch (Google.GoogleApiException e) when (e.HttpStatusCode == HttpStatusCode.NotFound)
        {
            exists = false;
        }
    }

    public void Object_Delete(Authentication authentication, string bucketName, string objectName)
    {
        var storageClient = GetStorageClient(authentication);
        storageClient.DeleteObject(bucketName, objectName);
    }

    public void Object_GetSignedUrl(Authentication authentication, string bucketName, string objectName, int expirationMinutes, out string url)
    {
        var credential = GetServiceAccountCredential(authentication);
        var urlSigner = UrlSigner.FromCredential(credential);

        url = urlSigner.Sign(
            bucketName,
            objectName,
            TimeSpan.FromMinutes(expirationMinutes),
            HttpMethod.Get
        );
    }

    public void Bucket_List(Authentication authentication, out IEnumerable<Bucket> buckets)
    {
        var storageClient = GetStorageClient(authentication);
        var gcsBuckets = storageClient.ListBuckets(authentication.ProjectId);

        buckets = gcsBuckets.Select(b => new Bucket
        {
            Name = b.Name,
            Location = b.Location,
            StorageClass = b.StorageClass,
            Created = b.TimeCreatedDateTimeOffset?.UtcDateTime ?? new DateTime(1900, 1, 1)
        }).ToList();
    }

    public void Bucket_Create(Authentication authentication, string bucketName, string location)
    {
        var storageClient = GetStorageClient(authentication);
        storageClient.CreateBucket(
            authentication.ProjectId,
            new Google.Apis.Storage.v1.Data.Bucket
            {
                Name = bucketName,
                Location = location
            }
        );
    }

    public void Bucket_Delete(Authentication authentication, string bucketName)
    {
        var storageClient = GetStorageClient(authentication);
        storageClient.DeleteBucket(bucketName);
    }
}
