using OutSystems.ExternalLibraries.SDK;

namespace OutSystems.ExternalLibraries.GoogleCloudStorage_Connector.Structures;

[OSStructure(Description = "Represents a file (object) stored in a Google Cloud Storage bucket.")]
public struct Object
{
    public Object()
    {
        Name = string.Empty;
        ContentType = string.Empty;
        Updated = new DateTime(1900, 1, 1);
    }

    [OSStructureField(Description = "The full path and filename of the object within the bucket.", IsMandatory = true)]
    public string Name { get; set; }

    [OSStructureField(Description = "The total size of the object in bytes.")]
    public long Size { get; set; }

    [OSStructureField(Description = "The MIME type of the object (e.g., image/png).")]
    public string ContentType { get; set; }

    [OSStructureField(Description = "The date and time (UTC) when the object was last modified.")]
    public DateTime Updated { get; set; }
}
